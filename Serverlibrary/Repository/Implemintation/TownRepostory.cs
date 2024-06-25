using Base.Entites;
using Base.Responses;
using Microsoft.EntityFrameworkCore;
using Serverlibrary.Data;
using Serverlibrary.Repository.Contact;
using System.Reflection.Metadata.Ecma335;

namespace Serverlibrary.Repository.Implemintation
{
    public class TownRepostory(AppDBContext _context) : IGenaricReopInterface<Twon>
    {
        public async Task<GeneralRespons> DeleteById(int id)
        {
           var twon =await _context.Twons.FindAsync(id);
            if(twon is null)return NotFound();
            _context.Twons.Remove(twon);
            await Commite();
            return Success();
        }

        public async Task<List<Twon>> GetAll()=>await _context.Twons.ToListAsync();


        public async Task<Twon> GetById(int id) => await _context.Twons.FindAsync(id);
      

        public async Task<GeneralRespons> Insert(Twon item)
        {
            if (!await checkName(item.Name)) return new(false, "This Twon is Already Exist");
            _context.Twons.Add(item);
            await Commite();
            return Success();
        }

        public async Task<GeneralRespons> Update(Twon item)
        {
           var twon = await _context.Twons.FindAsync(item.Id);
            if (twon is null) return NotFound();
            twon.Name = item.Name;
            await Commite();
            return Success();
        }
        private GeneralRespons NotFound() => new(false, "the Town is not found");
        private GeneralRespons Success() => new(true, "Proccess is completed Successfuly");
        private async Task Commite()=>await _context.SaveChangesAsync();
        private async Task<bool> checkName(string name)
        {
            var item = await _context.Twons.FirstOrDefaultAsync(x=>x.Name.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}
