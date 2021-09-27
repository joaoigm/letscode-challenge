using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Resistence.Entities.DTOs;
using Resistence.Interfaces.UseCases.Interfaces;

namespace Resistence.Api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class RelatoriosController : ControllerBase
    {
        private readonly IRelatoriosUseCase _relatorios;

        public RelatoriosController(IRelatoriosUseCase relatorios)
        {
            _relatorios = relatorios;
        }
        [HttpGet("percentual/rebeldes")]
        public async Task<IActionResult> PrecentualRebeldes()
        {
            try
            {
                return Ok(await _relatorios.RelatorioPercentualRebeldes());
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Mensagem = ex.Message
                });
            }
        }

        [HttpGet("percentual/traidores")]
        public async Task<IActionResult> PrecentualTraidores()
        {
            try
            {
                return Ok(await _relatorios.RelatorioPercentualTraidores());
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Mensagem = ex.Message
                });
            }
        }

        [HttpGet("itens/rebeldes")]
        public async Task<IActionResult> ItensPorRebelde()
        {
            try {
                return Ok(await _relatorios.RelatorioMediaItensPorRebelde());
            } catch(Exception ex) {
                return BadRequest(new {
                    Mensagem = ex.Message
                });
            }
        }

        [HttpGet("pontos/perdidos")]
        public async Task<IActionResult> PontosPerdidos()
        {
            try {
                return Ok(await _relatorios.RelatorioPontosPerdidosPorTraidores());
            } catch(Exception ex) {
                return BadRequest(new {
                    Mensagem = ex.Message
                });
            }
        }
    }
}