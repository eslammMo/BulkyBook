using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        

        public ProductController(IUnitOfWork unitOfWork , IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment=hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
      
        //Get
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product=new(),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
                 u => new SelectListItem
                 {
                     Text = u.Name,
                     Value = u.Id.ToString(),
                 }),
                CategoryList = _unitOfWork.Category.GetAll().Select(
               u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.Id.ToString(),

               })
            };
           
            if (id == null || id == 0) 
            {   //add Product

                return View(productVM);
            }
            else
            {
                //update Product
                productVM.Product=_unitOfWork.Product.GetFirstOrDefault(u=> u.Id== id);
                return View(productVM);
            }
            
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath; 
                if(file != null)
                {
                    string fileName=Guid.NewGuid().ToString();
                    var uploads=Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);
                    //Update image
                    if (obj.Product.ImgeUrl != null) { 
                        var oldImgPath= Path.Combine(wwwRootPath, obj.Product.ImgeUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImgPath))
                        {
                            System.IO.File.Delete(oldImgPath);
                        }
                    }
                    using (var fileStream =new FileStream(Path.Combine(uploads,fileName+extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.Product.ImgeUrl= @"\images\products\"+fileName+extension;   
                }
                if (obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                    TempData["success"] = "Product Created Successfully";
                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                    TempData["success"] = "Product Updataed Successfully";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
       
       
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includedProperties:"Category,CoverType");
            return Json(new { data = productList });
        }
        //Post
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj=_unitOfWork.Product.GetFirstOrDefault(u=>u.Id==id);
            if (obj == null)
            {
                return Json(new {success=false , message="Error while deleting"});
            }
            var oldImgPath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImgeUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImgPath))
            {
                System.IO.File.Delete(oldImgPath);
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new {success=true , message="Delete Successful"});

        }

        #endregion

    }

}
