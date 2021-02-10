using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class DirectMessagesController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IDirectMessageRepository _directMessageRepository;
        private readonly IMapper _mapper;
        public DirectMessagesController(IUserRepository userRepository, IDirectMessageRepository directMessageRepository, IMapper mapper)
        {
            _mapper = mapper;
            _directMessageRepository = directMessageRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<DirectMessageDto>> CreateDirectMessage(CreateDirectMessageDto createDirectMessageDto)
        {
            var username = User.GetUsername();

            if (username == createDirectMessageDto.RecieverUsername.ToLower()) return BadRequest("You can't send direct messages to yourself");

            var sender = await _userRepository.GetUserByUsernameAsync(username);
            var reciever = await _userRepository.GetUserByUsernameAsync(createDirectMessageDto.RecieverUsername);

            if (reciever == null) return NotFound();

            var directMessage = new DirectMessage
            {
                Sender = sender,
                SenderUsername = sender.UserName,
                Reciever = reciever,
                RecieverUsername = reciever.UserName,
                MessageContent = createDirectMessageDto.MessageContent
            };

            _directMessageRepository.AddDirectMessage(directMessage);

            if (await _directMessageRepository.SaveAllAsync()) return Ok(_mapper.Map<DirectMessageDto>(directMessage));

            return BadRequest("Sending direct message failed");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectMessageDto>>> GetDirectMessagesForUser([FromQuery] DirectMessageParams directMessageParams)
        {
            directMessageParams.Username = User.GetUsername();

            var directMessages = await _directMessageRepository.GetDirectMessagesForUser(directMessageParams);

            Response.AddPaginationHeader(directMessages.CurrentPage, directMessages.PageSize, directMessages.TotalCount, directMessages.TotalPages);

            return directMessages;
        }
    }
}