using API.Controllers;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")] // /api/users 

  public class UserController : BaseApiController
  {

    private readonly DataContext _context;
    public UserController(DataContext context)
    {
      _context = context;
    }

    [Authorize]

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
    [AllowAnonymous] // temporarily allow all requests.
    public async Task<ActionResult <IEnumerable<AppUser>>> GetUsers()
    {
      var users = await _context.Users.ToListAsync();
      return Ok(users);
    }
  }

}