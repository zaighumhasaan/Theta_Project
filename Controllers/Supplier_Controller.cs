using Microsoft.AspNetCore.Mvc;
using Pharmacy_POS.Models;
using System.Reflection;

namespace Pharmacy_POS.Controllers
{
    public class Supplier_Controller : Controller
    {
        private readonly PharmacyPOSContext _dbcontext;

        public Supplier_Controller(PharmacyPOSContext context)
        {
            _dbcontext = context;
        }
        [HttpGet]
        public IActionResult Add_Supplier()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add_Supplier(Supplier sp)
        {
            try
            {

                _dbcontext.Suppliers.Add(sp);
                _dbcontext.SaveChanges();
                ViewBag.SMESSAGE = "Data Saved Successfully";
            }
            catch (AmbiguousMatchException)
            {
                ViewBag.EMESSAGE = "Some Error Occur ! Please Try Again";

            }

            return View(sp);
        }

        [HttpGet]
        public IActionResult Update_Supplier(int id)
        {
            try
            {

                ViewBag.SMESSAGE = TempData["SMESSAGE"];
                ViewBag.EMESSAGE = TempData["EMESSAGE"];
                Supplier sp = _dbcontext.Suppliers.Find(id);
                return View(sp);


            }
            catch (AmbiguousMatchException)
            {
                return View();
            }


        }
        [HttpPost]
        public IActionResult Update_Supplier(Supplier sp)
        {
            try
            {

                _dbcontext.Suppliers.Update(sp);
                _dbcontext.SaveChanges();
                TempData["SMESSAGE"] = "Data Updated Successfully";


            }
            catch (AmbiguousMatchException)
            {
                TempData["EMESSAGE"] = "Some Error Occur ! Please try Agin ";
            }
            return RedirectToAction(nameof(Supplier_Controller.Update_Supplier), new { sp.SupplierId });
        }

        [HttpGet]
        public IActionResult All_Supplier()
        {
            try
            {

                ViewBag.DSMESSAGE = TempData["DSMESSAGE"];
                IList<Supplier> Sup_List = _dbcontext.Suppliers.ToList();
                return View(Sup_List);
            }
            catch (AmbiguousMatchException)
            {

            }
            return View();
        }


        [HttpPost]
        public IActionResult Delete_Supplier(int id)
        {

            try
            {
                Supplier sp = _dbcontext.Suppliers.Find(id);
                if (sp != null)
                {
                    _dbcontext.Suppliers.Remove(sp);
                    _dbcontext.SaveChanges();
                    return RedirectToAction("All_Supplier");
                }
                else if (sp == null)
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
        public IActionResult Supplier_Detail(int id)
        {
            try
            {
                Supplier sp = _dbcontext.Suppliers.Find(id);
                if (sp != null)
                {
                    return View(sp);
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
