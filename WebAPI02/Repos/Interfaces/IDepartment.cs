using WebAPI02.Models;

namespace WebAPI02.Repos.Interfaces
{
    public interface IDepartment : IGenericRepository<Department>
    {
        List<Department> GetAllIncludeStudents();
        Department? GetByIdIncludeStudents(int id);
        void DetachAndModify(Department existingDepartment, Department updatedDepartment);
    }
}
