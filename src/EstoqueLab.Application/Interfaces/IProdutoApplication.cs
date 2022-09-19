using AutoLab.Application.ViewModel;
using EstoqueLab.Application.Parameters;
using EstoqueLab.Uteis.Http.Response;

namespace EstoqueLab.Application.Interfaces
{
    public interface IProdutoApplication
    {
        Task<BaseResponse<IEnumerable<ProdutoViewModel>>> ListProdutoAsync(ProdutoParams paran);
        Task<BaseResponse<IEnumerable<ProdutoViewModel>>> ListProdutoAsync();
        Task<BaseResponse<ProdutoViewModel>> Create(ProdutoViewModel paranObj);
        Task<BaseResponse<ProdutoViewModel>> Update(ProdutoViewModel paranObj);
        Task<BaseResponse<ProdutoViewModel>> Remove(string key);
    }
}
