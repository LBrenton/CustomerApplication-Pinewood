using CustomerFrontend.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CustomerFrontend.Services
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri("https://localhost:44356/")
            };
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/customers");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var customers = JsonConvert.DeserializeObject<List<Customer>>(data);
                return customers;
            }

            return new List<Customer>();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/customers/" + id);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Customer>(data);
                return customer;
            }

            return null;
        }

        public async Task<Customer> PostCustomer(Customer customer)
        {
            var jsonCustomer = JsonConvert.SerializeObject(customer);
            var content = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"api/customers", content);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var createdCustomer = JsonConvert.DeserializeObject<Customer>(data);
                return createdCustomer;
            }

            return new Customer();
        }

        public async Task<Customer> PutCustomerEdit(Customer customer)
        {
            var jsonCustomer = JsonConvert.SerializeObject(customer);
            var content = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"api/customers/{customer.CustomerID}", content);

            if (response.IsSuccessStatusCode)
            {
                return customer;
            }

            return null;
        }

        public async Task<bool> DeleteCustomerById(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/customers/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}