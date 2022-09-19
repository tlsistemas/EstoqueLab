using AutoLab.Domain.Services;
using EstoqueLab.Application.Application;
using EstoqueLab.Application.AutoMapper;
using EstoqueLab.Application.Interfaces;
using EstoqueLab.Data.Repositories;
using EstoqueLab.Domain.Interfaces.Repositories;
using EstoqueLab.Domain.Interfaces.Services;
using EstoqueLab.Uteis.Bases;
using EstoqueLab.Uteis.Bases.Interface;
using EstoqueLab.Uteis.Http;
using EstoqueLab.Uteis.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EstoqueLab.Application
{
    public class DependenciesInjector
    {
        public static void Register(IServiceCollection svcCollection)
        {
            svcCollection.AddAutoMapper(typeof(ModelToViewModel), typeof(ViewModelToModel));

            #region Api
            svcCollection.AddScoped<IApiService, ApiService>();
            #endregion

            #region Application
            svcCollection.AddScoped<ICategoriaApplication, CategoriaApplication>();
            svcCollection.AddScoped<IProdutoApplication, ProdutoApplication>();
            #endregion

            #region Domain
            svcCollection.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
            svcCollection.AddScoped(typeof(ICategoriaService), typeof(CategoriaService));
            svcCollection.AddScoped(typeof(IProdutoService), typeof(ProdutoService));
            #endregion

            #region Repository
            svcCollection.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            svcCollection.AddScoped(typeof(ICategoriaRepository), typeof(CategoriaRepository));
            svcCollection.AddScoped(typeof(IProdutoRepository), typeof(ProdutoRepository));

            #endregion

        }
    }
}
