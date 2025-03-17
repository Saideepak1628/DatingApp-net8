using System.Security.Claims;
using API.Controllers;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

  [Authorize]
  public class UserController(IUserRepository repository, IMapper mapper) : BaseApiController
  {

    // private readonly DataContext _context;
    // public UserController(DataContext context)
    // {
    //   _context = context;
    // }

     [HttpGet] // /api/users
    // [AllowAnonymous] // temporarily allow all requests.
    public async Task<ActionResult <IEnumerable<MemberDto>>> GetUsers()
    {
      var users = await  repository.GetMembersAsync();
   
      return Ok(users);
    }
  
    // [HttpGet("id/{id:int}")] // /api/users/1
    // public async Task<ActionResult <MemberDto>> GetUser(int id)
    // {
    //   var user = await repository.GetMemberAsync(id);
    //   if (user != null)
    //   {
    //       return mapper.Map<MemberDto>(user);
    //   }
    //   else 
    //   {
    //     return BadRequest("User not found");
    //   }
    // }


    [HttpGet("{username}")] // /api/users/username
    public async Task<ActionResult <MemberDto>> GetUserByUsername(string username)
    {
      var user = await repository.GetMemberAsync(username);
      if (user != null)
      {
          return Ok(user);
      }
      else 
      {
        return BadRequest("User not found");
      }
    }

    [HttpPut]
public async Task<ActionResult> UpdateUser([FromBody] MemberUpdateDto memberUpdateDto) 
{
    // Get username from token
    var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    
    if (string.IsNullOrEmpty(username))
    {
        return BadRequest("No Username found in the token");
    }

    // Fetch user from the repository
    var user = await repository.GetUserByUsernameAsync(username);

    if (user == null) 
    {
        return NotFound("User not found");
    }

    // Map DTO to user entity
    mapper.Map(memberUpdateDto, user);

    // Save changes
    if (await repository.SaveAllAsync()) 
        return NoContent(); // Success: 204 No Content

    return BadRequest("Failed to update the user");
}


  }




}