using Application.Services.CommentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Filters;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CommentsController : Controller
    {
        private readonly ICommentService commentService;

        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        // POST comment
        [ValidateForm]
        [HttpPost("{message}")]
        [Authorize(Roles = "1,3")]
        public async Task<IActionResult> Post(string message)
        {
            return await Task.FromResult<IActionResult>(null);
        }


        // Put comment
        [ValidateForm]
        [HttpPut("{id}/{message}")]
        [Authorize(Roles = "1,3")]
        public async Task<IActionResult> Put(int id, string message)
        {
            return await Task.FromResult<IActionResult>(null);
        }

        // Delete comment
        [ValidateForm]
        [HttpDelete("{id}")]
        [Authorize(Roles = "2,3")]
        public async Task<IActionResult> Delete(int id)
        {
            return await Task.FromResult<IActionResult>(null);
        }

        // Get comment
        [ValidateForm]
        [HttpGet("{id}")]
        [Authorize(Roles = "1,2,3")]
        public async Task<IActionResult> Get(int id)
        {
            return await Task.FromResult<IActionResult>(null);
        }

        // Post comment
        [ValidateForm]
        [HttpPost("ReplyToPost/{id}/{message}")]
        [Authorize(Roles = "1,3")]
        public async Task<IActionResult> ReplyToPost(int id, string message)
        {
            return await Task.FromResult<IActionResult>(null);
        }
    }
}