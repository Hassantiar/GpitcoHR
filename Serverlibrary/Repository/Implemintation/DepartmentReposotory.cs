using Base.Entites;
using Base.Responses;
using Microsoft.EntityFrameworkCore;
using Serverlibrary.Data;
using Serverlibrary.Repository.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serverlibrary.Repository.Implemintation
{
    public class DepartmentReposotory(AppDBContext _context) : IGenaricReopInterface<Departrment>
    {
        public async Task<GeneralRespons> DeleteById(int id)
        {
           var dep = await _context.Departrment.FindAsync(id);
            if(dep is null) return NotFound();
            _context.Departrment.Remove(dep);
            await Commite();
            return Success();
        }

        public async Task<List<Departrment>> GetAll()=>await _context.Departrment.ToListAsync();


        public async Task<Departrment> GetById(int id) => await _context.Departrment.FindAsync(id);
        

        public async Task<GeneralRespons> Insert(Departrment item)
        {
           var dep=await _context.Departrment.FindAsync(item.Id);
            if (!await checkName(item.Name!)) return new GeneralRespons(false, "departmet is already existe");
           _context.Departrment.Add(item);
            await Commite();
            return Success();
        }

        public async Task<GeneralRespons> Update(Departrment item)
        {
            var dep =await _context.Departrment.FindAsync(item.Id);
            if(dep is null)return NotFound();
            dep.Name= item.Name;
            await Commite();
            return Success();
        }
        private GeneralRespons NotFound() => new(false, "Not Found");
        private GeneralRespons Success()=>new (true, "success");
        private async Task Commite()=>await _context.SaveChangesAsync();
        private async Task<bool> checkName(string name)
        {
            var item =await _context.Departrment.FirstOrDefaultAsync(x=>x.Name.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}
