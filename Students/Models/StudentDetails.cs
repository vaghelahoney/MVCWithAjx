using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Students.Models
{
    public class StudentDetails
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Aadhar Number is required!")]
        [StringLength(12, ErrorMessage = "Aadhar Number cannot be longer than 12 characters.")]
        public string? AadharNumber { get; set; }

        [Required(ErrorMessage = "Mobile Number is required!")]
        [Phone]
        public string? MobileNumber { get; set; }

        [Required]
        [ForeignKey("StudentId")]
        public int StudentId { get; set; }

        //public Student? Student { get; set; }
    }
}
