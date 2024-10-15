using LibraryManagment.DTO.RequestDTO.AuthorRequest;
using LibraryManagment.DTO.RequestDTO.UserRequest;
using LibraryManagment.InterFace.IService.IUserServ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //Add user
        [HttpPost("AddUsers")]
        public async Task<IActionResult> AddUsers(UserRequestDTO request)
        {
            try
            {
                var data = await _userService.AddUsers(request);
                return Ok(data);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        //login
        [HttpPost("Cant_cook")]
        public async Task<IActionResult> Login(string NIC, string password)
        {
            try
            {
            var Data = await _userService.LoginUserAsync(NIC, password);
            return Ok(Data);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //UserLogin
        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin(UserLoginDTO login)
        {
            if(login == null)
            {
                throw new Exception("error");
            }
            try
            {
                var data = await _userService.UserLogin(login);
                return Ok(data);

            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
