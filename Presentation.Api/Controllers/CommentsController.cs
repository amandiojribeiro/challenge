using Application.Services.CommentService;
using Domain.Model.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        [Authorize(Roles = "0,2")]
        [HttpPost("{message}")]
        public async Task<IActionResult> Post(string message)
        {
            var role = await GetAuthorType();
            var result = await this.commentService.AddComment(new Application.Dto.CommentDto() { Message = message}, role, (int)role);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        // Put comment
        [HttpPut("{id}/{message}")]
        public async Task<IActionResult> Put(int id, string message)
        {
            var role = await GetAuthorType();
            var result = await this.commentService.EditComment(new Application.Dto.CommentDto() {Id=id, Message = message }, role, (int)role);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        // Delete comment
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await GetAuthorType();
            var result = await this.commentService.DeleteComment(new Application.Dto.CommentDto() { Id = id}, role, (int)role);

            if (result != false)
            {
                return Ok(result);
            }

            return NotFound();
        }

        // Get comment
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var role = await GetAuthorType();
            var result = await this.commentService.GetComment(id);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        // Post comment
        [HttpPost("ReplyToPost/{id}/{message}")]
        public async Task<IActionResult> ReplyToPost(int id, string message)
        {
            var role = await GetAuthorType();
            var result = await this.commentService.ReplyToComment(new Application.Dto.CommentDto() { Message = message}, role, id, (int)role);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        private async Task<AuthorType> GetAuthorType()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userRole = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value;
            return await Task.FromResult<AuthorType> ((AuthorType) int.Parse(userRole));
        }
    }
}