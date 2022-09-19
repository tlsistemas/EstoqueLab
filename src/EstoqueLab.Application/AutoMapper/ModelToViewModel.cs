using AutoLab.Application.ViewModel;
using AutoMapper;
using EstoqueLab.Domain.Entities;

namespace EstoqueLab.Application.AutoMapper
{
    public class ModelToViewModel : Profile
    {
        public ModelToViewModel()
        {
            CreateMap<Categoria, CategoriaViewModel>();
            CreateMap<Produto, ProdutoViewModel>();
        }
    }
}
