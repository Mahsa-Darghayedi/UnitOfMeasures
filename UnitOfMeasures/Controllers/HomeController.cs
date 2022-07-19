using Microsoft.AspNetCore.Mvc;
using UnitOfMeasures.Application.Contracts;
using UnitOfMeasures.Application.Models.DTO;
using UnitOfMeasures.Application.Models.DTO.BaseMeasurementUnitDTOs;
using UnitOfMeasures.Infrastructure.Persistents.DBContext;

namespace UnitOfMeasures.Controllers
{
    [Route(" Home")]
    public class HomeController : ControllerBase
    {
        //  MeasureDBContext _context;
        ICalculateService _calculateService;
        public HomeController(ICalculateService calculateService)
        {
            _calculateService = calculateService; 
        }


        //[Route("CalculateT")]
        //[HttpGet]
        //public string CalculateT()
        //{
        //    string value = "5.6+6+((7/(4+(3))-4.5)*2)-(4.1*6)*(5-6.2*(7*5+(4*2.2)))";
        ////    var result = _calculateService.CalculateFormula(value);
        //    return value;
        //}




        [Route("GetMeasures")]
        [HttpGet]
        public async Task<List<BaseMeasurementUnitDetailDTO>> GetMeasures()
        {
            return await _calculateService.GetMeasures();
        }

        [Route("GetUnitsByMeasureName")]
        [HttpGet]
       public async Task<List<UnitsDTO>> GetUnitsByMeasureName(string measureName)
        {
            return await _calculateService.GetUnitsByMeasureName(measureName);
        }
    }
}
