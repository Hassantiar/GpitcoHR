using System.ComponentModel.DataAnnotations;

namespace Base.Entites
{
    public class Emploee :BaseEntity
    {
        [Required]
        public string civilid { get; set; }=string.Empty;
        [Required]
        public string FileNumber { get; set; } = string.Empty;
        [Required]
        public string FullName { get; set; }= string.Empty;
        [Required]
        public string JobName { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required, DataType(DataType.PhoneNumber)]
        public string TelephonNumber { get; set; } = string.Empty;
        [Required]
        public string photo { get; set; } = string.Empty;
        public string Other { get; set; } = string.Empty;
        /// <summary>
        /// RelationShep : many to one
        /// </summary>

        public Branch? Branch { get; set; }
        public int Branchid { get; set; }
        public Twon? Twon { get; set; }  
        public int Twonid { get; set;}  

    }
}
