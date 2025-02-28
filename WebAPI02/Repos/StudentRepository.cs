using Microsoft.EntityFrameworkCore;
using WebAPI02.Data;
using WebAPI02.Models;
using WebAPI02.Repos.Interfaces;

namespace WebAPI02.Repos
{
    public class StudentRepository : GenericRepository<Student>, IStudent
    {
        public StudentRepository(AppDbContext db) : base(db) { }

        public List<Student> GetAllIncludeDepartment()
            => _db.Students.Include(S => S.Dept).ToList();

        public Student? GetByIdIncludeDepartment(int id)
            => _db.Students.Include(s => s.Dept).FirstOrDefault(s => s.StId == id);

        public (List<Student> Students, int TotalCount) GetPaginatedStudents(int pageNumber, int pageSize)
        {
            var totalCount = _db.Students.Count();

            var students = _db.Students
                .Include(s => s.Dept)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (students, totalCount);
        }
    }
}