using Microsoft.EntityFrameworkCore;
using TestMVC.Models;

namespace TestMVC.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        ApplicationDbContext context;

        public string ServiceId { get; set; }
        public ProductRepository(ApplicationDbContext _context) : base(_context)
        {
            context = _context;
            ServiceId = Guid.NewGuid().ToString();
        }
        public List<Product> GetAll(string include = null)
        {
            return context.Products.ToList();

        }

        public Product Get(int id)
        {
            return context.Products.FirstOrDefault(p => p.Id == id);
        }
        public void Update(Product product)
        {
            Product orgProduct = Get(product.Id);
            orgProduct.Name = product.Name;
            orgProduct.Price = product.Price;
            orgProduct.Image = product.Image;
            orgProduct.CatId = product.CatId;
            orgProduct.Description = product.Description;
        }
        public void Insert(Product product)
        {
            context.Products.Add(product);
        }

        public void Delete(int id)
        {
            Product oldPro = Get(id);
            context.Products.Remove(oldPro);
        }
        public void Save()
        {
            context.SaveChanges();
        }

        public List<Product> GetOrderDetails(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
