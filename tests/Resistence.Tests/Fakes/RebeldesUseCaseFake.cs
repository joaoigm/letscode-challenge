using System.Threading.Tasks;
using Resistence.Entities.DTOs;
using Resistence.Entities.Results;
using Resistence.Interfaces.UseCases.Interfaces;

namespace Resistence.Tests.Fakes
{
    public class RebeldesUseCaseFake : IRebeldesUseCase
    {
        public async Task<AdicionarRebeldeResult> AdicionarRebelde(AdicionarRebeldeDto rebelde) => new AdicionarRebeldeResult
        {
            Genero = rebelde.Genero,
            Idade = rebelde.Idade,
            Nome = rebelde.Nome
        };


        public async Task<AtualizarLocalizacaoResult> AtualizarLocalizacao(LocalizacaoDto novaLocalizacao, int codigoRebelde) => new AtualizarLocalizacaoResult {
            Latitude = novaLocalizacao.Latitude,
            Longitude = novaLocalizacao.Longitude,
            Nome = novaLocalizacao.Nome
        };
    }
}