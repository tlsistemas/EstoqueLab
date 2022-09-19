using AutoLab.Application.ViewModel;
using EstoqueLab.Application.Parameters;
using EstoqueLab.Uteis.Http.Response;

namespace EstoqueLab.Application.Interfaces
{
    public interface ICategoriaApplication
    {
        Task<BaseResponse<IEnumerable<CategoriaViewModel>>> ListCategoriaAsync(CategoriaParams paran);
        Task<BaseResponse<IEnumerable<CategoriaViewModel>>> ListCategoriaAsync();
        Task<BaseResponse<CategoriaViewModel>> Create(CategoriaViewModel paranObj);
        Task<BaseResponse<CategoriaViewModel>> Update(CategoriaViewModel paranObj);
        Task<BaseResponse<CategoriaViewModel>> Remove(string key);
    }
}
