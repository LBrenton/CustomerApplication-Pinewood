using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerFrontend.Models
{
    public class Customer
    {
        [DisplayName("Customer ID")]
        public int CustomerID { get; set; }

        [DisplayName("Customer Name")]
        [Required]
        public string CustomerName { get; set; } = string.Empty;

        [DisplayName("Customer Email")]
        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; }

        [DisplayName("Customer Phone")]
        [Required]
        [Phone]
        public string CustomerPhone { get; set; }

        [DisplayName("Date Added")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Last Updated")]
        [DataType(DataType.Date)]
        public DateTime? UpdatedDate { get; set; }
    }
}