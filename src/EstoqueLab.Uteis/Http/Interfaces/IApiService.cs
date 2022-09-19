using EstoqueLab.Uteis.Http.Response;

namespace EstoqueLab.Uteis.Interfaces
{
    public interface IApiService
    {
        BaseResponse MsgError(string BaseResponse = "");
        Task<string> GetRestAsync(string url);
        Task<string> GetClientAsync(string url);
    }
}
