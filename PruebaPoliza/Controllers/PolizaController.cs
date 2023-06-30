using API.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Poliza controller
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PolizaController : ControllerBase
    {
        private readonly IPolizaService _polizaService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="polizaService"></param>
        /// <param name="mapper"></param>
        public PolizaController(IPolizaService polizaService, IMapper mapper)
        {
            _polizaService = polizaService;
            _mapper = mapper;
        }

        /// <summary>
        /// Crea una póliza
        /// </summary>
        /// <remarks>
        /// Ejemplo request:
        ///
        ///     POST /
        ///     {
        ///         "numeroPoliza": "123",
        ///         "nombreCliente": "Roberto Cerquera",
        ///         "identificacionCliente": "1081564878",
        ///         "fechaInicio": "2023-07-01",
        ///         "fechaFin": "2024-07-01",
        ///         "valorMaximoPoliza": 5000000,
        ///         "nombrePlanPoliza": "Sura",
        ///         "ciudadCliente": "NEIVA",
        ///         "direccionResidencia": "Calle 25h 4w 04",
        ///         "placa": "JGR011",
        ///         "modelo": "2017",
        ///         "tieneInspeccion": false
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<PolizaResponse>> CrearPoliza([FromBody] CrearPolizaReq data)
        {
            var res = await _polizaService.CrearPoliza(_mapper.Map<Poliza>(data));
            return Ok(_mapper.Map<PolizaResponse>(res));
        }

        /// <summary>
        /// Consultar póliza por placa o número de póliza
        /// </summary>
        /// <remarks>
        /// Ejemplo request:
        ///
        ///     GET /?poliza=123&placa=KUG19D
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PolizaResponse>>> Search([FromQuery] string? poliza, string? placa)
        {
            var res = await _polizaService.ObtenerPoliza(new PolizaFilter
            {
                NumeroPoliza = poliza,
                Placa = placa
            });

            if (res == null) throw new NotFoundException("Poliza no encontrada.");

            return Ok(_mapper.Map<IEnumerable<PolizaResponse>>(res));
        }
    }
}
