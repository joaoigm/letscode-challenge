using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Resistence.Entities;
using Resistence.Entities.DTOs;
using Resistence.Entities.Exceptions;
using Resistence.Entities.Results;
using Resistence.Interfaces.UseCases.Interfaces;
using Resistence.Middleware;

namespace Resistence.UseCases
{
    public class RebeldesUseCase : IRebeldesUseCase
    {

        private readonly EFContext _context;

        public RebeldesUseCase(EFContext context)
        {
            _context = context;
        }

        public RebeldesUseCase() { }
        public async Task<AdicionarRebeldeResult> AdicionarRebelde(AdicionarRebeldeDto rebelde)
        {
            Rebelde newRebelde = new Rebelde(rebelde);

            await _context.Rebeldes.AddAsync(newRebelde);
            await _context.SaveChangesAsync();
            return new AdicionarRebeldeResult(newRebelde);
        }

        public async Task<AtualizarLocalizacaoResult> AtualizarLocalizacao(LocalizacaoDto novaLocalizacao, int codigoRebelde)
        {

            if (!await _context.Rebeldes.AnyAsync(r => r.Id == codigoRebelde))
            {
                throw new RebeldeNaoEncontradoException(codigoRebelde);
            }


            Rebelde rebelde = await _context.Rebeldes
            .AsTracking()
            .Include(r => r.Localizacao)
            .SingleAsync(re => re.Id.Equals(codigoRebelde));

            if(rebelde.Traidor) {
                throw new RebeldeTraidorException(codigoRebelde);
            }

            rebelde.Localizacao.Latitude = novaLocalizacao.Latitude;
            rebelde.Localizacao.Longitude = novaLocalizacao.Longitude;
            rebelde.Localizacao.Nome = novaLocalizacao.Nome;

            _context.Rebeldes.Update(rebelde);
            await _context.SaveChangesAsync();

            return new AtualizarLocalizacaoResult
            {
                Latitude = rebelde.Localizacao.Latitude,
                Longitude = rebelde.Localizacao.Longitude,
                Nome = rebelde.Localizacao.Nome
            };


        }
    }
}