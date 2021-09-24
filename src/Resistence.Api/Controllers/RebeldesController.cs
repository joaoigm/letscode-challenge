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

        public RebeldesController(IRebeldesUseCase rebeldesUseCase) {
            _rebeldesUseCase = rebeldesUseCase;
        }
        [HttpPost]
        public async Task<IActionResult> Post(AdicionarRebeldeDto rebelde)
        {
            return Ok(await _rebeldesUseCase.AdicionarRebelde(rebelde));
        }

        [HttpPut("/{codigo}/localizacao")]
        public async Task<IActionResult> UpdateLocation(
            [FromBody] LocalizacaoDto localizacao,
            [FromRoute]int codigo) {
            return Ok(await _rebeldesUseCase.AtualizarLocalizacao(localizacao, codigo));
        }
    }
}