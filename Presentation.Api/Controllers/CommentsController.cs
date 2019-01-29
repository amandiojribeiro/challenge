using Application.Services.CommentService;
using Domain.Model.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Filters;
using Presentation.Api.Model;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    [Authorize(Policy = "Member")]
    [Route("api/[controller]")]
    public class CommentsController : Controller
    {
        private readonly ICommentService commentService;

        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        // POST comment
       
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
            //var user = await _userManager.GetUserAsync(HttpContext.User);
            //var role = await _userManager.GetRolesAsync(user);
            //return (AuthorType) int.Parse(role.FirstOrDefault());
            return await Task.FromResult<AuthorType>(AuthorType.User);
        }
    }
}