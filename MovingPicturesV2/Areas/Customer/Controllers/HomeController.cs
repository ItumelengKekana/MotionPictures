using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovingPicturesV2.DataAccess.Repository.IRepository;
using MovingPicturesV2.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace MovingPicturesV2.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;

		public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			IEnumerable<ProductModel> productList = _unitOfWork.Product.GetAll(includeProperties: "Genre,MediaType");

			return View(productList);
		}

		public IActionResult Details(int productId)
		{
			ShoppingCart cartobj = new()
			{
				Count = 1,
				ProductId = productId,
				Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == productId, includeProperties: "Genre,MediaType"),
			};
			if (ModelState.IsValid)
			{

				return View(cartobj);
			}
			else
			{
				cartobj.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == productId, includeProperties: "Genre,MediaType");
				try
				{
					return View(cartobj);
				}
				catch
				{

					return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
				}

			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]  //Requires the user to log in before accessing this. (adding to a cart)
		public IActionResult Details(ShoppingCart shoppingCart)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity; //type-casting to Claims Indentity when retreiving the user identity
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier); //extracting the claim that we want (NameIdentifier)
			shoppingCart.ApplicationUserId = claim.Value;  //Assigning the user id to the shopping cart (available while the user is signed in)

			//retieving user id and product id to validate that we are working with the same cart
			ShoppingCart carFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId);

			if (carFromDb == null)
			{
				_unitOfWork.ShoppingCart.Add(shoppingCart);  //add a new product if it does not exist
			}
			else
			{
				_unitOfWork.ShoppingCart.IncrementCount(carFromDb, shoppingCart.Count);  //increment the count of the product if it does exist in the cart
			}

			_unitOfWork.Save();

			return RedirectToAction(nameof(Index));  //redirect to the index page after operation
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}