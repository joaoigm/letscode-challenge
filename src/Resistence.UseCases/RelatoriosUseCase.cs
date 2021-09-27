using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Resistence.Entities;
using Resistence.Entities.Results;
using Resistence.Interfaces.UseCases.Interfaces;
using Resistence.Middleware;

namespace Resistence.UseCases
{
    public class RelatoriosUseCase : BaseUseCase, IRelatoriosUseCase {

        private readonly EFContext context;

        public RelatoriosUseCase(EFContext context): base(context) {
            this.context = context;
        }

        public async Task<PercentualQuantidadeItensPorRebeldeResult> RelatorioMediaItensPorRebelde()
        {
            var resultado = new PercentualQuantidadeItensPorRebeldeResult();

            var inventarios = await context.Rebeldes
                .Where(r => !r.Traidor)
                .Select(r => r.Inventario)
                .ToListAsync();

            var quantidadeTotalArmas = inventarios.Where(i => i.ContainsKey(Entities.ITEM_INVENTARIO.ARMA)).Select(i => i[Entities.ITEM_INVENTARIO.ARMA]).Sum();
            var quantidadeRebeldesComArma = inventarios.Where(i => i.ContainsKey(Entities.ITEM_INVENTARIO.ARMA)).Count();

            resultado.Arma = quantidadeTotalArmas / quantidadeRebeldesComArma;


            var quantidadeTotalMunicao = inventarios.Where(i => i.ContainsKey(Entities.ITEM_INVENTARIO.MUNICAO)).Select(i => i[Entities.ITEM_INVENTARIO.MUNICAO]).Sum();
            var quantidadeRebeldesComMunicao = inventarios.Where(i => i.ContainsKey(Entities.ITEM_INVENTARIO.MUNICAO)).Count();

            resultado.Municao = quantidadeTotalMunicao / quantidadeRebeldesComMunicao;

            var quantidadeTotalAgua = inventarios.Where(i => i.ContainsKey(Entities.ITEM_INVENTARIO.AGUA)).Select(i => i[Entities.ITEM_INVENTARIO.AGUA]).Sum();
            var quantidadeRebeldesComAgua = inventarios.Where(i => i.ContainsKey(Entities.ITEM_INVENTARIO.AGUA)).Count();

            resultado.Agua = quantidadeTotalAgua / quantidadeRebeldesComAgua;

            var quantidadeTotalComida = inventarios.Where(i => i.ContainsKey(Entities.ITEM_INVENTARIO.COMIDA)).Select(i => i[Entities.ITEM_INVENTARIO.COMIDA]).Sum();
            var quantidadeRebeldesComComida = inventarios.Where(i => i.ContainsKey(Entities.ITEM_INVENTARIO.COMIDA)).Count();

            resultado.Comida = quantidadeTotalComida / quantidadeRebeldesComComida;

            return resultado;
        }

        public async Task<PercentualResult> RelatorioPercentualRebeldes()
        {
            var totalRebeldes = Convert.ToDecimal(await context.Rebeldes
                .Where(r => !r.Traidor)
                .CountAsync());

            var totalNaBase = Convert.ToDecimal(await context.Rebeldes.CountAsync());

           var result = new PercentualResult {
                Percentual = Math.Round(totalRebeldes/totalNaBase, 2)
            };

            return result;
        }

        public async Task<PercentualResult> RelatorioPercentualTraidores()
        {
            var totalTraidores = Convert.ToDecimal(await context.Rebeldes
                .Where(r => r.Traidor)
                .CountAsync());

            var totalNaBase = Convert.ToDecimal(await context.Rebeldes.CountAsync());

            return new PercentualResult {
                Percentual = Math.Round(totalTraidores / totalNaBase, 2)
            };
        }

        public async Task<QuantidadePontosPerdidosResult> RelatorioPontosPerdidosPorTraidores()
        {
            //O Comparer que criei não consegue ser convertido para query no banco
            var todosOsPontosNaGalaxia = (await context.Localizacoes.ToListAsync()).Distinct(new LocalizacaoPorNomeComparer());
            var pontosPerdidos = todosOsPontosNaGalaxia.Where(ponto => {
                var rebeldesNoPonto = context.Rebeldes
                    .Include(r => r.Localizacao)
                    .Where(r => r.Localizacao == ponto)
                    .ToList();
                var rebeldesTraidores = rebeldesNoPonto.Where(r => r.Traidor);

                return rebeldesNoPonto.Count == rebeldesTraidores.Count();
            });
            return new QuantidadePontosPerdidosResult {
                Quantidade = pontosPerdidos.Count()
            };
        }
    }
}