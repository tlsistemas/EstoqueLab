using AutoLab.Application.ViewModel;
using AutoMapper;
using EstoqueLab.Domain.Entities;

namespace EstoqueLab.Application.AutoMapper
{
    public class ViewModelToModel : Profile
    {
        public ViewModelToModel()
        {
            CreateMap<CategoriaViewModel, Categoria>();
            CreateMap<ProdutoViewModel, Produto>();
        }
    }
}
