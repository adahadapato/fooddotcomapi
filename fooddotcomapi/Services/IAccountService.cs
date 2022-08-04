using fooddotcomapi.Authorization;
using fooddotcomapi.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fooddotcomapi.Services
{
    public interface IAccountService
    {
        Task<(bool IsSuccess, IEnumerable<ApplicationUser> result, string ErrorMessage)> Get();
        Task<(bool IsSuccess, ApplicationUser result, string ErrorMessage)> Get(string email);
        Task<(bool IsSuccess, string ErrorMessage)> Register(RegisterDto model);
        Task<(bool IsSuccess, string ErrorMessage)> UpdateName(UpdateNameDto model);
        Task<(bool IsSuccess, string ErrorMessage)> UpdatePhone(UpdatePhoneDto model);
        Task<(bool IsSuccess, string ErrorMessage)> ConfirmEmail(string token, string email);
        Task<(bool IsSuccess, string ErrorMessage)> ForgotPassword(string email);
        Task<(bool IsSuccess, string ErrorMessage)> ResetPassword(string email, string code, string password);
        Task<(bool IsSuccess, TokenResponseDto result, string ErrorMessage)> Login(LoginDto model);
        Task<(bool IsSuccess, string ErrorMessage)> Delete(string email);
    }
}
