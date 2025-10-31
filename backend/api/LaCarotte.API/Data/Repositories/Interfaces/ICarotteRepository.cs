using LaCarotte.API.Models.Documents;

namespace LaCarotte.API.Repositories.Interfaces;

public interface ICarotteRepository
{
    Task<CarotteDocument?> GetByIdAsync(string id);
    Task<List<CarotteDocument>> ListAsync();
    Task AddAsync(CarotteDocument document);
    Task UpdateAsync(CarotteDocument product);
    Task DeleteAsync(string id);
}