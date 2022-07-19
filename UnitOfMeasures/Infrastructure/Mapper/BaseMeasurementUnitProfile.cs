using AutoMapper;
using UnitOfMeasures.Application.Models.DTO;
using UnitOfMeasures.Application.Models.DTO.BaseMeasurementUnitDTOs;
using UnitOfMeasures.Application.Models.Enums;
using UnitOfMeasures.Domain.Models;

namespace UnitOfMeasures.Infrastructure.Mapper
{
    public class BaseMeasurementUnitProfile : Profile
    {
        public BaseMeasurementUnitProfile()
        {
            CreateMap<CreateBaseMeasurementUnitDTO, BaseMeasurementUnit>();
            CreateMap<BaseMeasurementUnit, BaseMeasurementUnitDetailDTO>();
            CreateMap<BaseMeasurementUnit, UnitsDTO>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Code, opt => opt.MapFrom(x => x.Code))
                .ForMember(b=> b.Type, opt=> opt.MapFrom(_=> UnitType.baseMeasure));
        }
    }
}
