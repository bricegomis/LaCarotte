using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class TimedTaskService(ILogger<TimedTaskService> logger) : IHostedService, IDisposable
{
    private readonly ILogger<TimedTaskService> _logger = logger;
    private readonly List<(Timer timer, Func<Task> task)> _tasks = [];

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("TimedTaskService running.");
        return Task.CompletedTask;
    }

    public void DoSomethingAt(Func<Task> method, DayOfWeek dayOfWeek, int hour)
    {
        var initialDelay = GetInitialDelayForWeeklyTask(dayOfWeek, hour);
        var timer = new Timer(async _ => await ExecuteTask(method), null, initialDelay, TimeSpan.FromDays(7));

        _tasks.Add((timer, method));
    }

    private TimeSpan GetInitialDelayForWeeklyTask(DayOfWeek dayOfWeek, int hour)
    {
        var now = DateTime.Now;
        int daysUntilNext = ((int)dayOfWeek - (int)now.DayOfWeek + 7) % 7;
        if (daysUntilNext == 0 && now.Hour >= hour)
        {
            daysUntilNext = 7;
        }
        var nextScheduledTime = now.Date.AddDays(daysUntilNext).AddHours(hour);

        return nextScheduledTime - now;
    }

    private async Task ExecuteTask(Func<Task> method)
    {
        try
        {
            _logger.LogInformation("Executing scheduled task at: {time}", DateTimeOffset.Now);
            await method();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while executing scheduled task.");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        foreach (var task in _tasks)
        {
            task.timer?.Change(Timeout.Infinite, 0);
        }

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        foreach (var task in _tasks)
        {
            task.timer?.Dispose();
        }
    }
}

