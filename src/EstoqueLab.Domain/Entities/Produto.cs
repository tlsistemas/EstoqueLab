using EstoqueLab.Uteis.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
