using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Resistence.Api.Controllers;
using Resistence.Tests.Fakes;
using Resistence.Interfaces.UseCases.Interfaces;
using Xunit;

namespace Resistence.Tests
{
    public class RebeldesControllerTest
    {
        private RebeldesController controller;
        private IRebeldesUseCase rebeldesUseCase;
        public RebeldesControllerTest() {
            this.rebeldesUseCase = new RebeldesUseCaseFake();
            this.controller = new RebeldesController(this.rebeldesUseCase);
        }

        [Fact]
        public async Task Post_WhenCalled_ReturnOkObjectResult() 
        {
            var result = await controller.Post(new Entities.DTOs.AdicionarRebeldeDTO {
                Genero = 'O',
                Idade = 25,
                Nome = "Paul"
            }) as OkObjectResult;
            Assert.IsType<OkObjectResult>(result);
            Assert.True(result.StatusCode == 200);
        }

        // [Fact]
        // public void Post
    }
}
