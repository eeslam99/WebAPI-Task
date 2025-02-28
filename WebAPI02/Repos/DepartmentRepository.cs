using Microsoft.EntityFrameworkCore;
using WebAPI02.Data;
using WebAPI02.Models;
using WebAPI02.Repos.Interfaces;

namespace WebAPI02.Repos
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartment
    {
        public DepartmentRepository(AppDbContext db) : base(db) { }

        public List<Department> GetAllIncludeStudents()
            => _db.Departments.Include(d => d.Students).ToList();

        public Department? GetByIdIncludeStudents(int id)
            => _db.Departments.Include(d => d.Students).FirstOrDefault(d => d.DeptId == id);

        public void DetachAndModify(Department existingDepartment, Department updatedDepartment)
        {
            _db.Entry(existingDepartment).State = EntityState.Detached;
            _db.Entry(updatedDepartment).State = EntityState.Modified;
        }
    }
}