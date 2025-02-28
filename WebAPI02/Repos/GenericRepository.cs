using WebAPI02.Data;
using WebAPI02.Repos.Interfaces;

namespace WebAPI02.Repos
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private protected readonly AppDbContext _db;

        public GenericRepository(AppDbContext db)
        {
            _db = db;
        }

        public void Add(T t)
        {
            _db.Add(t);
        }

        public void Delete(T t)
        {
            _db.Remove(t);
        }

        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public void Update(T t)
        {
            _db.Set<T>().Update(t);
        }
    }
}
