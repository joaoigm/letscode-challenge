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
        private IReportarUseCase reportarUseCase;

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
            this.reportarUseCase = new ReportarUseCase(context);
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

        [Fact]
        public void ReportarRebelde_WhenCalled_ThreeTimes_RebeldeDeveSeTornarTraidor()
        {
            for(int i = 0; i < 3; i++) {
                this.reportarUseCase.Reportar(2);
            }

            Assert.ThrowsAsync<RebeldeTraidorException>(() =>  this.rebeldesUseCase.AtualizarLocalizacao(new Entities.DTOs.LocalizacaoDTO
            {
                Latitude = "123",
                Longitude = "456",
                Nome = "Planeta 1"
            }, 2));
        }

        [Fact]
        public async Task ReportarRebelde_WhenCalled_TwoTimes_RebeldeNaoDeveSeTornarTraidor()
        {
            for(int i = 0; i < 2; i++) {
                await this.reportarUseCase.Reportar(3);
            }

           var result = await this.rebeldesUseCase.AtualizarLocalizacao(new Entities.DTOs.LocalizacaoDTO
            {
                Latitude = "123",
                Longitude = "456",
                Nome = "Planeta 1"
            }, 3);

            Assert.NotNull(result);
            Assert.True(result.Latitude == "123" && result.Longitude == "456" && result.Nome == "Planeta 1");
        }
    }
}
