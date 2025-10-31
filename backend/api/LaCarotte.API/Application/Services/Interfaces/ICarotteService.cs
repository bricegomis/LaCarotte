using LaCarotte.API.Models.Dto;

namespace LaCarotte.API.Manager.Interfaces;

public interface ICarotteService
{
    Task Create(CarotteCreateDto carotte);
    Task<CarotteDto> Get(string id);
    Task<List<CarotteDto>> List();
    Task Update(CarotteUpdateDto carotte);
    Task Delete(string id);
}