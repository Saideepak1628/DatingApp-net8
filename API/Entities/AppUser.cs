using System.ComponentModel.DataAnnotations;
using API.Extensions;

namespace API.Entities {
public partial class AppUser 
  {
    [Key]
    public int Id { get; set; }
    public required string UserName { get; set;}
    public byte[] PasswordHash { get; set; }   = [];
    public  byte[] PasswordSalt { get; set; } = [];

    public DateOnly DateOfBirth { get; set; }

    public required string KnownAs { get; set; }

    public DateTime created { get; set; } = DateTime.UtcNow;

    public DateTime LastActive { get; set; } = DateTime.UtcNow;

    public string? Introduction { get; set; }
    public string? LookingFor { get; set; }

    public string? Interests { get; set; }

    public required string City { get; set; }
    public required string Country { get; set; }
    public List<Photos> Photos { get; set; } = [];

    // public int GetAge() 
    // {
    //   return DateOfBirth.CalculateAge();
    // }

  }
}