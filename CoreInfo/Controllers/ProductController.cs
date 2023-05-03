using CoreInfo.Context;
using CoreInfo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CoreInfo.Controllers
{
    public class ProductController : Controller
    {
        private CustomerDbContext _customerDbContext;
        public ProductController(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _customerDbContext.Products.ToListAsync();
            return View(list);
        }

        [HttpGet]
        public JsonResult GetDetailsByID(int id)
        {
            var customer = _customerDbContext.Products
                                             .FirstOrDefault(p => p.ID.Equals(id));

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
        public JsonResult Add(Product product)
        {
            JsonResponseViewModel model = new JsonResponseViewModel();
            var result = _customerDbContext.Products.Add(product);
            int successResponse = _customerDbContext.SaveChanges();
            if (successResponse != 0)
            {
                model.ResponseCode = 0;
                model.ResponseMessage = JsonConvert.SerializeObject(product);
            }
            else
            {
                model.ResponseCode = 1;
                model.ResponseMessage = "Bir Hata Oluştu";
            }
            return Json(model);
        }


        [HttpPut]
        public JsonResult Update(Product product, int id)
        {
            JsonResponseViewModel model = new JsonResponseViewModel();
            var result = _customerDbContext.Products.FirstOrDefault(p => p.ID.Equals(id));
            if (result != null)
            {
                result.ProductName = product.ProductName;
                result.Price = product.Price;
                result.Stock = product.Stock;
                var p = _customerDbContext.SaveChanges();
                if (p != 0)
                {
                    model.ResponseCode = 0;
                    model.ResponseMessage = JsonConvert.SerializeObject(product);
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
            var result = _customerDbContext.Products.FirstOrDefault(p => p.ID.Equals(id));
            if (result != null)
            {
                _customerDbContext.Products.Remove(result);
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

        [HttpGet]
        public JsonResult Refresh()
        {
            return Json("Index");
        }

    }
}
