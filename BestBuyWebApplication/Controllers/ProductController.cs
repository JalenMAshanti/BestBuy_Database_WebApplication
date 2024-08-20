using Microsoft.AspNetCore.Mvc;
using Testing.Abstractions;
using Testing.Models;

namespace Testing.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductRepository _repo;
		private readonly IReviewRepository _reviewRepo;

		public ProductController(IProductRepository repo, IReviewRepository reviewRepo)
		{
			_repo = repo;
			_reviewRepo = reviewRepo;	

		}
		public IActionResult Index()
		{
			var products = _repo.GetAllProducts();
			return View(products);
		}

		public IActionResult ViewProduct(int id) 
		{
			ProductReview productReview = new ProductReview();
			productReview.Product = _repo.GetProduct(id);
			productReview.Review = _reviewRepo.GetReviewsById(id);
			return View(productReview);
		}

		public IActionResult UpdateProduct(int id) 
		{ 
			Product prod = _repo.GetProduct(id);

			if (prod == null) 
			{
				return View("ProductNotFound");
			}

			return View(prod);		
		}

		public IActionResult UpdateProductToDatabase(Product product) 
		{
			_repo.UpdateProduct(product);
			return RedirectToAction("ViewProduct", new { id = product.ProductId });
		}

		public IActionResult InsertProduct() 
		{
			var prod = _repo.AssignCategory();
			return View(prod);
		}

		public IActionResult InsertProductToDatabase(Product productToInsert) 
		{
			_repo.InsertProduct(productToInsert);
			return RedirectToAction("Index");
		}

		public IActionResult DeleteProduct(Product product) 
		{
			_repo.DeleteProduct(product);
			return RedirectToAction("Index");
		}
	}
}
