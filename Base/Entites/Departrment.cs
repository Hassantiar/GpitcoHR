

namespace Base.Entites
{
    public class Departrment : BaseEntity
    {
        public GeneralDepartment? GeneralDepartment { get; set; }
        public int GeneralDepartmentid { get; set; }

        public List<Branch>? Branches{get;set;}
    }
}
