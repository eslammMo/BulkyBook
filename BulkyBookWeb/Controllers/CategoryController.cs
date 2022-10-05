using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;


namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {   
        private readonly IUnitOfWork _unitOfWork;
        public Category category  ;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoriesList = _unitOfWork.Category.GetAll();

            return View(objCategoriesList);
        }
        //Get
        public  IActionResult  Create(){ 
            
            return View();
            }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category has Created";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //Get
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0) { return NotFound(); }

            Category selectedCategory = _unitOfWork.Category.GetFirstOrDefault(u=>u.Id==id);
            
            if(selectedCategory == null) { return NotFound(); }

            return View(selectedCategory);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category has Updated";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }

            Category selectedCategory = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);

            if (selectedCategory == null) { return NotFound(); }

            return View(selectedCategory);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category obj)
        {

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category has Deleted";
            return RedirectToAction("Index");
           
        }

    }
}
