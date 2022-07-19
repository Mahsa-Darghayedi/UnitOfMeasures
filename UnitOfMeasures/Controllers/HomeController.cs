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


        [Route("ConvertUnits")]
        [HttpPost]
        public double ConvertUnits(ConvertDTO dto)
        {
            return _calculateService.ConvertUnits(dto.FromCode, dto.ToCode, dto.Value);
        }
    }
}
