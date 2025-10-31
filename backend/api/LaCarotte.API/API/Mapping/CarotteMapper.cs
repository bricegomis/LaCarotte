using Ati.API.Common.Attributes;
using LaCarotte.API.Models.Documents;
using LaCarotte.API.Models.Dto;
using LaCarotte.API.Models.Mapping.Interfaces;
using Riok.Mapperly.Abstractions;

namespace LaCarotte.API.Models.Mapping;

[Injectable(ServiceLifetime.Singleton)]
[Mapper]
public partial class CarotteMapper : ICarotteMapper
{
    [MapperIgnoreSource(nameof(CarotteDocument.ProfileId))]
    public partial CarotteDto ToDto(CarotteDocument doc);
    
    // public partial List<CarotteDto> ToDtoList(List<CarotteDocument> docs);

    // // Mapping for create/update DTOs  
    // [MapperIgnoreTarget(nameof(CarotteDocument.Id))]
    // [MapperIgnoreTarget(nameof(CarotteDocument.DateCreated))]
    // [MapperIgnoreTarget(nameof(CarotteDocument.DateModified))]
    // [MapperIgnoreTarget(nameof(CarotteDocument.History))]
    // [MapperIgnoreTarget(nameof(CarotteDocument.HistoryBonus))]
    // public partial CarotteDocument ToDocument(CarotteCreateDto dto, string profileId);
    
    // [MapperIgnoreTarget(nameof(CarotteDocument.DateCreated))]
    // [MapperIgnoreTarget(nameof(CarotteDocument.DateModified))]
    // [MapperIgnoreTarget(nameof(CarotteDocument.History))]
    // [MapperIgnoreTarget(nameof(CarotteDocument.HistoryBonus))]
    // public partial void UpdateDocument(CarotteUpdateDto dto, CarotteDocument document);
}