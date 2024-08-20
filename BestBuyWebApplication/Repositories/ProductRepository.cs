using Dapper;
using System.Collections.Generic;
using System.Data;
using Testing.Abstractions;
using Testing.Models;

namespace Testing.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;

        public ProductRepository(IDbConnection conn){
            _conn = conn;
        }

		public Product AssignCategory()
		{
			var categoryList = GetCategories();
            var product = new Product();
            product.Categories = categoryList;

            return product;
		}

		public void CreateProduct(Product product)
        {
            string sql = "INSERT INTO products (Name, Price, CategoryID, OnSale, StockLevel) VALUES (@Name, @Price, @CategoryID, 0, @StockLevel);";
            _conn.Execute(sql, new {Name = product.Name, Price = product.Price, CategoryID = product.CategoryId, StockLevel = product.StockLevel });
        }

		public void DeleteProduct(Product product)
		{

			_conn.Execute("DELETE FROM REVIEWS WHERE ProductID = @id;",
									   new { id = product.ProductId });
			_conn.Execute("DELETE FROM Sales WHERE ProductID = @id;",
									   new { id = product.ProductId });
			_conn.Execute("DELETE FROM Products WHERE ProductID = @id;",
									   new { id = product.ProductId });


		}

		public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("SELECT * FROM products;");
        }

		public IEnumerable<Category> GetCategories()
		{
			return _conn.Query<Category>("SELECT * FROM categories;");
		}

		public Product GetProduct(int id)
		{
            string sql = "SELECT * FROM products WHERE ProductId = @Id";
            var product = _conn.QuerySingleOrDefault<Product>(sql, new { Id = id});
            return product;
		}

		public void InsertProduct(Product productToInsert)
		{
            string sql = "INSERT INTO products (NAME, PRICE, CATEGORYID) VALUES (@Name, @Price, @CategoryID)";
            _conn.Execute(sql, new { Name = productToInsert.Name, Price = productToInsert.Price, CategoryID = productToInsert.CategoryId });
		}

		public void UpdateProduct(Product product)
        {
            string sql = "UPDATE products SET Name = @Name, Price = @Price WHERE ProductID = @Id";
            _conn.Execute(sql, new {Name = product.Name, Price = product.Price, Id = product.ProductId });
        }


    }
}
