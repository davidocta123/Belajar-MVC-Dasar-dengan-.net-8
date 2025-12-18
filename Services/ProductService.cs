using ECommerceApp.Models;

namespace ECommerceApp.Services
{
    public class ProductService : IProductService
    {
        private static readonly List<Product> _products = new();
        private static int _nextId = 1;

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Product? GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Product product)
        {
            product.Id = _nextId++;
            _products.Add(product);
        }

        public void Update(Product product)
        {
            var existing = GetById(product.Id);
            if (existing == null) return;

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Description = product.Description;
            existing.ImageUrl = product.ImageUrl;
        }

        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }
    }
}
