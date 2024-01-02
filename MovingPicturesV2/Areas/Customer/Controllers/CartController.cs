using MovingPicturesV2.DataAccess.Repository.IRepository;
using MovingPicturesV2.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovingPicturesV2.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public ShoppingCartVM ShoppingCartVM { get; set; }

		public CartController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity; //type-casting to Claims Indentity when retreiving the user identity
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			ShoppingCartVM = new ShoppingCartVM()
			{
				ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "Product")
			};

			foreach (var cart in ShoppingCartVM.ListCart)
			{
				cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);

				ShoppingCartVM.CartTotal += (cart.Price * cart.Count);
			}

			return View(ShoppingCartVM);
		}

		public IActionResult Summary()
		{
			/*var claimsIdentity = (ClaimsIdentity)User.Identity; //type-casting to Claims Indentity when retreiving the user identity
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			ShoppingCartVM = new ShoppingCartVM()
			{
				ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "Product")
			};

			foreach (var cart in ShoppingCartVM.ListCart)
			{
				cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);

				ShoppingCartVM.CartTotal += (cart.Price * cart.Count);
			}*/

			return View();
		}


		public IActionResult Plus(int cartId)
		{
			var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId); //get the id of the current cart and track it
			_unitOfWork.ShoppingCart.IncrementCount(cart, 1); //increment the count by one when pressed
			_unitOfWork.Save(); //save the changes (NB!)

			return RedirectToAction(nameof(Index));
		}
		public IActionResult Minus(int cartId)
		{
			var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId); //get the id of the current cart and track it
			if (cart.Count <= 1)
			{
				_unitOfWork.ShoppingCart.Remove(cart); //remove the item from the cart when pressed if it's less or equal to 1
			}
			else
			{
				_unitOfWork.ShoppingCart.DecrementCount(cart, 1); //decrement the count by one when pressed
			}
			_unitOfWork.Save(); //save the changes (NB!)

			return RedirectToAction(nameof(Index));
		}
		public IActionResult Remove(int cartId)
		{
			var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId); //get the id of the current cart and track it
			_unitOfWork.ShoppingCart.Remove(cart); //remove the item from the cart when pressed
			_unitOfWork.Save(); //save the changes (NB!)

			return RedirectToAction(nameof(Index));
		}


		private double GetPriceBasedOnQuantity(double quantity, double price, double price50, double price100)
		{
			if (quantity <= 50)
			{
				return price;
			}
			else
			{
				if (quantity <= 100)
				{
					return price50;
				}
				return price100;
			}
		}
	}
}
