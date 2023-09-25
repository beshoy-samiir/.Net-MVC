using TestMVC.Models;

namespace TestMVC.Repository
{
    public interface IRepository<T>
    {
        string ServiceId { get; set; }
        List<T> GetAll(string includes = null);
        T Get(int id);
        void Update(T cat);
        void Insert(T obj);
        void Delete(int id);
        void Save();
    }
}