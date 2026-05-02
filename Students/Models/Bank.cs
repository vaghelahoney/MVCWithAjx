using System.ComponentModel.DataAnnotations;

namespace Students.Models
{
    public class Bank
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Bank Name is required!")]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "IFSC Code is required!")]
        [StringLength(50)]
        public string? IFSCCode { get; set; }

        public bool IsActive { get; set; } = true;

        public List<BankDetails> BankDetails { get; set; } = new List<BankDetails>();
    }
}
