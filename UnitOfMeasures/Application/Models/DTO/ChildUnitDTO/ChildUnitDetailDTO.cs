using UnitOfMeasures.Application.Models.DTO.BaseMeasurementUnitDTOs;

namespace UnitOfMeasures.Application.Models.DTO.ChildUnitDTO
{
    public class ChildUnitDetailDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public BaseMeasurementUnitDetailDTO BaseMeasurementUnitDetailDTO { get; set; }
    }
}
