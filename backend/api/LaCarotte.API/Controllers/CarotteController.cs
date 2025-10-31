using System.ComponentModel.DataAnnotations;
using LaCarotte.API.Manager;
using LaCarotte.API.Manager.Interfaces;
using LaCarotte.API.Models;
using LaCarotte.API.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace LaCarotte.API.Controllers;

[Route("[controller]")]
[ApiController]
public class CarotteController(
    ICarotteService carotteService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<CarotteDto>> ListAsync()
    {
        return await carotteService.List();
    }

    [HttpGet("{id}")]
    [Produces(typeof(CarotteDto))]
    public async Task<CarotteDto> Get([FromRoute, Required]string id)
    {
        return await carotteService.Get(id);
    }

    [HttpPost]
    public async Task CreateCarotte([FromBody, Required]CarotteCreateDto item)
    {
        await carotteService.Create(item);
    }

    [HttpPut]
    public async Task UpdateCarotte([FromBody, Required]CarotteUpdateDto item)
    {
        await carotteService.Update(item);
    }

    [HttpDelete("{id}")]
    public async Task DeleteCarotte([FromRoute, Required]string id)
    {
        await carotteService.Delete(id);
    }
}