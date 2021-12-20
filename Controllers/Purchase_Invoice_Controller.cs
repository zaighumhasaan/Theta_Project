using Microsoft.AspNetCore.Mvc;
using Pharmacy_POS.Models;
using System.Reflection;

namespace Pharmacy_POS.Controllers
{
    public class Purchase_Invoice_Controller : Controller
    {

        private readonly PharmacyPOSContext _dbcontext;

        public Purchase_Invoice_Controller(PharmacyPOSContext context)
        {
            _dbcontext = context;
        }
        [HttpGet]
        public IActionResult Add_Purchase_Invoice()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add_Purchase_Invoice(PurchaseInvoice pi)
        {
            try
            {

                _dbcontext.PurchaseInvoices.Add(pi);
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
                PurchaseInvoice pi = _dbcontext.PurchaseInvoices.Find(id);
                return View(pi);


            }
            catch (AmbiguousMatchException)
            {
                return View();
            }


        }
        [HttpPost]
        public IActionResult Update_Purchase_Invoice(PurchaseInvoice pi)
        {
            try
            {

                _dbcontext.PurchaseInvoices.Update(pi);
                _dbcontext.SaveChanges();
                TempData["SMESSAGE"] = "Data Updated Successfully";


            }
            catch (AmbiguousMatchException)
            {
                TempData["EMESSAGE"] = "Some Error Occur ! Please try Agin ";
            }
            return RedirectToAction(nameof(Purchase_Invoice_Controller.Update_Purchase_Invoice), new { pi.PurchaseInvoiceId });
        }

        [HttpGet]
        public IActionResult All_Purchase_Invoices()
        {
            try
            {

                ViewBag.DSMESSAGE = TempData["DSMESSAGE"];
                IList<PurchaseInvoice>  Purchase_List = _dbcontext.PurchaseInvoices.ToList();
                return View(Purchase_List);
            }
            catch (AmbiguousMatchException)
            {

            }
            return View();
        }


        [HttpPost]
        public IActionResult Delete_Drug(int id)
        {

            try
            {
               PurchaseInvoice pi= _dbcontext.PurchaseInvoices.Find(id);
                if (pi != null)
                {
                    _dbcontext.PurchaseInvoices.Remove(pi);
                    _dbcontext.SaveChanges();
                    return RedirectToAction("All_Purchase");
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
               PurchaseInvoice pi = _dbcontext.PurchaseInvoices.Find(id);
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
