using UnitOfMeasures.Application.Models.DTO;
using UnitOfMeasures.Application.Models.DTO.BaseMeasurementUnitDTOs;

namespace UnitOfMeasures.Application.Contracts
{
    public interface ICalculateService
    {
        string GetParentCode(string childCode);
        UnitsDTO GetUnitFromCode(string code);
        double ConvertUnits(string FromCode, string ToCode, double value);

        Task<List<BaseMeasurementUnitDetailDTO>?> GetMeasures();
        Task<List<UnitsDTO>> GetUnitsByMeasureName(string measureName);
        bool IsConvertValid(UnitsDTO unitsDTO1, UnitsDTO unitsDTO2);
    }
}
