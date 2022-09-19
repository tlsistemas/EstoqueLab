using EstoqueLab.Domain.Entities;
using EstoqueLab.Domain.Interfaces.Repositories;
using EstoqueLab.Domain.Interfaces.Services;
using EstoqueLab.Uteis.Bases;

namespace AutoLab.Domain.Services
{
    public class CategoriaService : ServiceBase<Categoria>, ICategoriaService
    {
        public readonly ICategoriaRepository _repository;

        public CategoriaService(ICategoriaRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}