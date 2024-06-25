

namespace Base.Entites
{
    public class Twon:BaseEntity
    {
        public List<Emploee>? Emploees { get; set; }
        public City? City { get; set; }
        public int CityId {  get; set; }
    }
}
