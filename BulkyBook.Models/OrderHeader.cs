using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public double OrderTotal { get; set; }

        public DateTime? OrderDate { get; set; }
        public DateTime? ShippingDate { get; set; }
        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Carrier { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentDueDate    { get; set; }
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String StreetAddress { get; set; }
        [Required]
        public String City { get; set; }
        [Required]
        public String State { get; set; }
        [Required]
        public String PostalCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

    }
}
