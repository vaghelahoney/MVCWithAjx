using System.ComponentModel.DataAnnotations;

namespace Students.Models
{
    public class Cities
    {

        [Key]
        public int City_Id { get; set; }

        public string? City_Name { get; set; }

        public int State_Id { get; set; }
    }
}

