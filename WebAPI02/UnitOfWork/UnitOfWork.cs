using WebAPI02.Data;
using WebAPI02.Repos.Interfaces;
using WebAPI02.Repos;

namespace WebAPI02.UnitofWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IStudent _students;
        private IDepartment _departments;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IStudent Students => _students ??= new StudentRepository(_context);

        public IDepartment Departments => _departments ??= new DepartmentRepository(_context);

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
