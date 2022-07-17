using AutoMapper;
using UnitOfMeasures.Application.Models.DTO.CoefficientUnitDTOs;
using UnitOfMeasures.Domain.Models;

namespace UnitOfMeasures.Infrastructure.Mapper
{
    public class CoefficientUnitProfile : Profile
    {
        public CoefficientUnitProfile()
        {
            CreateMap<CreateCoefficientUnitDTO, CoefficientUnit>().ForMember(b=> b.BaseMeasuremenID, opt=> opt.MapFrom(c=> c.BaseMeasurementUnitDetailDTO.ID));    
        }
    }
}
