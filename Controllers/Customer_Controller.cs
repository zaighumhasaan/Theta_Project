using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Pharmacy_POS.Models;
using System.Reflection;

namespace Pharmacy_POS.Controllers
{
    public class Customer_Controller : Controller
    {
        private readonly PharmacyPOSContext _dbcontext;

        public Customer_Controller(PharmacyPOSContext context)
        {
            _dbcontext = context;
        }


        [HttpGet]
        public ActionResult Add_Customer()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Add_Customer(string cust)
        {
            Customer customer = JsonConvert.DeserializeObject<Customer>(cust, new IsoDateTimeConverter());

            if (cust != null)
            {

                _dbcontext.Customers.Add(customer);
                _dbcontext.SaveChanges();

            }

            
            return Json(true);

        }
        //public IActionResult Add_Customer(Customer cust)
        //{
        //    try
        //    {

        //        _dbcontext.Customers.Add(cust);
        //        _dbcontext.SaveChanges();
        //        ViewBag.SMESSAGE = "Data Saved Successfully";
        //    }
        //    catch (AmbiguousMatchException)
        //    {
        //        ViewBag.EMESSAGE = "Some Error Occur ! Please Try Again";

        //    }

        //    return View(cust);
        //}

        [HttpGet]
        public IActionResult Update_Customer(int id)
        {
            var customer = _dbcontext.Customers.Where(a=> a.CustomerId==id).FirstOrDefault();
            return View(customer);


        }
        [HttpPost]
        public JsonResult Update_Customer(string cust)
        {
            
            try
            {
                Customer customer = JsonConvert.DeserializeObject<Customer>(cust, new IsoDateTimeConverter());

                if (customer!=null)
                {
                    _dbcontext.Customers.Update(customer);
                    _dbcontext.SaveChanges();
                    TempData["SMESSAGE"] = "Data Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                TempData["EMESSAGE"] = "Some Error Occur ! Please try Agin ";
            }
            return Json(RedirectToAction(nameof(Customer_Controller.All_Customer)));
        }

        [HttpGet]
        public IActionResult All_Customer()
        {

            try
            {

                ViewBag.DSMESSAGE = TempData["DSMESSAGE"];
                IList<Customer> Cust_List = _dbcontext.Customers.ToList();
                return View(Cust_List);
            }
            catch (AmbiguousMatchException)
            {

            }
            return View();
        }

        [HttpGet]
        public JsonResult Delete_Customer(int Id)
        {
            var customer = _dbcontext.Customers.Where(x => x.CustomerId == Id).FirstOrDefault();
            _dbcontext.Customers.Remove(customer);
            _dbcontext.SaveChanges();
            return Json(true);
        }


        //[HttpPost]
        //public JsonResult Delete_Customer(int id)
        //{
        //    var customer =_dbcontext.Customers.Where(x=>x.CustomerId == id).FirstOrDefault();
        //    _dbcontext.Customers.Remove(customer);
        //    _dbcontext.SaveChanges();
        //    return Json(true);

        //    //try
        //    //{
        //    //    Customer cust= _dbcontext.Customers.Find(id);
        //    //    if (cust != null)
        //    //    {
        //    //        _dbcontext.Customers.Remove(cust);
        //    //        _dbcontext.SaveChanges();
        //    //        return RedirectToAction("All_Customer");
        //    //    }
        //    //    else if (cust == null)
        //    //    {
        //    //        return View();
        //    //    }



        //    //}
        //    //catch (AmbiguousMatchException)
        //    //{
        //    //}
        //    //return View();
        //}
        [HttpGet]
        public IActionResult Customer_Detail(int id)
        {
            try
            {
                Customer cust = _dbcontext.Customers.Find(id);
                if (cust != null)
                {
                    return View(cust);
                }


            }
            catch (AmbiguousMatchException)
            {

            }

            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
