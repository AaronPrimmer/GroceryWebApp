namespace WebApp.Models
{
    public class ProductRepository
    {
        private static List<Product> _products = new List<Product>
        {
            new Product { ProductId = 1, Name = "Coca-Cola", CategoryId = 1, Quantity = 100, Price = 1.5 },
            new Product { ProductId = 2, Name = "Bread", CategoryId = 2, Quantity = 50, Price = 2.0 },
            new Product { ProductId = 3, Name = "Chicken Breast", CategoryId = 3, Quantity = 30, Price = 5.0 },
            new Product { ProductId = 4, Name = "Whole Wheat Bread", CategoryId = 2, Quantity = 40, Price = 2.5 }
        };

        public static void AddProduct(Product product)
        {
            if (_products.Count() > 0)
            {
                product.ProductId = _products.Max(p => p.ProductId) + 1;
            }
            else
            {
                product.ProductId = 1;
            }
            _products.Add(product);
        }

        public static List<Product> GetProducts() => _products;

        public static Product? GetProductById(int productId)
        {
            var product = _products.FirstOrDefault(x => x.ProductId == productId);
            if (product != null)
            {
                return new Product
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    CategoryId = product.CategoryId,
                    Quantity = product.Quantity,
                    Price = product.Price
                };
            }
            return null;
        }

        public static void UpdateProduct(int productId, Product product)
        {
            if (productId != product.ProductId) return;

            var productToUpdate = _products.FirstOrDefault(x => x.ProductId == productId);
            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.CategoryId = product.CategoryId;
                productToUpdate.Quantity = product.Quantity;
                productToUpdate.Price = product.Price;
            }
        }

        public static void DeleteProduct(int productId)
        {
            var productToDelete = _products.FirstOrDefault(x => x.ProductId == productId);
            if (productToDelete != null)
            {
                _products.Remove(productToDelete);
            }
        }
    }
}
