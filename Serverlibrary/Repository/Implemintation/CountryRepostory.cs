using Base.Entites;
using Base.Responses;
using Microsoft.EntityFrameworkCore;
using Serverlibrary.Data;
using Serverlibrary.Repository.Contact;

namespace Serverlibrary.Repository.Implemintation
{
    public class CountryRepostory(AppDBContext _context) : IGenaricReopInterface<Country>
    {
        public async Task<GeneralRespons> DeleteById(int id)
        {
           var country = await _context.Countries.FindAsync(id);
            if (country is null) return NotFound(); 
            _context.Countries.Remove(country);
            await Commit();
            return Success();
        }
        public async Task<List<Country>> GetAll()=> await _context.Countries.ToListAsync();
        public async Task<Country> GetById(int id) => await _context.Countries.FindAsync(id);

        public async Task<GeneralRespons> Insert(Country item)
        {
            if (!await checkName(item.Name)) return new(false,"The country is already exist");
            _context.Countries.Add(item);
            await Commit();
            return Success();

        }

        public async Task<GeneralRespons> Update(Country item)
        {
           var country = await _context.Countries.FindAsync(item.Id);
            if (country is null) return NotFound();
            country.Name= item.Name;
            await Commit(); 
            return Success();
        }
        private GeneralRespons NotFound() => new(false, "Not Found");
        private GeneralRespons Success()=>new(true , "success");    
        private async Task Commit()=>await _context.SaveChangesAsync();
        private async Task<bool> checkName(string name)
        {
            var item = await _context.Countries.FirstOrDefaultAsync(x=>x.Name.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}
