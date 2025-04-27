using AuthenticationApi_Application.DTOs;
using AuthenticationApi_Application.Interfaces;
using AuthenticationApi_Domain.Entites;
using E_Commerce.SharedLibrary.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApi_Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController(IUser user_repo) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<Response>> Register(AppUserDTO appUser)
        {
            if(ModelState.IsValid)
            {
                var result = await user_repo.Regsiter(appUser);
                return result.Flag ? Ok(result) : BadRequest(result);

            } 
            return BadRequest(ModelState);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<Response>> Login(LoginDTO loginUserDTO)
        {
            if(ModelState.IsValid)
            {
                var result = await user_repo.Login(loginUserDTO);
                return result.Flag? Ok(result) : BadRequest(result);

            } 
            return BadRequest(ModelState);
        }
        
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult<GetUserDTO>> GetUser(int id)
        {
            if (id == 0) return BadRequest("Invalid user id");

            var user= await user_repo.GetUser(id);
                return user.Id >0 ? Ok(user) : NotFound(user);

        }
    }
}
