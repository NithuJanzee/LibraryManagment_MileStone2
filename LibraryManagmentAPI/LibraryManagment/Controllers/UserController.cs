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

        //get user details with nic
        [HttpGet("GetUserDetailsUsingID")]
        public async Task<IActionResult> FindUserWithNic(string NIC)
        {
            try
            {
                var response = await _userService.FindUserWithNic(NIC);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //Get all Users
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var data = await _userService.GetAllUsers();
                return Ok(data);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //get user details with GUID
        [HttpGet("UserDetailsGUID")]
        public async Task<IActionResult> FindUserGUID(Guid ID)
        {
            try
            {
                var response = await _userService.FindUserGUID(ID);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        //Edit User
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UserEditRequestDTO userEditRequest)
        {
            if (userEditRequest == null)
            {
                return BadRequest("Invalid data.");
            }

            bool isUpdated = await _userService.UpdateUserAsync(userId, userEditRequest);

            if (isUpdated)
            {
                return Ok("User updated successfully.");
            }
            else
            {
                return NotFound("User not found or no fields to update.");
            }
        }

    }
}
