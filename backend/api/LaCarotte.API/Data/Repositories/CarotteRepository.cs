using Ati.API.Common.Attributes;
using LaCarotte.API.Models.Documents;
using LaCarotte.API.Repositories.Interfaces;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace LaCarotte.API.Repositories;

[Injectable]
public class CarotteRepository(IAsyncDocumentSession session) : ICarotteRepository
{
    public async Task<CarotteDocument?> GetByIdAsync(string id)
    {
        return await session.LoadAsync<CarotteDocument>(id);
    }

    public async Task<List<CarotteDocument>> ListAsync()
    {
        return await session.Query<CarotteDocument>()
            .ToListAsync();
    }
    
    public async Task AddAsync(CarotteDocument document)
    {
        await session.StoreAsync(document);
        await session.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(CarotteDocument product)
    {
        await session.StoreAsync(product);
        await session.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var item = await session.LoadAsync<CarotteDocument>(id);
        if (item != null)
        {
            session.Delete(item);
            await session.SaveChangesAsync();
        }
    }
}