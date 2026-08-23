using System.Threading.Tasks;
using AiApplication.Application.Chat;
using Microsoft.AspNetCore.Mvc;

namespace AiApplication.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public async Task<IActionResult> Chat(ChatRequest request)
        {
            if (request == null)
            {
                return BadRequest("请求体不能为空。");
            }

            var result = await _chatService.ChatAsync(
                request,
                HttpContext.RequestAborted);

            return Ok(result);
        }
    }
}
