using WebAPI02.Repos.Interfaces;

namespace WebAPI02.UnitofWork
{
    public interface IUnitOfWork : IDisposable
    {
        IStudent Students { get; }
        IDepartment Departments { get; }

        int Complete();
    }
}
