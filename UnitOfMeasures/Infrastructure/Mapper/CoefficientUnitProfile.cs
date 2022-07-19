using AutoMapper;
using UnitOfMeasures.Application.Models.DTO;
using UnitOfMeasures.Application.Models.DTO.CoefficientUnitDTOs;
using UnitOfMeasures.Application.Models.Enums;
using UnitOfMeasures.Domain.Models;

namespace UnitOfMeasures.Infrastructure.Mapper
{
    public class CoefficientUnitProfile : Profile
    {
        public CoefficientUnitProfile()
        {
            CreateMap<CreateCoefficientUnitDTO, CoefficientUnit>();
            CreateMap<CoefficientUnit, CoefficientUnitDetailDTO>();
            CreateMap<CoefficientUnit, UnitsDTO>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.ChildUnit.Name))
                .ForMember(x => x.Code, opt => opt.MapFrom(x => x.ChildUnit.Code))
                .ForMember(b => b.Type, opt => opt.MapFrom(_ => UnitType.coefficient));
        }
    }
}
