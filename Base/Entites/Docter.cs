

using System.ComponentModel.DataAnnotations;

namespace Base.Entites
{
    public class Docter:OtherEntityBase
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string MedicalDiagnose { get; set; }=string.Empty;
        [Required]
        public string MedicalRecommendation {  get; set; }=string.Empty;
    }
}
