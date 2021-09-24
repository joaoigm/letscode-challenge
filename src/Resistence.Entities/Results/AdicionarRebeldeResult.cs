namespace Resistence.Entities.Results
{
    public class AdicionarRebeldeResult {
        public string Nome { get; set; }
        public char Genero { get; set; }
        public int Idade { get; set; }
        

        public AdicionarRebeldeResult() {}
        public AdicionarRebeldeResult(Rebelde rebelde) {
            this.Nome = rebelde.Nome;
            this.Genero = rebelde.Genero;
            this.Idade = rebelde.Idade;
        }
    }
}