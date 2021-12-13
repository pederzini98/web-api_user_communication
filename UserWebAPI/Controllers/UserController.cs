using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UseModels.Entities;
using UseModels.Enums;
using UseModels.Services;
using UseModels.ViewModels;

namespace UserWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromServices] IUserService userService)
        {
            IList<User> users = await _userService.GetUsers();
            if (users == null)
            {
                return StatusCode(500);
            }
            IList<UserViewModel> userViewModel = users.Select(x => new UserViewModel(x)).ToList();

            return Ok(userViewModel);
        }

        [HttpGet("{id}", Name = "GetOneUser")]
        public async Task<IActionResult> GetOneUser(string id)
        {
            User user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            UserViewModel userViewModel = new(user);
            return Ok(userViewModel);
        }

        /// <summary>
        /// Email = 1,
        ///Residencial Phone = 2,
        ///Call Cellphone = 3,
        /// Whatsapp = 4,
        /// Telegram = 5,
        ///Other CellPhone Number = 6,
        ///Facebook Page = 7,
        /// Linkedin Page = 8,
        ///Instagram Page = 9,
        ///Youtubr Channel = 10,
        ///Other Page Source = 11,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserViewModel createUserViewModel)
        {

            if (createUserViewModel.MainContactType == 0)
            {
                return BadRequest("Main contact Type can't be null");
            }
            if (string.IsNullOrEmpty(createUserViewModel.Name))
            {
                return BadRequest("User name can't be null");

            }
            try
            {
                User user = new();
                user.Name = createUserViewModel.Name;
                user.MainContactType = createUserViewModel.MainContactType;
                user.PreferedContactTypes = createUserViewModel.PreferedContactTypes;
                user.AlternativeContactTypes = createUserViewModel.AlternativeContactType;
                user.Nickname = createUserViewModel.Nickame;
                user.Disable = createUserViewModel.Disabled;
                await _userService.CreateUser(user);
                return CreatedAtRoute("GetOneUser", new { id = user.Id }, user);

            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return Ok();
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("usersDisabled")]
        public async Task<IActionResult> GetAllDisabledUsers()
        {
            IList<User> users = await _userService.GetDisabledUsers();
            if (users == null)
            {
                return StatusCode(500);
            }
            IList<UserViewModel> userViewModelList = users.Select(x => new UserViewModel(x)).ToList();

            return Ok(userViewModelList);
        }

        [HttpGet("enableUsersByContactType")]
        public async Task<IActionResult> GetEnabledUsersByContactType(ContactType contactType)
        {
            IList<User> users = await _userService.GetEnabledUsersByContactType(contactType);
            if (users == null)
            {
                return NotFound();
            }
            IList<UserViewModel> userViewModelList = users.Select(x => new UserViewModel(x)).ToList();

            return Ok(userViewModelList);
        }

        [HttpGet("usersEnabled")]
        public async Task<IActionResult> GetAllEnabledUsers()
        {
            IList<User> users = await _userService.GetEnabledUsers();
            if (users == null)
            {
                return NotFound();
            }
            IList<UserViewModel> userViewModelList = users.Select(x => new UserViewModel(x)).ToList();

            return Ok(userViewModelList);
        }

        [HttpGet("usersContactTypes")]
        public async Task<IActionResult> GetContactTypesByUser(string userId)
        {
            User user = await _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }
            UserContactTypesViewModel userContactTypesViewModel = new(user);

            return Ok(userContactTypesViewModel);
        }

        [HttpPut("disableUser {id}")]
        public async Task<IActionResult> DisabledUser(string id)
        {
            try
            {
                User user = await _userService.DisableUser(id);
                if (user == null)
                {
                    return NotFound();
                }
                return CreatedAtRoute("GetOneUser", new { id = user.Id }, user);

            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut("enableUser {id}")]
        public async Task<IActionResult> EnabledUser(string id)
        {
            try
            {
                User user = await _userService.EnabledUser(id);
                if (user == null)
                {
                    return NotFound();
                }
                return CreatedAtRoute("GetOneUser", new { id = user.Id }, user);

            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }

    }
}
