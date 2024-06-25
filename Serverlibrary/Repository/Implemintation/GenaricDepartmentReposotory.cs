using Base.Entites;
using Base.Responses;
using Microsoft.EntityFrameworkCore;
using Serverlibrary.Data;
using Serverlibrary.Repository.Contact;
using System.Reflection.Metadata.Ecma335;

namespace Serverlibrary.Repository.Implemintation
{
    public class GenaricDepartmentReposotory(AppDBContext _context) : IGenaricReopInterface<GeneralDepartment>
    {
        public async Task<GeneralRespons> DeleteById(int id)
        {
            var dep = await _context.GeneralDepartment.FindAsync(id);
            if (dep is null)
                return NotFound();
            _context.GeneralDepartment.Remove(dep);
            await Commit();
            return Success();
        }

        public async Task<List<GeneralDepartment>> GetAll() => await _context.GeneralDepartment.ToListAsync();


        public async Task<GeneralDepartment> GetById(int id) => await _context.GeneralDepartment.FindAsync(id);


        public async Task<GeneralRespons> Insert(GeneralDepartment item)
        {
            var checknam = await checkName(item.Name);
            if (checknam) return new GeneralRespons(false, "The Departmet is already exist !!");
            _context.GeneralDepartment.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralRespons> Update(GeneralDepartment item)
        {
            var dep = await _context.GeneralDepartment.FindAsync(item.Id);
            if (dep is null) return NotFound();
            dep.Name = item.Name;
            await Commit();
            return Success();
        }
        private GeneralRespons NotFound() => new(false, "the department not found..");
        private GeneralRespons Success() => new(true, "Process completed");
        private async Task Commit() => await _context.SaveChangesAsync();
        private async Task<bool> checkName(string name)
        {
            var item = await _context.GeneralDepartment.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return item is null ? true : false;
        }
    }
}
