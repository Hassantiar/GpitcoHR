

using System.ComponentModel.DataAnnotations;

namespace Base.Entites
{
    public class Sanction:OtherEntityBase
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Punchment {  get; set; }=string.Empty;
        [Required]
        public DateTime PunchmentDate { get; set; }
        public SanctionType? SanctionType { get; set; }
    }
}
