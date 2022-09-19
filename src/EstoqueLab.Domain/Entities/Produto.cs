using EstoqueLab.Uteis.Bases;

namespace EstoqueLab.Domain.Entities
{
    public class Produto : EntityBase
    {
        public  string Nome { get; set; }
        public decimal Valor { get; set; }
        public Categoria Categoria { get; set; }    
        public int CategoriaId { get; set; }
    }
}
