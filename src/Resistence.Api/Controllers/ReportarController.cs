using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Resistence.Entities.DTOs;
using Resistence.Interfaces.UseCases.Interfaces;

namespace Resistence.Api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ReportarController : ControllerBase
    {
        private readonly IReportarUseCase _reportar;

        public ReportarController(IReportarUseCase reportar) {
            _reportar = reportar;
        }
        [HttpPost("{codigo}")]
        public async Task<IActionResult> Post([FromRoute] int codigo)
        {
            try {
                return Ok(await _reportar.Reportar(codigo));
            } catch(Exception ex) {
                return BadRequest(new {
                    Mensagem = ex.Message
                });
            }
        }

    }
}