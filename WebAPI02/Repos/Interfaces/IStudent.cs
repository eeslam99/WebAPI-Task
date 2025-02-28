using WebAPI02.Models;

namespace WebAPI02.Repos.Interfaces
{
    public interface IStudent : IGenericRepository<Student>
    {
        // Add any additional signatures for methods here for the Student entity
        List<Student> GetAllIncludeDepartment();
        Student? GetByIdIncludeDepartment(int id);
        (List<Student> Students, int TotalCount) GetPaginatedStudents(int pageNumber, int pageSize);
    }
}
