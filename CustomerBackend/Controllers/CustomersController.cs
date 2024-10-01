using CustomerBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CustomerBackend.Controllers
{
    public class CustomersController : ApiController
    {
        private static List<Customer> customers = new List<Customer>
            {
                new Customer() { CustomerID = 1, CustomerName = "John Doe", CustomerEmail = "John.Doe@mail.com", CustomerPhone = "071111111", CreatedDate = DateTime.Now },
                new Customer() { CustomerID = 2, CustomerName = "James Smith", CustomerEmail = "James.Smith@anothermail.com", CustomerPhone = "072222222", CreatedDate = DateTime.Now }
            };

        public IHttpActionResult GetCustomers()
        {
            return Ok(customers);
        }

        public IHttpActionResult GetCustomerById(int id)
        {
            var customer = customers.FirstOrDefault(x => x.CustomerID == id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public IHttpActionResult CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null || string.IsNullOrEmpty(customer.CustomerName))
                return BadRequest("Customer data invalid.");

            int custId = customers.Count > 0 ? customers.Max(c => c.CustomerID) + 1 : 1;

            customer.CustomerID = custId;
            customer.CreatedDate = DateTime.Now;
            customers.Add(customer);

            return CreatedAtRoute("DefaultApi", new { id = customer.CustomerID }, customer);
        }


        [HttpPut]
        public IHttpActionResult EditCustomer(int id, [FromBody] Customer customer)
        {
            var existingCustomer = customers.FirstOrDefault(c => c.CustomerID == id);
            if (existingCustomer == null)
                return NotFound();

            existingCustomer.CustomerName = customer.CustomerName;
            existingCustomer.CustomerEmail = customer.CustomerEmail;
            existingCustomer.CustomerPhone = customer.CustomerPhone;
            existingCustomer.UpdatedDate = DateTime.Now;

            return Ok(existingCustomer);
        }

        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customer = customers.FirstOrDefault(c => c.CustomerID == id);
            if (customer == null)
                return NotFound();

            customers.Remove(customer);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
