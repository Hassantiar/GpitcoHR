

namespace Base.Entites
{
    public class Branch:BaseEntity
    {
        public Departrment? Departrment { get; set; }
        public int Departmentid {  get; set; }  
        public List<Emploee> Emploees { get; set; } 
    }
}
