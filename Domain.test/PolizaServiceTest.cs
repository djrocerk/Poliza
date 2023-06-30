using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Services;
using Moq;
using NUnit.Framework;
using System.Linq.Expressions;

namespace Domain.test
{
    public class PolizaServiceTest
    {
        private IPolizaService _polizaService;
        private Mock<IPolizaRepository> _polizaRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _polizaRepositoryMock = new Mock<IPolizaRepository>();
            _polizaService = new PolizaService(_polizaRepositoryMock.Object);
        }

        [Test]
        public void CrearPolizaSuccessTest()
        {
            var poliza = NewPoliza();
            _polizaRepositoryMock.Setup(m => m.CountAsync(It.IsAny<Expression<Func<Poliza, bool>>>()).Result).Returns(0);
            _polizaRepositoryMock.Setup(m => m.CreateAsync(poliza).Result).Returns(poliza);

            var res = _polizaService.CrearPoliza(poliza).Result;

            Assert.That(poliza.Placa, Is.EqualTo(res.Placa));
        }

        [Test]
        public void CrearPolizaExistenteTest()
        {
            _polizaRepositoryMock.Setup(m => m.CountAsync(It.IsAny<Expression<Func<Poliza, bool>>>()).Result).Returns(1);

            var poliza = NewPoliza();
            Assert.ThrowsAsync<BadRequestException>(() => _polizaService.CrearPoliza(poliza));
        }

        [Test]
        public void CrearPolizaFechaErradaTest()
        {
            _polizaRepositoryMock.Setup(m => m.CountAsync(It.IsAny<Expression<Func<Poliza, bool>>>()).Result).Returns(0);

            var poliza = NewPoliza();
            poliza.FechaFin = DateTime.Now.AddDays(-1);
            poliza.FechaInicio = poliza.FechaFin.AddYears(-1).AddDays(-1);

            Assert.ThrowsAsync<BadRequestException>(() => _polizaService.CrearPoliza(poliza));
        }

        private Poliza NewPoliza()
        {
            return new Poliza
            {
                NumeroPoliza = "123",
                NombreCliente = "Roberto Cerquera",
                IdentificacionCliente = "123456789",
                FechaInicio = DateTime.Parse("2023-07-01"),
                FechaFin = DateTime.Parse("2023-07-01").AddYears(1).AddDays(-1),
                ValorMaximoPoliza = 5000000,
                NombrePlanPoliza = "SURA",
                CiudadCliente = "NEIVA",
                DireccionResidencia = "Cra 25",
                Placa = "JGR011",
                Modelo = "2017",
                TieneInspeccion = true
            };
        }
    }
}
