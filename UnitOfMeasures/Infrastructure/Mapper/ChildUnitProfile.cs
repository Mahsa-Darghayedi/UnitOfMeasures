using AutoMapper;
using UnitOfMeasures.Application.Models.DTO;
using UnitOfMeasures.Application.Models.DTO.ChildUnitDTO;
using UnitOfMeasures.Domain.Models;

namespace UnitOfMeasures.Infrastructure.Mapper
{
    public class ChildUnitProfile :Profile
    {
        public ChildUnitProfile()
        {
            CreateMap<CreateChildUnitDTO, ChildUnit>().ForMember(b=> b.BaseMeasuremenID, opt=> opt.MapFrom(c=> c.BaseMeasurementUnitDetailDTO.ID));
            CreateMap<ChildUnit, CreateChildUnitDTO>();
            CreateMap<ChildUnit, UnitsDTO>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Code, opt => opt.MapFrom(x => x.Code));
        }
    }
}
