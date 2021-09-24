using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Resistence.Entities.Exceptions;
using Resistence.Interfaces.UseCases.Interfaces;
using Resistence.Middleware;
using Resistence.UseCases;
using Xunit;

namespace Resistence.Tests
{
    public class UseCasesTests
    {
        private IRebeldesUseCase rebeldesUseCase;

        public UseCasesTests()
        {
            var connection = new SqliteConnection("DataSource=file:resistencedb?mode=memory&cache=shared");
            connection.Open();
            var dbOptions = new DbContextOptionsBuilder<EFContext>()
                .UseSqlite(connection)
                .Options;
            var context = new EFContext(dbOptions);
            context.Database.EnsureCreated();
            this.rebeldesUseCase = new RebeldesUseCase(context);

        }

        [Fact]
        public async Task AtualizarLocalizacao_WhenCalled_ShouldReturnAtualizarLocalizacaoResult_Object()
        {
            var result = await this.rebeldesUseCase.AtualizarLocalizacao(new Entities.DTOs.LocalizacaoDTO
            {
                Latitude = "123",
                Longitude = "456",
                Nome = "Planeta 1"
            }, 1);

            Assert.NotNull(result);
            Assert.True(result.Latitude == "123" && result.Longitude == "456" && result.Nome == "Planeta 1");
        }

        [Fact]
        public void AtualizarLocalizacao_WhenCalled_ShouldReturn_RebeldeNaoEncontradoException()
        {
           Assert.ThrowsAsync<RebeldeNaoEncontradoException>(() => this.rebeldesUseCase.AtualizarLocalizacao(new Entities.DTOs.LocalizacaoDTO
            {
                Latitude = "123",
                Longitude = "456",
                Nome = "Planeta 1"
            }, 999));
        }
    }
}
