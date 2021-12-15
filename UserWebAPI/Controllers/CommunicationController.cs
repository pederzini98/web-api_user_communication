using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommunicationController : Controller
    {
        private readonly ICommunicationService _communicationService;
        private readonly IUserService _userService;

        public CommunicationController(ICommunicationService communicationService, IUserService userService)
        {
            _communicationService = communicationService;
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCommunicationsByUserId(string userId)
        {
            IList<Communication> communication = await _communicationService.GetCommunicationByUser(userId);
            IList<CommunicationViewModel> communicationViewModel = communication.Select(x => new CommunicationViewModel(x)).ToList();

            return Ok(communicationViewModel);
        }

        [HttpGet("{id}", Name = "GetCommunicationsById")]
        public async Task<IActionResult> GetCommunicationsById(string id)
        {
            Communication communication = await _communicationService.GetCommunicationById(id);
            if (communication == null)
            {
                return NotFound();
            }
            CommunicationViewModel communicationViewModel = new(communication);
            return Ok(communicationViewModel);
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
        [HttpGet("{userId}/{contactType}")]
        public async Task<IActionResult> GetCommunicationsByUserIdAndContactType(string userId, ContactType contactType)
        {
            IList<Communication> communication = await _communicationService.GetCommunicationByUserAndContactType(userId, contactType);
            IList<CommunicationViewModel> communicationViewModel = communication.Select(x => new CommunicationViewModel(x)).ToList();

            return Ok(communicationViewModel);
        }
        [HttpGet("{title}")]
        public async Task<IActionResult> GetCommunicationsWithTitle( string title)
        {
            IList<Communication> communication = await _communicationService.GetCommunicationWithTitle(title);
            IList<CommunicationViewModel> communicationViewModel = communication.Select(x => new CommunicationViewModel(x)).ToList();

            return Ok(communicationViewModel);
        }
        [HttpGet()]
        public async Task<IActionResult> GetCommunicationsWithTitleBiggerThanFive()
        {

            Communication communication = new();
            List<Communication> communications = await _communicationService.GetBiggerCommunicationTitles();
            communications = communication.VerifyTitleLongerThanFiveChars(communications);
            IList<CommunicationViewModel> communicationViewModel = communications.Select(x => new CommunicationViewModel(x)).ToList();

            return Ok(communicationViewModel);
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
        public async Task<IActionResult> CreateCommunication(CreateCommunicationViewModel createCommunicationViewModel)
        {
            try
            {
                User findUser = await _userService.GetUserById(createCommunicationViewModel.UserId);
                if (findUser == null)
                {
                    return NotFound("User no found");
                }
                else if (String.IsNullOrEmpty(createCommunicationViewModel.CommunicationTitle) || createCommunicationViewModel.CommunicationTitle.Length < 5)
                {
                    return BadRequest("Communication Title needs to contain at least 5 characters");

                }
                else if (createCommunicationViewModel.UsedContactType != findUser.MainContactType &&
                    !findUser.PreferedContactTypes.Contains(createCommunicationViewModel.UsedContactType) &&
                    !findUser.AlternativeContactTypes.Contains(createCommunicationViewModel.UsedContactType))
                {
                    return BadRequest("This user doesn't have this contact type registered");
                }
                Communication communication = new();
                communication.CommunicationTitle = createCommunicationViewModel.CommunicationTitle;
                communication.UserId = createCommunicationViewModel.UserId;
                communication.UsedContactType = createCommunicationViewModel.UsedContactType;
                communication.CommunicationContent = createCommunicationViewModel.CommunicationContent;

                await _communicationService.CreateCommunication(communication);

                return CreatedAtRoute("GetCommunicationsById", new { id = communication.Id }, communication);

            }
            catch (Exception e)
            {

                return BadRequest("User not Found");
            }

        }
    }
}
