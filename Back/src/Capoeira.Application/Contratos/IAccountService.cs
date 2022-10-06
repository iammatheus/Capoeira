using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Capoeira.Application.Dtos;

namespace Capoeira.Application.Contratos
{
    public interface IAccountService
    {
        Task<bool> UserExists(string userName);
        Task<UserUpdateDto> GetUserByUserNameAsync(string userName);
        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password);
        Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto);
    }
}