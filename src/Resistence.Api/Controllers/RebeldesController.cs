using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Resistence.Entities.DTOs;
using Resistence.Interfaces.UseCases.Interfaces;

namespace Resistence.Api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class RebeldesController : ControllerBase
    {
        private readonly IRebeldesUseCase _rebeldesUseCase;

        public RebeldesController(IRebeldesUseCase rebeldesUseCase)
        {
            _rebeldesUseCase = rebeldesUseCase;
        }
        [HttpPost]
        public async Task<IActionResult> Post(AdicionarRebeldeDto rebelde)
        {
            try
            {

                return Ok(await _rebeldesUseCase.AdicionarRebelde(rebelde));
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Mensagem = ex.Message
                });
            }
        }

        [HttpPut("{codigo}/localizacao")]
        public async Task<IActionResult> UpdateLocation(
            [FromBody] LocalizacaoDto localizacao,
            [FromRoute] int codigo)
        {
            try
            {

                return Ok(await _rebeldesUseCase.AtualizarLocalizacao(localizacao, codigo));
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Mensagem = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Todos() {
            return Ok(await _rebeldesUseCase.Todos());
        }
    }
}