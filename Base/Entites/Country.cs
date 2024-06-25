

namespace Base.Entites
{
    public class Country:BaseEntity
    {
        public List<City>? Cities { get; set; }
    }
}
