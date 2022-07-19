using AutoMapper;
using UnitOfMeasures.Application.Models.DTO;
using UnitOfMeasures.Application.Models.DTO.FormulaUnitDTOs;
using UnitOfMeasures.Application.Models.Enums;
using UnitOfMeasures.Domain.Models;

namespace UnitOfMeasures.Infrastructure.Mapper
{
    public class FormulaUnitProfile : Profile
    {
        public FormulaUnitProfile()
        {
           CreateMap<CreateFormulaUnitDTO, FormulaUnit>();
           CreateMap<FormulaUnit, FormulaUnitDetailDTO>();
            CreateMap<FormulaUnit, UnitsDTO>()
                .ForMember(x => x.Name, opt=> opt.MapFrom(x=>x.ChildUnit.Name))
                .ForMember(x => x.Code, opt=> opt.MapFrom(x=>x.ChildUnit.Code))
                .ForMember(b => b.Type, opt => opt.MapFrom(_ => UnitTypeEnum.formula));
        }
    }
}
