using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRep;

        public MessageController(IMessageRepository messageRep)
        {
            _messageRep = messageRep;
        }

        [HttpDelete("DeleteForOwner")]
        public async Task<IActionResult> DeleteForOwner(Guid messageId)
        {
            await _messageRep.DeleteForOwner(messageId, User.Claims.ToList()[0].Value);
            return Ok();
        }

        [HttpDelete("DeleteForAnyboy")]
        public async Task<IActionResult> DeleteForAnybody(Guid messageId)
        {
            await _messageRep.DeleteForAnybody(messageId, User.Claims.ToList()[0].Value);
            return Ok();
        }

        [HttpPut("UpdateMessage")]
        public async Task<IActionResult> UpdateMessage(Guid messageId, string newText)
        {
            await _messageRep.UpdateMessage(messageId, User.Claims.ToList()[0].Value, newText);
            return Ok();
        }
    }
}
