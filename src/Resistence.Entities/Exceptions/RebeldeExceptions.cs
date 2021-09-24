using System;

namespace Resistence.Entities.Exceptions
{

    public class RebeldeNaoEncontradoException : Exception
    {
        public RebeldeNaoEncontradoException() {}
        public RebeldeNaoEncontradoException(int codigo) : base($"Rebelde de código: {codigo} não encontrado", null)
        {
        }
    }

}