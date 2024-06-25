

using System.ComponentModel.Design;

namespace Base.Entites
{
    public class City:BaseEntity
    {
        public Country? Country { get; set; }
        public int ? CountryId { get; set; }


        public List<Twon>Twons { get; set; }

    }
}
