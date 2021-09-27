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
    public class RelatoriosTests
    {
        private IRebeldesUseCase rebeldesUseCase;
        private IReportarUseCase reportarUseCase;
        private INegociacaoUseCase negociacaoUseCase;
        private IRelatoriosUseCase relatoriosUseCase;

        public RelatoriosTests()
        {
            var connection = new SqliteConnection("DataSource=file:resistencedbRelatorios?mode=memory&cache=shared");
            connection.Open();
            var dbOptions = new DbContextOptionsBuilder<EFContext>()
                .UseSqlite(connection)
                .Options;
            var context = new EFContext(dbOptions);
            context.Database.EnsureCreated();
            this.rebeldesUseCase = new RebeldesUseCase(context);
            this.reportarUseCase = new ReportarUseCase(context);
            this.negociacaoUseCase = new NegociacaoUseCase(context);
            this.relatoriosUseCase = new RelatoriosUseCase(context);
        }

        [Fact]
        public async Task RelatorioPercentualRebeldes_WhenCalled_ShouldReturnFiftyPercent()
        {
            //Tornando metade dos rebeldes traidores
            for (int i = 0; i < 3; i++)
            {
                await reportarUseCase.Reportar(1);
                await reportarUseCase.Reportar(2);
            }
            var result = await relatoriosUseCase.RelatorioPercentualRebeldes();
            Assert.Equal(0.5M, result.Percentual);
        }

        [Fact]
        public async Task RelatorioPercentualTraidores_WhenCalled_ShouldReturnFiftyPercent()
        {
            //Tornando metade dos rebeldes traidores
            for (int i = 0; i < 3; i++)
            {
                await reportarUseCase.Reportar(1);
                await reportarUseCase.Reportar(2);
            }
            var result = await relatoriosUseCase.RelatorioPercentualTraidores();
            Assert.Equal(0.5M, result.Percentual);
        }

        [Fact]
        public async Task RelatorioPontosPerdidosPorTraidores_WhenCalled_ShoudlReturnOneLostPoint()
        {
            // Testes anteriores já tornaram dois pontos na galáxia como perdidos para os traidores
            var resultado = await relatoriosUseCase.RelatorioPontosPerdidosPorTraidores();
            Assert.True(resultado.Quantidade == 2);
        }
    }
}
