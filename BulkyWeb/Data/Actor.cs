using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Data
{
    public class Actor
    {
        public int Id { get; set; }
        [Required]
        public string Fullname { get; set; }
    }
}