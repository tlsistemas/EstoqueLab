using EstoqueLab.Domain.Entities;
using EstoqueLab.Uteis.Bases;
using EstoqueLab.Uteis.Delegates;
using System.Linq.Expressions;

namespace EstoqueLab.Application.Parameters
{
    public class CategoriaParams : BaseParams<Categoria>
    {
        public string Key { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;

        public override Expression<Func<Categoria, bool>> Filter()
        {
            var predicate = PredicateBuilder.New<Categoria>();


            if (!string.IsNullOrWhiteSpace(Key))
            {
                var aliasDevedor = new Categoria { Key = Key };
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
            return predicate;
        }
    }
}
