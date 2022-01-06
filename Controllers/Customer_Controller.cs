using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Add_Customer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add_Customer(Customer cust)
        {
            try
            {

                _dbcontext.Customers.Add(cust);
                _dbcontext.SaveChanges();
                ViewBag.SMESSAGE = "Data Saved Successfully";
            }
            catch (AmbiguousMatchException)
            {
                ViewBag.EMESSAGE = "Some Error Occur ! Please Try Again";

            }

            return View(cust);
        }

        [HttpGet]
        public IActionResult Update_Customer(int id)
        {
            try
            {

                ViewBag.SMESSAGE = TempData["SMESSAGE"];
                ViewBag.EMESSAGE = TempData["EMESSAGE"];
                Customer cust = _dbcontext.Customers.Find(id);
                return View(cust);


            }
            catch (AmbiguousMatchException)
            {
                return View();
            }


        }
        [HttpPost]
        public IActionResult Update_Drugs(Customer cust)
        {
            try
            {

                _dbcontext.Customers.Update(cust);
                _dbcontext.SaveChanges();
                TempData["SMESSAGE"] = "Data Updated Successfully";


            }
            catch (AmbiguousMatchException)
            {
                TempData["EMESSAGE"] = "Some Error Occur ! Please try Agin ";
            }
            return RedirectToAction(nameof(Customer_Controller.Update_Customer), new { cust.CustomerId});
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




        [HttpPost]
        public IActionResult Delete_Customer(int id)
        {

            try
            {
                Customer cust= _dbcontext.Customers.Find(id);
                if (cust != null)
                {
                    _dbcontext.Customers.Remove(cust);
                    _dbcontext.SaveChanges();
                    return RedirectToAction("All_Customer");
                }
                else if (cust == null)
                {
                    return View();
                }



            }
            catch (AmbiguousMatchException)
            {
            }
            return View();
        }
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
