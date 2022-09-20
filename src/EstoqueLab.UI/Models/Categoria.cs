namespace EstoqueLab.UI.Models
{
    public class Categoria
    {
        public string Key { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public Boolean Ativo { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }
    }
}
