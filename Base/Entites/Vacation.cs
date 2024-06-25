

using System.ComponentModel.DataAnnotations;

namespace Base.Entites
{
    public class Vacation:OtherEntityBase
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public int NumbuerOfDay {  get; set; }
        public DateTime EndDate=>StartDate.AddDays(NumbuerOfDay);
        public VacationType? VacationType { get; set; }
        [Required]
        public int VacationTypeId { get; set; }
    }
}
