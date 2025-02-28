namespace WebAPI02.Repos.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        void Add(T t);
        void Update(T t);
        void Delete(T t);
    }
}
