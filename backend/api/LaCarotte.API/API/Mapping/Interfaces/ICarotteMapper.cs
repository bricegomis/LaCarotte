using LaCarotte.API.Models.Documents;
using LaCarotte.API.Models.Dto;

namespace LaCarotte.API.Models.Mapping.Interfaces;

public interface ICarotteMapper
{
    CarotteDto ToDto(CarotteDocument doc);
    // List<CarotteDto> ToDtoList(List<CarotteDocument> docs);
    // CarotteDocument ToDocument(CarotteCreateDto dto, string profileId);
    // void UpdateDocument(CarotteUpdateDto dto, CarotteDocument document);
}