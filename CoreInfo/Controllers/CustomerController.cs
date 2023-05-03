using CoreInfo.Context;
using CoreInfo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CoreInfo.Controllers
{
    public class CustomerController : Controller
    {
        private  CustomerDbContext _customerDbContext;
        public CustomerController(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _customerDbContext.Customers.ToListAsync();
            return View(list);
        }

        [HttpGet]
        public JsonResult GetDetailsByID(int id)
        {
            var customer = _customerDbContext.Customers
                                             .FirstOrDefault(p=>p.ID.Equals(id));

            JsonResponseViewModel model = new JsonResponseViewModel();
            if (customer != null)
            {
                model.ResponseCode = 0;
                model.ResponseMessage = JsonConvert.SerializeObject(customer);
            }
            else
            {
                model.ResponseCode = 1;
                model.ResponseMessage = "Bir Hata oluştu";
            }
            return Json(model);
        }

        [HttpPost]
        public JsonResult Add(Customer customer)
        {
            JsonResponseViewModel model = new JsonResponseViewModel();
            var result = _customerDbContext.Customers.Add(customer);
            int successResponse = _customerDbContext.SaveChanges();
            if (successResponse != 0)
            {
                model.ResponseCode = 0;
                model.ResponseMessage = JsonConvert.SerializeObject(customer);
            }
            else
            {
                model.ResponseCode = 1;
                model.ResponseMessage = "Bir Hata Oluştu";
            }
            return Json(model);
        }

        [HttpPut]
        public JsonResult Update(Customer customer,int id)
        {
            JsonResponseViewModel model = new JsonResponseViewModel();
            var result = _customerDbContext.Customers.FirstOrDefault(p=>p.ID.Equals(id));
            if (result != null)
            {
                result.Name = customer.Name;
                result.SurName = customer.SurName;
                result.Country = customer.Country;
                result.PhoneNumber = customer.PhoneNumber;
                var p = _customerDbContext.SaveChanges();
                if (p !=0)
                {
                    model.ResponseCode = 0;
                    model.ResponseMessage = JsonConvert.SerializeObject(customer);
                }
                else
                {
                    model.ResponseCode = 1;
                    model.ResponseMessage = "Bir Hata Oluştu";
                }
            }
            else
            {
                model.ResponseCode = 1;
                model.ResponseMessage = "Bir Hata Oluştu";
            }
            return Json(model);
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            JsonResponseViewModel model = new JsonResponseViewModel();
            var result = _customerDbContext.Customers.FirstOrDefault(p => p.ID.Equals(id));
            if (result != null)
            {
                _customerDbContext.Customers.Remove(result);
                int p = _customerDbContext.SaveChanges();
                if (p != 0)
                {
                    model.ResponseCode = 0;
                    model.ResponseMessage = JsonConvert.SerializeObject(result);
                }
                else
                {
                    model.ResponseCode = 1;
                    model.ResponseMessage = "Bir Hata Oluştu";
                }
            }
            else
            {
                model.ResponseCode = 1;
                model.ResponseMessage = "Bir Hata Oluştu";
            }
            return Json(model);
        }
    }
}
