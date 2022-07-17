using AutoMapper;
using UnitOfMeasures.Application.Models.DTO.FormulaUnitDTOs;
using UnitOfMeasures.Domain.Models;

namespace UnitOfMeasures.Infrastructure.Mapper
{
    public class FormulaUnitProfile : Profile
    {
        public FormulaUnitProfile()
        {
            CreateMap<CreateFormulaUnitDTO, FormulaUnit>().ForMember(b => b.BaseMeasuremenID, opt => opt.MapFrom(c => c.BaseMeasurementUnitDetailDTO.ID));
        }
    }
}
