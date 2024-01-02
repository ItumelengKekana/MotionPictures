using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using MovingPicturesV2.DataAccess.Repository.IRepository;
using MovingPicturesV2.Models;
using MovingPicturesV2.Models.ViewModels;

namespace MovingPicturesV2.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _hostEnvironment;

		public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_hostEnvironment = hostEnvironment;

		}

		// GET: ProductController
		public ActionResult Index()
		{
			return View();
		}

		//GET
		public ActionResult Upsert(int? id)
		{
			ProductVM productVM = new()
			{
				Product = new(),
				GenreList = _unitOfWork.Genre.GetAll().Select(i => new SelectListItem
				{
					Text = i.Name,
					Value = i.Id.ToString(),
				}),
				MediaTypeList = _unitOfWork.MediaType.GetAll().Select(i => new SelectListItem
				{
					Text = i.Name,
					Value = i.Id.ToString(),
				}),

			};


			if (id == null || id == 0)
			{
				//create product

				return View(productVM);
			}
			else
			{
				productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

				return View(productVM);
			}

		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Upsert(ProductVM obj, IFormFile? file)
		{

			if (ModelState.IsValid)
			{
				string wwwRootPath = _hostEnvironment.WebRootPath;
				if (file != null)
				{
					string fileName = Guid.NewGuid().ToString();
					var uploads = Path.Combine(wwwRootPath, @"images\products");
					var extension = Path.GetExtension(file.FileName);

					if (obj.Product.ImageURL != null)
					{
						var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageURL.TrimStart('\\')); // using 2 backslashes because one would count as an escape character
						if (System.IO.File.Exists(oldImagePath))
						{
							System.IO.File.Delete(oldImagePath);
						}
					}

					using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
					{
						file.CopyTo(fileStreams);
					}

					obj.Product.ImageURL = @"\images\products\" + fileName + extension;
				}

				if (obj.Product.Id == 0)
				{
					_unitOfWork.Product.Add(obj.Product);
				}
				else
				{
					_unitOfWork.Product.Update(obj.Product);
				}

				_unitOfWork.Save();
				TempData["success"] = "Product created successfully";
				return RedirectToAction("Index"); //redirects to an action in this controller. A second parameter would point to an external controller
			}
			return View(obj);
		}

		#region API CALLS
		[HttpGet]
		public ActionResult GetAll()
		{
			var productList = _unitOfWork.Product.GetAll(includeProperties: "Genre,MediaType");
			return Json(new { data = productList });
		}

		//POST
		[HttpDelete]
		public IActionResult Delete(int? id)
		{
			var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

			if (obj == null)
			{
				return Json(new { success = false, message = "Error while deleting" });
			}

			var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageURL.TrimStart('\\')); // using 2 backslashes because one would count as an escape character
			if (System.IO.File.Exists(oldImagePath))
			{
				System.IO.File.Delete(oldImagePath);
			}

			_unitOfWork.Product.Remove(obj);
			_unitOfWork.Save();
			return Json(new { success = true, message = "Delete Successful" });
		}

		#endregion
	}
}
