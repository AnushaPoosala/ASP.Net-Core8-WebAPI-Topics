using DemoServerAppForHMAC.Context;
using DemoServerAppForHMAC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoServerAppForHMAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ServerAppDbContext _serverAppDbContext;

        public UserController(ServerAppDbContext serverAppDbContext)
        {
            _serverAppDbContext = serverAppDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _serverAppDbContext.Users.AddAsync(user);
            await _serverAppDbContext.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = user.Id }, user);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _serverAppDbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound("With given userid user is not exist");
            }

            return user;
        }

    }
}