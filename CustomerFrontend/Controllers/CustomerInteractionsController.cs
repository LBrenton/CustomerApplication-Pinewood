using CustomerFrontend.Models;
using CustomerFrontend.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CustomerFrontend.Controllers
{
    public class CustomerInteractionsController : Controller
    {
        private readonly CustomerService _customerService;

        public CustomerInteractionsController()
        {
            _customerService = new CustomerService();
        }

        public async Task<ActionResult> Index(string sort, string searchString)
        {
            var customers = await _customerService.GetCustomersAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.CustomerName.ToLower().Contains(searchString.ToLower())).ToList();
            }

            switch (sort)
            {
                case "id":
                    customers = customers.OrderBy(c => c.CustomerID).ToList(); 
                    break;
                case "name":
                    customers = customers.OrderBy(c => c.CustomerName).ToList();
                    break;
                case "email":
                    customers = customers.OrderBy(c => c.CustomerEmail).ToList();
                    break;
                case "phone":
                    customers = customers.OrderBy(c => c.CustomerPhone).ToList();
                    break;
                default:
                    customers = customers.OrderBy(c => c.CustomerID).ToList();
                    break;
            }

            return View(customers);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var newCustomer = await _customerService.PostCustomer(customer);

                if (newCustomer.CustomerID != 0)
                    return RedirectToAction("Details", new { id = newCustomer.CustomerID });
            }

            return View(customer);
        }

        public async Task<ActionResult> Details(int id)
        {
            var customer = await _customerService.GetCustomerById(id);

            if (customer == null)
                return View("NotFound");

            return View(customer);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var customer = await _customerService.GetCustomerById(id);

            if (customer == null)
                return View("NotFound");

            return View(customer);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var edit = await _customerService.PutCustomerEdit(customer);

                if (edit != null)
                    return RedirectToAction("Details", new { id = edit.CustomerID });
            }

            return View(customer);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var customer = await _customerService?.GetCustomerById(id);

            if (customer == null)
                return View("NotFound");

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _customerService.DeleteCustomerById(id);
            return RedirectToAction("Index");
        }
    }
}