using System.Threading.Tasks;

namespace NGColombia.Api.Service
{
    public interface IRecaptchaTokenValidator
    {
        Task<bool> IsValidToken(string token);
    }
}