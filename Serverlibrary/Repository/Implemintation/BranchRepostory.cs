using Base.Entites;
using Base.Responses;
using Microsoft.EntityFrameworkCore;
using Serverlibrary.Data;
using Serverlibrary.Repository.Contact;

namespace Serverlibrary.Repository.Implemintation
{
    public class BranchRepostory(AppDBContext _context) : IGenaricReopInterface<Branch>
    {
        public async Task<GeneralRespons> DeleteById(int id)
        {
           var branch=await _context.Branches.FindAsync(id);
            if (branch is null) return NotFound();
            _context.Branches.Remove(branch);
            await Commit();
            return Success();
        }

        public async Task<List<Branch>> GetAll()=> await _context.Branches.ToListAsync();


        public async Task<Branch> GetById(int id) => await _context.Branches.FindAsync(id);
        

        public async Task<GeneralRespons> Insert(Branch item)
        {
           
            if (!await checkName(item.Name)) return new GeneralRespons(false, "The Branch is already Exist");
            await Commit();
            return Success();
        }

        public async Task<GeneralRespons> Update(Branch item)
        {
            var branch = await _context.Branches.FindAsync(item.Id);
            if (branch is null) return NotFound();
            branch.Name= item.Name;
            await Commit();
            return Success();
        }
        private GeneralRespons NotFound() => new(false, "Not Found");
        private GeneralRespons Success()=>new(true,"success");
        private async Task Commit()=>await _context.SaveChangesAsync();
        private async Task<bool> checkName(string name)
        {
            var item =await _context.Branches.FirstOrDefaultAsync(x=>x.Name.ToLower().Equals(name.ToLower()));
            return item is null;
        }
        
    }
}
