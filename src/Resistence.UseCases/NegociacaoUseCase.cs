using System.Threading.Tasks;
using Resistence.Entities;
using Resistence.Entities.DTOs;
using Resistence.Entities.Exceptions;
using Resistence.Interfaces.UseCases.Interfaces;
using Resistence.Middleware;
using System.Linq;

namespace Resistence.UseCases
{
    public class NegociacaoUseCase : INegociacaoUseCase
    {
        private readonly EFContext _context; 

        public NegociacaoUseCase(EFContext context) {
            _context = context;
        }
        public async Task<bool> Negociar(NegociacaoDto negociacao)
        {
            Rebelde rebeldeUm = await _context.Rebeldes.FindAsync(negociacao.codigoRebeldeUm);
            Rebelde rebeldeDois = await _context.Rebeldes.FindAsync(negociacao.codigoRebeldeDois);

            if (rebeldeUm == null || rebeldeDois == null)
            {
                throw new RebeldeNaoEncontradoException();
            }

            VerificarRebeldesTraidores(rebeldeUm, rebeldeDois);
            VerificarValorDosItensNegociacao(negociacao);
            VerificarConsistenciaItensNegociacaoComInventarioReal(negociacao, rebeldeUm, rebeldeDois);

            var inventarioUm = rebeldeUm.Inventario;
            var inventarioDois = rebeldeDois.Inventario;

            foreach(var item in negociacao.itensDeTrocaRebeldeUm) {
                rebeldeUm.removerOuReduzirItemInventario(item.Key, item.Value);
                rebeldeDois.adicionarOuIncrementarItemInventario(item.Key, item.Value);
            }

            foreach(var item in negociacao.itensDeTrocaRebeldeDois) {
                rebeldeDois.removerOuReduzirItemInventario(item.Key, item.Value);
                rebeldeUm.adicionarOuIncrementarItemInventario(item.Key, item.Value);
            }

            _context.Update(rebeldeUm);
            _context.Update(rebeldeDois);

            await _context.SaveChangesAsync();

            return true;
        }

        private static void VerificarConsistenciaItensNegociacaoComInventarioReal(NegociacaoDto negociacao, Rebelde rebeldeUm, Rebelde rebeldeDois)
        {
            foreach (var item in negociacao.itensDeTrocaRebeldeUm)
            {
                if (!rebeldeUm.Inventario.ContainsKey((ITEM_INVENTARIO)item.Key) || rebeldeUm.Inventario[(ITEM_INVENTARIO)item.Key] < item.Value)
                {
                    throw new InconsistenciaDeItensInventarioException();
                }
            }

            foreach (var item in negociacao.itensDeTrocaRebeldeDois)
            {
                if (!rebeldeDois.Inventario.ContainsKey((ITEM_INVENTARIO)item.Key) || rebeldeDois.Inventario[(ITEM_INVENTARIO)item.Key] < item.Value)
                {
                    throw new InconsistenciaDeItensInventarioException();
                }
            }
        }

        private static void VerificarValorDosItensNegociacao(NegociacaoDto negociacao)
        {
            int quantidadePontosItensRebeldeUm = negociacao.itensDeTrocaRebeldeUm
                            .Select(item => buscarValorItemInventario(item.Key) * item.Value)
                            .Sum();

            int quantidadePontosItensRebeldeDois = negociacao.itensDeTrocaRebeldeDois
                .Select(item => buscarValorItemInventario(item.Key) * item.Value)
                .Sum();

            if (quantidadePontosItensRebeldeUm != quantidadePontosItensRebeldeDois)
            {
                throw new SomaDosItensNaoCompativelException();
            }
        }

        private static int buscarValorItemInventario(ITEM_INVENTARIODTO item) {
            switch(item) {
                case ITEM_INVENTARIODTO.COMIDA: {
                    return 1;
                }
                case ITEM_INVENTARIODTO.AGUA: {
                    return 2;
                }
                case ITEM_INVENTARIODTO.MUNICAO: {
                    return 3;
                }
                case ITEM_INVENTARIODTO.ARMA: {
                    return 4;
                }
                default: {
                    throw new ItemInventarioNaoEncontradoException();
                }
            }
        }
    
        private void VerificarRebeldesTraidores(Rebelde um, Rebelde dois) {
            if(um.Traidor) {
                throw new RebeldeTraidorException(um.Id);
            }

            if(dois.Traidor) {
                throw new RebeldeTraidorException(dois.Id);
            }
        }
    }
}