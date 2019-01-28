using Application.Services.CommentService;
using Domain.Model.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Filters;
using Presentation.Api.Model;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    public class CommentsController : Controller
    {
        private readonly ICommentService commentService;
        //private readonly UserManager<User> _userManager;

        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
            //this._userManager = userManager;
        }

        // POST comment
       
        [HttpPost("{message}")]
        //[Authorize("Bearer")]
        //[Authorize(Roles = "1,3")]
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
        [ValidateForm]
        [HttpPut("{id}/{message}")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //[Authorize(Roles = "1,3")]
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
        [ValidateForm]
        [HttpDelete("{id}")]
        //[Authorize(Roles = "2,3")]
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
        [ValidateForm]
        [HttpGet("{id}")]
        //[Authorize(Roles = "1,2,3")]
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
        [ValidateForm]
        [HttpPost("ReplyToPost/{id}/{message}")]
        //[Authorize(Roles = "1,3")]
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