

using System.ComponentModel.DataAnnotations;

namespace Base.Entites
{
    public class OverTime:OtherEntityBase
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required] 
        public DateTime EndDate { get; set; }
        public int NumberOfDates=>(EndDate-StartDate).Days;
        public OverTimeType? OverTimeType {  get; set; }
        [Required]
        public int OverTimeTypeId {  get; set; }
    }
}
