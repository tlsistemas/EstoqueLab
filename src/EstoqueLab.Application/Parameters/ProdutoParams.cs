using EstoqueLab.Domain.Entities;
using EstoqueLab.Uteis.Bases;
using EstoqueLab.Uteis.Delegates;
using System.Linq.Expressions;

namespace EstoqueLab.Application.Parameters
{
    public class ProdutoParams : BaseParams<Produto>
    {
        public string Key { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public string CategoriaKey { get; set; }

        public override Expression<Func<Produto, bool>> Filter()
        {
            var predicate = PredicateBuilder.New<Produto>();


            if (!string.IsNullOrWhiteSpace(Key))
            {
                var aliasDevedor = new Produto { Key = Key };
                predicate = predicate.And(p => p.Id.Equals(aliasDevedor.Id));
            }

            if (Id.HasValue)
            {
                predicate = predicate.And(p => p.Id == Id);
            }

            if (!string.IsNullOrWhiteSpace(Nome))
            {
                predicate = predicate.And(p => p.Nome.Equals(Nome));
            }
            if (Valor > 0)
            {
                predicate = predicate.And(p => p.Valor.Equals(Valor));
            }
            if (!string.IsNullOrWhiteSpace(CategoriaKey))
            {
                var categoria = new Categoria { Key = CategoriaKey };
                predicate = predicate.And(p => p.CategoriaId.Equals(categoria.Id));
            }
            return predicate;
        }
    }
}
