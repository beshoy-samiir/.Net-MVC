using TestMVC.Models;

namespace TestMVC.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetOrderDetails(int orderId);
    }
}