using EstoqueLab.Data.Contexts;
using EstoqueLab.Domain.Entities;
using EstoqueLab.Domain.Interfaces.Repositories;
using EstoqueLab.Uteis.Bases;

namespace EstoqueLab.Data.Repositories
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public ProdutoRepository(LabContext ctx) : base(ctx)
        {
        }
    }
}
