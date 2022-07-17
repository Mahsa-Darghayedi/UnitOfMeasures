using AutoMapper;
using UnitOfMeasures.Application.Models.DTO.MeasurementDimensionDTOs;
using UnitOfMeasures.Domain.Models;

namespace UnitOfMeasures.Infrastructure.Mapper
{
    public class MeasurementDimensionProfile : Profile
    {
        public MeasurementDimensionProfile()
        {
            CreateMap<CreateMeasurementDimensionDTO, MeasurementDimension>();
            CreateMap<MeasurementDimension, MeasurementDimensionDetailDTO>();
        }

    }
}
