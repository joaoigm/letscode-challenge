using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Resistence.Entities.DTOs;
using Resistence.Interfaces.UseCases.Interfaces;

namespace Resistence.Api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class NegociacaoController : ControllerBase
    {
        private readonly INegociacaoUseCase _negociacao;

        public NegociacaoController(INegociacaoUseCase negociacao) {
            _negociacao = negociacao;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NegociacaoDto negociacao)
        {
            try {
                return Ok(await _negociacao.Negociar(negociacao));
            } catch(Exception ex) {
                return BadRequest(new {
                    Mensagem = ex.Message
                });
            }
        }

    }
}