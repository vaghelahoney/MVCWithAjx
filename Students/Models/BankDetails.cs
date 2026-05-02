using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Students.Models
{
    public class BankDetails
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Account Name is required!")]
        public string? AccountName { get; set; }

        [Required(ErrorMessage = "Account Number is required!")]
        public string? AccountNumber { get; set; }

        [Required(ErrorMessage = "Branch Name is required!")]
        public string? BranchName { get; set; }

        [Required]
        [ForeignKey("Bank")]
        public int BankId { get; set; }
    }
}
