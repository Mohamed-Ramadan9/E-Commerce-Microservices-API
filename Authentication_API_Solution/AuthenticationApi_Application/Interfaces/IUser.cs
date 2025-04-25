using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationApi_Application.DTOs;
using E_Commerce.SharedLibrary.Responses;

namespace AuthenticationApi_Application.Interfaces
{
    public interface IUser
    {
        Task<Response> Regsiter(AppUserDTO appUserDTO);
        Task<Response> Login( LoginDTO loginDTO);

        Task<GetUserDTO> GetUser(int userId);
    }
}
