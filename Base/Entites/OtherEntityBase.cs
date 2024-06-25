

using System.ComponentModel.DataAnnotations;

namespace Base.Entites
{
    public class OtherEntityBase
    {
        public int ID {  get; set; }
        [Required]
        public string CivialId {  get; set; }=string.Empty;
        [Required]
        public string FileName { get; set; }= string.Empty;
        public string? Other {  get; set; }
    }
}
