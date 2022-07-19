using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitOfMeasures.Application.Contracts;
using UnitOfMeasures.Application.Models.DTO;
using UnitOfMeasures.Application.Models.DTO.BaseMeasurementUnitDTOs;
using UnitOfMeasures.Application.Models.Enums;
using UnitOfMeasures.Infrastructure.Mapper;
using UnitOfMeasures.Infrastructure.Persistents.DBContext;
using Xunit;
namespace UnitOfMeasures_Test
{
    public class CalculateServiceTest
    {

        private readonly MeasureDBContext _context;
        private readonly ICalculateService _service;
        private IMapper _mapper;

        public CalculateServiceTest()
        {
            DbContextOptions<MeasureDBContext> dbContextOptions = new DbContextOptionsBuilder<MeasureDBContext>().UseSqlServer("Data Source=(local);Initial Catalog=MeasureDB33;Integrated Security=true").Options;
            _context = new MeasureDBContext(dbContextOptions);
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BaseMeasurementUnitProfile());
                cfg.AddProfile(new ChildUnitProfile());
                cfg.AddProfile(new CoefficientUnitProfile());
                cfg.AddProfile(new FormulaUnitProfile());
            });
            _mapper = mockMapper.CreateMapper();
            _service = new CalculateService(_context, _mapper);


        }

        [Fact]
        public void Service_Success()
        {
            Assert.NotNull(_context);
            Assert.NotNull(_service);
        }
        [Fact]
        public void GetMeasures_Success()
        {
            var result = _service.GetMeasures();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetUnitsByMeasureName_Failed_NullMeasureName()
        {
            await Assert.ThrowsAnyAsync<ArgumentNullException>(() => _service.GetUnitsByMeasureName(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("xc ")]
        [InlineData("123")]
        [InlineData("leng")]
        public async void GetUnitsByMeasureName_Failed_InvalidMeasureName(string measureName)
        {
            var result = await _service.GetUnitsByMeasureName(measureName);
            Assert.Null(result);
        }


        [Theory]
        [InlineData("Length")]
        [InlineData("Electric Current")]
        [InlineData("Mass")]
        [InlineData("Time")]
        [InlineData("Count")]
        [InlineData("Temperature")]
        [InlineData("length")]
        [InlineData("electric Current")]
        [InlineData("mass")]
        [InlineData("time")]
        [InlineData("count")]
        [InlineData("temperature")]
        public void GetUnitsByMeasureName_Success(string measureName)
        {
            var result = _service.GetUnitsByMeasureName(measureName);
            Assert.NotNull(result);
        }

        [Fact]
        public void ConvertUnits_Failed_NullParameters()
        {
            Assert.Throws<ArgumentNullException>(() => _service.ConvertUnits(null, null, double.NaN));
        }

        [Fact]
        public void ConvertUnits_Failed_InvalidValue()
        {
            Assert.Throws<ArgumentException>(() => _service.ConvertUnits("", "", double.NaN));
            Assert.Throws<ArgumentException>(() => _service.ConvertUnits("", "", -1));
        }
        [Fact]
        public void ConvertUnits_Failed_InvalidUnitType_Empty()
        {
            Assert.Throws<InvalidOperationException>(() => _service.ConvertUnits("xx", "", 1));
        }

        [Fact]
        public void ConvertUnits_Failed_ConvertFromBaseToBase()
        {
            Assert.Throws<InvalidCastException>(() => _service.IsConvertValid(new UnitsDTO() { Type = (UnitTypeEnum.baseMeasure) }, new UnitsDTO() { Type = UnitTypeEnum.baseMeasure }));
        }


        [Fact]
        public void ConvertUnits_Failed_ConvertFromDiffrentUnits()
        {
            Assert.Throws<InvalidCastException>(() => _service.IsConvertValid(new UnitsDTO() { ID = 1, Type = (UnitTypeEnum.coefficient) }, new UnitsDTO() { ID = 4, Type = UnitTypeEnum.coefficient }));
        }

        [Fact]
        public void ConvertUnits_Success_ConvertFromValidUnits()
        {
            Assert.True(_service.IsConvertValid(new UnitsDTO() { ID = 1, Type = (UnitTypeEnum.baseMeasure) }, new UnitsDTO() { ID = 2, Type = UnitTypeEnum.coefficient }));
            Assert.True(_service.IsConvertValid(new UnitsDTO() { ID = 1, Type = (UnitTypeEnum.coefficient) }, new UnitsDTO() { ID = 2, Type = UnitTypeEnum.coefficient }));
            Assert.True(_service.IsConvertValid(new UnitsDTO() { ID = 1, Type = (UnitTypeEnum.coefficient) }, new UnitsDTO() { ID = 1, Type = UnitTypeEnum.baseMeasure }));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void GetParentCode_Failed_NullOrEmptyCode(string childCode)
        {
            Assert.Throws<ArgumentNullException>(() => _service.GetParentCode(childCode));
        }
        //
        [Theory]
        [InlineData("xx")]
        [InlineData("cmmmm23")]
        [InlineData("123")]
        public void GetParentCode_Failed_InvalidCode(string childCode)
        {
            Assert.Throws<InvalidOperationException>(() => _service.GetParentCode(childCode));
        }

        [Theory]
        [InlineData("mm")]
        [InlineData("cm")]
        [InlineData("km")]
        [InlineData("mg")]
        [InlineData("kg")]
        [InlineData("ton")]
        [InlineData("K")]
        [InlineData("F")]
        public void GetParentCode_Success(string childCode)
        {
            List<string> vs = new List<string>() { "m", "A", "g", "S", "E", "C" };
            var result = _service.GetParentCode(childCode);
            Assert.True(vs.Contains(result));
        }


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void GetUnitFromCode_Failed_NullOrEmptyCode(string code)
        {
            Assert.Throws<ArgumentNullException>(() => _service.GetUnitFromCode(code));
        }

        [Theory]
        [InlineData("x")]
        [InlineData("22322")]
        [InlineData("KMMM")]
        public void GetUnitFromCode_Failed_InvalidCode(string code)
        {
            Assert.Throws<InvalidOperationException>(() => _service.GetUnitFromCode(code));
        }

        [Theory]
        [InlineData("m")]
        public void GetUnitFromCode_Success_ValidCode(string code)
        {
            UnitsDTO validResult = new UnitsDTO() { ID = 1, Code = "m", Name = "Meter", Type = UnitTypeEnum.baseMeasure };
            var result = _service.GetUnitFromCode(code);
            Assert.Equal(validResult.ID, result.ID);
            Assert.Equal(validResult.Code, result.Code);
            Assert.Equal(validResult.Name, result.Name);
            Assert.Equal(validResult.Type, result.Type);
        }

        [Fact]
        public void ConvertUnits_Success_ConvertBaseToCoefficient()
        {
            var result = _service.ConvertUnits("m", "cm", 10);
            Assert.Equal(Math.Round(result,2), 1000);


            result = _service.ConvertUnits("cm", "km", 10);
            Assert.Equal(result, 0.0001);
        }

        [Fact]
        public void ConvertUnits_Success_ConvertFormula()
        {
            var result = _service.ConvertUnits("c", "k", 10);
            Assert.Equal(result, 283.15);

             result = _service.ConvertUnits("c", "f", 10);
            Assert.Equal(Math.Round(result, 2), 50);

             result = _service.ConvertUnits("k", "c", 10);
            Assert.Equal(Math.Round(result, 2), -263.15);

            result = _service.ConvertUnits("f", "c", 10);
            Assert.Equal(Math.Round(result, 2), -12.22);

            result = _service.ConvertUnits("f", "k", 10);
            Assert.Equal(Math.Round(result, 2), 260.93);

        }
    }
}
