using EstoqueLab.Data.Contexts;
using EstoqueLab.Domain.Entities;
using EstoqueLab.Domain.Interfaces.Repositories;
using EstoqueLab.Uteis.Bases;

namespace EstoqueLab.Data.Repositories
{
    public class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(LabContext ctx) : base(ctx)
        {
        }
    }
}
