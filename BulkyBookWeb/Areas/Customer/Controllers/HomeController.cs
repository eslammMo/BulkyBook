using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger ,IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList= _unitOfWork.Product.GetAll(includedProperties:"Category,CoverType");
            return View(productList);
        }
        public IActionResult Details(int id)
        {
            ShoppingCart cartObj = new() {
                Product=_unitOfWork.Product.GetFirstOrDefault(x=>x.Id==id, includedProperties: "Category,CoverType"),
                Count=1,
            };
            return View(cartObj);
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