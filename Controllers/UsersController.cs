using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Data;
using Microsoft.EntityFrameworkCore;

public class UserPatchDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<User>> Update(int id, [FromBody] UserPatchDto userDto)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null) return NotFound();

            if (userDto.FirstName != null)
                existingUser.FirstName = userDto.FirstName;

            if (userDto.LastName != null)
                existingUser.LastName = userDto.LastName;

            if (userDto.Email != null)
                existingUser.Email = userDto.Email;

            if (userDto.Password != null)
                existingUser.Password = userDto.Password;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
