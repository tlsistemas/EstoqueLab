using EstoqueLab.Domain.Entities;
using EstoqueLab.Domain.Interfaces.Repositories;
using EstoqueLab.Domain.Interfaces.Services;
using EstoqueLab.Uteis.Bases;

namespace AutoLab.Domain.Services
{
    public class ProdutoService : ServiceBase<Produto>, IProdutoService
    {
        public readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}