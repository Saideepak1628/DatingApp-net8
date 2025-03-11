using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities {

    [Table("Photos")]
    public class Photos
    {
        public int Id { get; set;}

        public required string url { get; set; }

        public bool IsMain { get; set; }

        public string? PublicId { get; set; }

        //navigation properties 

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;

    }
}