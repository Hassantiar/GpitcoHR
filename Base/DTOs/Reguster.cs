using System.ComponentModel.DataAnnotations;

namespace Base.DTOs
{
    public class Reguster:AccountBase
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string FullName { get; set; }=string.Empty;

        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Required]
        public string CoonfirmPassword { get; set; } = string.Empty;
    }
}
