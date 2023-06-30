using Domain.Entities;
using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;
using System.Linq.Expressions;

namespace Domain.Services
{
    public interface IPolizaService
    {
        Task<Poliza> CrearPoliza(Poliza poliza);
        Task<IEnumerable<Poliza>> ObtenerPoliza(PolizaFilter filtro);
    }

    public class PolizaService : IPolizaService
    {
        private readonly IPolizaRepository _polizaRepository;

        public PolizaService(IPolizaRepository polizaRepository)
        {
            _polizaRepository = polizaRepository;
        }

        public async Task<Poliza> CrearPoliza(Poliza poliza)
        {
            await ValidatePoliza(poliza);
            poliza.FechaPoliza = DateTime.Now;

            return await _polizaRepository.CreateAsync(poliza);
        }

        public Task<IEnumerable<Poliza>> ObtenerPoliza(PolizaFilter filtro)
            => _polizaRepository.FindAsync(GetFilter(filtro));

        private Expression<Func<Poliza, bool>> GetFilter(PolizaFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.NumeroPoliza))
                return m => m.NumeroPoliza == filter.NumeroPoliza;
            else
                return m => m.Placa == filter.Placa;
        }

        private async Task ValidatePoliza(Poliza poliza)
        {
            var count = await _polizaRepository.CountAsync(m => m.NumeroPoliza == poliza.NumeroPoliza);
            if (count > 0)
            {
                throw new BadRequestException($"Ya existe la póliza número ({poliza.NumeroPoliza}).");
            }

            count = await _polizaRepository.CountAsync(m => m.Placa == poliza.Placa
                && (
                    (poliza.FechaInicio >= m.FechaInicio && poliza.FechaInicio <= m.FechaFin) ||
                    (poliza.FechaFin >= m.FechaInicio && poliza.FechaFin <= m.FechaFin) ||
                    (poliza.FechaInicio <= m.FechaInicio && poliza.FechaFin >= m.FechaFin)
                )
            );
            if (count > 0)
            {
                throw new BadRequestException($"Ya existe una póliza para la placa ({poliza.Placa}) que entra en conflicto con el rango de fechas seleccionadas.");
            }

            if (poliza.FechaFin.Date < DateTime.Now.Date)
            {
                throw new BadRequestException($"La póliza ({poliza.NumeroPoliza}) no se encuentra vigente.");
            }

            if (poliza.FechaInicio >= poliza.FechaFin)
            {
                throw new BadRequestException($"Las fechas de inicio y fin de la póliza no corresponden a fechas válidas.");
            }
        }
    }
}
