namespace AutoLab.Application.ViewModel
{
    public class ProdutoViewModel
    {
        public string Key { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public CategoriaViewModel Categoria { get; set; }
        public string CategoriaKey { get; set; }
        public Boolean Ativo { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }
    }
}
