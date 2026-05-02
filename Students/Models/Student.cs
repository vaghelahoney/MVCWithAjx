using System.ComponentModel.DataAnnotations;

namespace Students.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "First Name is required!")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public  string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required!")]
        public string? LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email ID is required!")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address!")]
        public string? Email { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public int CountryId { get; set; }

        [Required]
        public int StateId { get; set; }

        [Required]
        public int CityId { get; set; } 


        public StudentDetails? StudentDetails { get; set; }
    }
}
