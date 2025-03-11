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
  public class UserController(IUserRepository repository) : BaseApiController
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
  }




}