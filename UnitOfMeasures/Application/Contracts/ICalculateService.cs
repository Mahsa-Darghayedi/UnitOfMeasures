using UnitOfMeasures.Application.Models.DTO;
using UnitOfMeasures.Application.Models.DTO.BaseMeasurementUnitDTOs;

namespace UnitOfMeasures.Application.Contracts
{
    public interface ICalculateService
    {
        // string CalculateFormula(string value);
        double ConvertUnits(ConvertDTO dto);

        Task<List<BaseMeasurementUnitDetailDTO>?> GetMeasures();
        Task<List<UnitsDTO>> GetUnitsByMeasureName(string measureName);
    }
}
