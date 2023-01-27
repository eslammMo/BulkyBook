using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Range(1,10000)]
        [DisplayName("List Price")]
        public Double ListPrice { get; set; }
        [Required]
        [Range(1, 10000)]
        [DisplayName("Pricer for 1-50")]
        public Double Price { get; set; }
        [Required]
        [Range(1, 10000)]
        [DisplayName("Pricer for 51-100")]
        public Double Price50 { get; set; }
        [Required]
        [Range(1, 10000)]
        [DisplayName("Pricer for 100+")]
        public Double Price100 { get; set; }
        [DisplayName("ImageURL")]
        [ValidateNever]
        public string ImgeUrl{ get; set; }
        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [Required]
        [DisplayName("Cover Type")]
        public int CoverTypeId { get; set; }
        [ValidateNever]
        [ForeignKey("CoverTypeId")]
        public CoverType CoverType { get; set; }

    }
}
