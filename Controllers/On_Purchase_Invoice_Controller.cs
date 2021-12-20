using Microsoft.AspNetCore.Mvc;
using Pharmacy_POS.Models;
using System.Reflection;

namespace Pharmacy_POS.Controllers
{
    public class On_Purchase_Invoice_Controller : Controller
    {
        private readonly PharmacyPOSContext _dbcontext;

        public On_Purchase_Invoice_Controller(PharmacyPOSContext context)
        {
            _dbcontext = context;
        }
        [HttpGet]
        public IActionResult Add_Purchase_Invoice()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add_Purchase_Invoice(OnPurchaseInvoice pi)
        {
            try
            {

                _dbcontext.OnPurchaseInvoices.Add(pi);
                _dbcontext.SaveChanges();
                ViewBag.SMESSAGE = "Data Saved Successfully";
            }
            catch (AmbiguousMatchException)
            {
                ViewBag.EMESSAGE = "Some Error Occur ! Please Try Again";

            }

            return View(pi);
        }

        [HttpGet]
        public IActionResult Update_Purchase_Invoice(int id)
        {
            try
            {

                ViewBag.SMESSAGE = TempData["SMESSAGE"];
                ViewBag.EMESSAGE = TempData["EMESSAGE"];
               OnPurchaseInvoice pi = _dbcontext.OnPurchaseInvoices.Find(id);
                return View(pi);


            }
            catch (AmbiguousMatchException)
            {
                return View();
            }


        }
        [HttpPost]
        public IActionResult Update_Purchase_Invoice(OnPurchaseInvoice pi)
        {
            try
            {

                _dbcontext.OnPurchaseInvoices.Update(pi);
                _dbcontext.SaveChanges();
                TempData["SMESSAGE"] = "Data Updated Successfully";


            }
            catch (AmbiguousMatchException)
            {
                TempData["EMESSAGE"] = "Some Error Occur ! Please try Agin ";
            }
            return RedirectToAction(nameof(On_Purchase_Invoice_Controller.Update_Purchase_Invoice), new { pi.PurchaseInvoiceId });
        }

        [HttpGet]
        public IActionResult All_Purchase_Invoice()
        {
            try
            {

                ViewBag.DSMESSAGE = TempData["DSMESSAGE"];
                IList<OnPurchaseInvoice> Pi_List = _dbcontext.OnPurchaseInvoices.ToList();
                return View(Pi_List);
            }
            catch (AmbiguousMatchException)
            {

            }
            return View();
        }


        [HttpPost]
        public IActionResult Delete_Purchase_Invoice(int id)
        {

            try
            {
                OnPurchaseInvoice pi = _dbcontext.OnPurchaseInvoices.Find(id);
                if (pi != null)
                {
                    _dbcontext.OnPurchaseInvoices.Remove(pi);
                    _dbcontext.SaveChanges();
                    return RedirectToAction("All_Purchase_Invoice");
                }
                else if (pi == null)
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
        public IActionResult Purchase_Invoice_Detail(int id)
        {
            try
            {
                OnPurchaseInvoice pi = _dbcontext.OnPurchaseInvoices.Find(id);
                if (pi != null)
                {
                    return View(pi);
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
