using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerBackend.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        [EmailAddress]
        public string CustomerEmail { get; set; }

        [Phone]
        public string CustomerPhone { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? UpdatedDate { get; set; } = null;
    }
}