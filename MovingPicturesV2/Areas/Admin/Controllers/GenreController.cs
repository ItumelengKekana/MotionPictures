using Microsoft.AspNetCore.Mvc;
using MovingPicturesV2.DataAccess.Repository.IRepository;
using MovingPicturesV2.Models;

namespace MovingPicturesV2.Areas.Admin.Controllers
{
	[Area("Admin")]
    public class GenreController : Controller
    {
		private readonly IUnitOfWork _unitOfWork;

		public GenreController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		// GET: Admin/Genre
		public IActionResult Index()
        {
            IEnumerable<GenreModel> genreList = _unitOfWork.Genre.GetAll();

            return View(genreList);
        }

        // GET: Admin/Genre/Details/5
        /*public IActionResult Details(int? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            var genreModel = await _context.Genres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genreModel == null)
            {
                return NotFound();
            }

            return View(genreModel);
        }*/

        // GET: Admin/Genre/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Genre/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GenreModel genre)
        {
			if (genre.Name == genre.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "The Display Order cannot match the Name");
			}
			if (ModelState.IsValid)
			{

				_unitOfWork.Genre.Add(genre);
				_unitOfWork.Save();
				TempData["success"] = "Genre created successfully";
				return RedirectToAction("Index"); //redirects to an action in this controller. A second parameter would point to an external controller
			}
			return View(genre);
		}

        // GET: Admin/Genre/Edit/5
        public IActionResult Edit(int? id)
        {
			if (id == null || id == 0)
			{
				return NotFound();
			}

			var genreFirst = _unitOfWork.Genre.GetFirstOrDefault(genre => genre.Id == id);

			if (genreFirst == null)
			{
				return NotFound();
			}

			return View(genreFirst);
		}

        // POST: Admin/Genre/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GenreModel genre)
		{
			if (ModelState.IsValid)
			{

				_unitOfWork.Genre.Update(genre);
				_unitOfWork.Save();
				TempData["success"] = "Genre updated successfully";
				return RedirectToAction("Index"); //redirects to an action in this controller. A second parameter would point to an external controller
			}
			return View(genre);
		}

        // GET: Admin/Genre/Delete/5
        public IActionResult Delete(int? id)
        {
			if (id == null || id == 0)
			{
				return NotFound();
			}

			var genreFirst = _unitOfWork.Genre.GetFirstOrDefault(genre => genre.Id == id);

			if (genreFirst == null)
			{
				return NotFound();
			}

			return View(genreFirst);
		}

        // POST: Admin/Genre/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        {
			var genreFirst = _unitOfWork.Genre.GetFirstOrDefault(genre => genre.Id == id);

			if (genreFirst == null)
			{
				return NotFound();
			}

			_unitOfWork.Genre.Remove(genreFirst);
			_unitOfWork.Save();
			TempData["success"] = "Genre deleted successfully";
			return RedirectToAction("Index"); //redirects to an action in this controller. A second parameter would point to an external controller
		}

        /*private bool GenreModelExists(int id)
        {
          return (_context.Genres?.Any(e => e.Id == id)).GetValueOrDefault();
        }*/
    }
}
