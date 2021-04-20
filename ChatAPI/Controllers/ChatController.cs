using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace ChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly IChatRepository _chatRep;
        const string cId = "6f9619ff-8b86-d011-b42d-00cf4fc964ff";
        public ChatController(IChatRepository chatRep)
        {
            _chatRep = chatRep;
        }

        [Authorize]
        [HttpPost("AddMessage")]
        public async Task<IActionResult> AddMessage(Guid chatId, string text, Guid? isAnswerFor)
        {
            var authorId = User.Claims.ToList()[0].Value;
            await _chatRep.AddMessage(chatId,text,authorId,isAnswerFor);
            return Ok();
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(Guid chatId, string userId)
        {
            await _chatRep.AddUser(chatId,userId);
            return Ok();
        }

        [Authorize]
        [HttpPost("CreateChat")]
        public async Task<IActionResult> CreateChat()
        {
            var userId = User.Claims.ToList()[0].Value;
            await _chatRep.CreateChat(userId);
            return Ok();
        }

        [HttpPost("DeleteChat")]
        public async Task<IActionResult> DelteChat(Guid chatId)
        {
            await _chatRep.DeleteChat(chatId);
            return Ok();
        }

        [Authorize]
        [HttpGet("GetMessages")]
        public async Task<IActionResult> GetMessages(Guid chatId, int howMany = 20, int part = 0)
        {
            var userId = User.Claims.ToList()[0].Value;
            return Json(await _chatRep.GetMessagesForUser(userId, new Guid(cId), howMany, part));
            
        }
    }
}