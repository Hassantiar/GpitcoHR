using Base.Entites;
using Base.Responses;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using Serverlibrary.Data;
using Serverlibrary.Repository.Contact;

namespace Serverlibrary.Repository.Implemintation
{
    public class CityRepostory(AppDBContext _context) : IGenaricReopInterface<City>
    {
        public async Task<GeneralRespons> DeleteById(int id)
        {
           var city = await _context.Cities.FindAsync(id);
            if (city is null) return NotFound();
            _context.Cities.Remove(city);
            await Commite();
            return Success();
        }

        public async Task<List<City>> GetAll()=>await _context.Cities.ToListAsync();


        public async Task<City> GetById(int id) => await _context.Cities.FindAsync(id);
        

        public async Task<GeneralRespons> Insert(City item)
        {
            if (!await CheckName(item.Name)) return new(false, "The city is already exist");
            _context.Cities.Add(item);
            await Commite();
            return Success();
        }

        public async Task<GeneralRespons> Update(City item)
        {
           var city = await _context.Cities.FindAsync(item.Id);
            if (city is null) return NotFound();
            city.Name= item.Name;
            await Commite();
            return Success();
        }
        private GeneralRespons NotFound() => new(false, "City Not Found");
        private GeneralRespons Success() => new(true, "Proccess Successfuly completed");
        private async Task Commite()=>await _context.SaveChangesAsync();
        private async Task<bool> CheckName(string name)
        {
            var item =await _context.Cities.FirstOrDefaultAsync(x=>x.Name.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}
