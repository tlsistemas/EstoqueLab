namespace EstoqueLab.UI.Models
{
    public class Produto
    {
        public string Key { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public Categoria Categoria { get; set; } = new Categoria();
        public string CategoriaKey { get; set; } = string.Empty;
        public Boolean Ativo { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }
    }
}
