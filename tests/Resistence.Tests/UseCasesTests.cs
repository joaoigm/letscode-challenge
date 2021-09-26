using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Resistence.Entities.Exceptions;
using Resistence.Interfaces.UseCases.Interfaces;
using Resistence.Middleware;
using Resistence.UseCases;
using Xunit;
using Resistence.Entities.DTOs;
using System.Collections.Generic;

namespace Resistence.Tests
{
    public class UseCasesTests
    {
        private IRebeldesUseCase rebeldesUseCase;
        private IReportarUseCase reportarUseCase;
        private INegociacaoUseCase negociacaoUseCase;

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
            this.negociacaoUseCase = new NegociacaoUseCase(context);
        }

        [Fact]
        public async Task AtualizarLocalizacao_WhenCalled_ShouldReturnAtualizarLocalizacaoResult_Object()
        {
            var result = await this.rebeldesUseCase.AtualizarLocalizacao(new Entities.DTOs.LocalizacaoDto
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
            Assert.ThrowsAsync<RebeldeNaoEncontradoException>(() => this.rebeldesUseCase.AtualizarLocalizacao(new Entities.DTOs.LocalizacaoDto
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

            Assert.ThrowsAsync<RebeldeTraidorException>(() =>  this.rebeldesUseCase.AtualizarLocalizacao(new Entities.DTOs.LocalizacaoDto
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

           var result = await this.rebeldesUseCase.AtualizarLocalizacao(new Entities.DTOs.LocalizacaoDto
            {
                Latitude = "123",
                Longitude = "456",
                Nome = "Planeta 1"
            }, 3);

            Assert.NotNull(result);
            Assert.True(result.Latitude == "123" && result.Longitude == "456" && result.Nome == "Planeta 1");
        }

        [Fact]
        public void Negociar_WhellCalled_WithWrongItensSumValue_ShouldThrowSomaDosItensNaoCompativelException() {
            var inventarioUm = new Dictionary<ITEM_INVENTARIODTO, int>();
            var inventarioDois = new Dictionary<ITEM_INVENTARIODTO, int>();
            
            // Na soma vai dar 4 pontos
            inventarioUm.Add(ITEM_INVENTARIODTO.COMIDA, 4);

            // Na soma vai dar 2 pontos
            inventarioDois.Add(ITEM_INVENTARIODTO.AGUA, 1);
            
            var negociacaoDto = new NegociacaoDto() {
                codigoRebeldeUm = 1,
                codigoRebeldeDois = 2,
                itensDeTrocaRebeldeUm = inventarioUm,
                itensDeTrocaRebeldeDois = inventarioDois 
            };


            Assert.ThrowsAsync<SomaDosItensNaoCompativelException>(() => this.negociacaoUseCase.Negociar(negociacaoDto));
        }

        [Fact]
        public void Negociar_WhellCalled_WithWrongItensQuantityInBothRebelds_ShouldThrowInconsistenciaDeItensInventarioException() {
            var inventarioUm = new Dictionary<ITEM_INVENTARIODTO, int>();
            var inventarioDois = new Dictionary<ITEM_INVENTARIODTO, int>();
            
            // A soma de pontos vai dar certo mas a quantidade vai disparar a excecao de inconsistencia. Mais itens do que realmente tem
            inventarioUm.Add(ITEM_INVENTARIODTO.COMIDA, 100);

            inventarioDois.Add(ITEM_INVENTARIODTO.AGUA, 50);
            
            var negociacaoDto = new NegociacaoDto() {
                codigoRebeldeUm = 1,
                codigoRebeldeDois = 2,
                itensDeTrocaRebeldeUm = inventarioUm,
                itensDeTrocaRebeldeDois = inventarioDois 
            };


            Assert.ThrowsAsync<InconsistenciaDeItensInventarioException>(() => this.negociacaoUseCase.Negociar(negociacaoDto));
        }

        [Fact]
        public void Negociar_WhellCalled_WithWrongItensFromSecondRebel_ShouldThrowInconsistenciaDeItensInventarioException() {
            var inventarioUm = new Dictionary<ITEM_INVENTARIODTO, int>();
            var inventarioDois = new Dictionary<ITEM_INVENTARIODTO, int>();
            
            // A soma de pontos vai dar certo mas a quantidade vai disparar a excecao de inconsistencia. Mais itens do que realmente tem
            inventarioUm.Add(ITEM_INVENTARIODTO.AGUA, 4);

            inventarioDois.Add(ITEM_INVENTARIODTO.ARMA, 2);
            
            var negociacaoDto = new NegociacaoDto() {
                codigoRebeldeUm = 1,
                codigoRebeldeDois = 2,
                itensDeTrocaRebeldeUm = inventarioUm,
                itensDeTrocaRebeldeDois = inventarioDois 
            };


            Assert.ThrowsAsync<InconsistenciaDeItensInventarioException>(() => this.negociacaoUseCase.Negociar(negociacaoDto));
        }

        [Fact]
        public async Task Negociar_WhellCalled_WithWrongItensSumValue_ShouldReturnTrue() {
            var inventarioUm = new Dictionary<ITEM_INVENTARIODTO, int>();
            var inventarioDois = new Dictionary<ITEM_INVENTARIODTO, int>();
            
            // A soma de pontos vai dar certo e a quantidade também. Deve retornar true
            inventarioUm.Add(ITEM_INVENTARIODTO.COMIDA, 2);

            inventarioDois.Add(ITEM_INVENTARIODTO.AGUA, 1);
            
            var negociacaoDto = new NegociacaoDto() {
                codigoRebeldeUm = 1,
                codigoRebeldeDois = 2,
                itensDeTrocaRebeldeUm = inventarioUm,
                itensDeTrocaRebeldeDois = inventarioDois 
            };


            Assert.True(await this.negociacaoUseCase.Negociar(negociacaoDto));
        }
    }
}
