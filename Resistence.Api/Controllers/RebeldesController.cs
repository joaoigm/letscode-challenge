using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _rebeldesUseCase.BuscarRebeldes());
        }
    }
}