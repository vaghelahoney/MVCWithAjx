using System.ComponentModel.DataAnnotations;

namespace Students.Models
{
    public class Countries
    {
        [Key]
        public int Country_Id { get; set; }

        public string? Country_Name { get; set; }
    }
}
