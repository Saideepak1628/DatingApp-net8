using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers 
{


  public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController 
  {

    // `[controller]` will be replaced with `Account`

    [HttpPost("register")] // /account/register 

    public async Task<ActionResult<UserDTO>> Register(RegisterDto registerDto)
    {

      if (await UserExists(registerDto.Username)) return BadRequest("Username already exists");
          return Ok();
      // using var hmac = new HMACSHA512();
      
      // var user = new AppUser
      // {
      //   UserName = registerDto.Username.ToLower(),
      //   PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
      //   PasswordSalt = hmac.Key 

      // };
      // context.Users.Add(user);

      // await context.SaveChangesAsync();

      // return new UserDTO 
      // {
      //   Username = user.UserName,
      //   Token = tokenService.CreateToken(user)
      // };

    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDto loginDto)
    {

      try {
        var user = await context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == loginDto.Username.ToLower());
      if (user == null) return  Unauthorized("Invalid username or password");
      using var hmac = new HMACSHA512(user.PasswordSalt);

      var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

      for (int i = 0; i < ComputeHash.Length; i++)
      {
        if (ComputeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
      }
      return new UserDTO 
      {
        Username = user.UserName,
        Token = tokenService.CreateToken(user)
      };

      }
      catch (Exception ex) {
        return Unauthorized("User is invalid" + ex.Message);
      }
      
    }

    

    


    private async Task<bool> UserExists(string username)
    {
      return await context.Users.AnyAsync(x=>x.UserName.ToLower() == username.ToLower());
    }

  }
}