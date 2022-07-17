using AutoMapper;
using UnitOfMeasures.Application.Models.DTO.BaseMeasurementUnitDTOs;
using UnitOfMeasures.Domain.Models;

namespace UnitOfMeasures.Infrastructure.Mapper
{
    public class BaseMeasurementUnitProfile : Profile
    {
        public BaseMeasurementUnitProfile()
        {
            CreateMap<CreateBaseMeasurementUnitDTO, BaseMeasurementUnit>().ForMember(b=> b.MeasurementDimensionID, opt=> opt.MapFrom(c=> c.MeasurementDimension.ID));
            CreateMap<BaseMeasurementUnit, BaseMeasurementUnitDetailDTO>(); 
        }
    }
}
