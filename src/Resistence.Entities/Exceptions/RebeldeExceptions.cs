using System;

namespace Resistence.Entities.Exceptions
{

    public class RebeldeNaoEncontradoException : Exception
    {
        public RebeldeNaoEncontradoException() : base("Rebelde(s) informado(s) não encontrado(s)") { }
        public RebeldeNaoEncontradoException(int codigo) : base($"Rebelde de código: {codigo} não encontrado", null)
        {
        }
    }

    public class RebeldeTraidorException : Exception
    {
        public RebeldeTraidorException(int codigo) : base($"O Rebelde {codigo} é um traidor!") { }

    }

    public class ItemInventarioNaoEncontradoException : Exception
    {
        public ItemInventarioNaoEncontradoException() : base("Tipo de item do inventário não encontrado") { }
    }

    public class SomaDosItensNaoCompativelException : Exception
    {
        public SomaDosItensNaoCompativelException() : base("Os itens passados pelos rebeldes não estão em equilíbrio de valor") { }
    }

    public class InconsistenciaDeItensInventarioException : Exception
    {
        public InconsistenciaDeItensInventarioException() : base("Os itens enviados para negociação não batem com o inventário dos rebeldes") { }
    }

    public class QuantidadeItemInventarioASerRemovidaMaiorQueQuantidadeAtualException : Exception
    {
        public QuantidadeItemInventarioASerRemovidaMaiorQueQuantidadeAtualException() : base("A quantidade que o rebelde está tentando remover é maior que a quantidade que ele tem atualmente do item do inventário") {}
    }

}