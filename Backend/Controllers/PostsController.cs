using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        IPostsService _tittlesServices;

        public PostsController(IPostsService titlesSErvice)
        {
            _tittlesServices = titlesSErvice;
        }

        [HttpGet]
        public async Task<IEnumerable<PostDto>> Get() =>
            await _tittlesServices.Get();
    }
}
