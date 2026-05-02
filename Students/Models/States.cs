using System.ComponentModel.DataAnnotations;

namespace Students.Models
{
    public class States
    {
        [Key]
        public int State_Id { get; set; }

        public string? State_Name { get; set; }

        public int Country_Id { get; set; }
    }
}


 	
