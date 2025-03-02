using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API
{
  [ApiController]
  [Route("api/[controller]")] // /api/users 

  public class UserController : ControllerBase
  {

    private readonly DataContext _context;
    public UserController(DataContext context)
    {
      _context = context;
    }

    [HttpGet("{id:int}")] // /api/users/1
    public async Task<ActionResult <AppUser>> GetUser(int id)
    {
      var user = await _context.Users.FindAsync(id);
      if (user != null)
      {
          return Ok(user);
      }
      else 
      {
        return BadRequest("User not found");
      }
    }

    [HttpGet] // /api/users
    public async Task<ActionResult <IEnumerable<AppUser>>> GetUsers()
    {
      var users = await _context.Users.ToListAsync();
      return Ok(users);

    }
  }

}