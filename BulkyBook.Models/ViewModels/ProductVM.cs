using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.ViewModels
{
    public class ProductVM
    {
        public Product Product { set; get; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CoverTypeList { set; get; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { set; get; }
    }
}