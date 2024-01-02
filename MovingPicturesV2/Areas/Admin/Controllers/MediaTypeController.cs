using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovingPicturesV2.DataAccess.Repository.IRepository;
using MovingPicturesV2.Models;

namespace MovingPicturesV2.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class MediaTypeController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public MediaTypeController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		// GET: MediaTypeController
		public ActionResult Index()
		{
			IEnumerable<MediaTypeModel> mediaTypesList = _unitOfWork.MediaType.GetAll();

			return View(mediaTypesList);
		}

		/*// GET: MediaTypeController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}*/

		// GET: MediaTypeController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: MediaTypeController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(MediaTypeModel mediaType)
		{
			if (ModelState.IsValid)
			{

				_unitOfWork.MediaType.Add(mediaType);
				_unitOfWork.Save();
				TempData["success"] = "Media Type created successfully";
				return RedirectToAction("Index"); //redirects to an action in this controller. A second parameter would point to an external controller
			}
			return View(mediaType);
		}

		// GET: MediaTypeController/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			var mediaTypeFirst = _unitOfWork.MediaType.GetFirstOrDefault(media => media.Id == id);

			if (mediaTypeFirst == null)
			{
				return NotFound();
			}

			return View(mediaTypeFirst);
		}

		// POST: MediaTypeController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(MediaTypeModel mediaType)
		{
			if (ModelState.IsValid)
			{

				_unitOfWork.MediaType.Update(mediaType);
				_unitOfWork.Save();
				TempData["success"] = "Media Type updated successfully";
				return RedirectToAction("Index"); //redirects to an action in this controller. A second parameter would point to an external controller
			}
			return View(mediaType);
		}

		// GET: MediaTypeController/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			var mediaTypeFirst = _unitOfWork.MediaType.GetFirstOrDefault(media => media.Id == id);

			if (mediaTypeFirst == null)
			{
				return NotFound();
			}

			return View(mediaTypeFirst);
		}

		// POST: MediaTypeController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DeletePOST(int? id)
		{
			var obj = _unitOfWork.MediaType.GetFirstOrDefault(media => media.Id == id);

			if (obj == null)
			{
				return NotFound();
			}

			_unitOfWork.MediaType.Remove(obj);
			_unitOfWork.Save();
			TempData["success"] = "Media Type deleted successfully";
			return RedirectToAction("Index"); //redirects to an action in this controller. A second parameter would point to an external controller

		}
	}
}
