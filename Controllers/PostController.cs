using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostApi.Models;
using PostApi.Services;

namespace PostApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController(PostService postService) : ControllerBase
    {
        private readonly PostService _postService = postService;

        [HttpGet]
        public ActionResult<List<Post>> GetAllPosts()
        {
            try
            {
                return Ok(_postService.ListPost());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Post> GetPostById(int id)
        {
            try
            {
                return Ok(_postService.ShowPost(id));
            }
            catch (Exception)
            {
                return NotFound("Post not found");
            }
        }

        [HttpPost]
        public ActionResult<Post> CreatePost([FromBody] Post post)
        {
            try
            {
                _postService.PublishPost(post);
                return StatusCode(201, post);
            }
            catch (Exception ex)
            {
                //Log the error i.e., ex.Message
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult<string> UpdatePost(int id, Post post)
        {
            try
            {
                _postService.UpdatePost(id, post);
                return Ok("Post updated");
            }
            catch (Exception ex)
            {
                //Log the error i.e., ex.Message
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]

        public ActionResult<string> CancelPost(int id)
        {
            try
            {
                _postService.CancelPost(id);
                return Ok("post has cancelled");
            }
            catch (Exception ex)
            {
                //Log the error i.e., ex.Message
                return BadRequest(ex.Message);
            }
        }
    }
}