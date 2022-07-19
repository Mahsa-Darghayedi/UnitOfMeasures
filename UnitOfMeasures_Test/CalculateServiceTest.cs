using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitOfMeasures.Application.Contracts;
using UnitOfMeasures.Application.Models.DTO.BaseMeasurementUnitDTOs;
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
            await Assert.ThrowsAnyAsync<ArgumentNullException>( () =>_service.GetUnitsByMeasureName(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("xc ")]
        [InlineData("123")]
        public async void GetUnitsByMeasureName_Failed_InvalidMeasureName(string measureName)
        {
            var result =await _service.GetUnitsByMeasureName(measureName);
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
        public  void GetUnitsByMeasureName_Success(string measureName)
        {
            var result =  _service.GetUnitsByMeasureName(measureName);
            Assert.NotNull(result);
        }

    }
}
