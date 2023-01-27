using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public String? StreetAddress { get; set; }
        public String? City { get; set; }

        public String? State { get; set; }

        public String? PhoneNumber { get; set; }
        public String? PostalCode { get; set;}

    }
}
