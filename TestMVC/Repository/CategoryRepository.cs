using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using TestMVC.Models;

namespace TestMVC.Repository
{
    public class CategoryRepository: IRepository<Category>
    {
        ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public string ServiceId { get; set; }

        public List<Category> GetAll(string includes = null)
        {
            if(includes == null)
            {
                return context.Categories.ToList();
            }
            else
            {
                return context.Categories.Include(includes).ToList();
            }
        }

        public Category Get(int id)
        {
            return context.Categories.FirstOrDefault(p => p.Id == id);
        }

        public void Update(Category cat) 
        {
            Category orgCat = Get(cat.Id);
            orgCat.Name = cat.Name;
        }
        public void Insert(Category cat)
        {
            context.Categories.Add(cat);
        }
        public void Delete(int id)
        {
            Category oldCat = Get(id);
            context.Categories.Remove(oldCat);
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
