using System;

namespace Resistence.Entities.Exceptions
{

    public class RebeldeNaoEncontradoException : Exception
    {
        public RebeldeNaoEncontradoException(): base("Rebelde(s) informado(s) não encontrado(s)") {}
        public RebeldeNaoEncontradoException(int codigo) : base($"Rebelde de código: {codigo} não encontrado", null)
        {
        }
    }

    public class RebeldeTraidorException : Exception {
        public RebeldeTraidorException(int codigo): base($"O Rebelde {codigo} é um traidor!") {}

    }

}