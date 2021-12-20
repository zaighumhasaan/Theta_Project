using Microsoft.AspNetCore.Mvc;
using Pharmacy_POS.Models;
using System.Reflection;

namespace Pharmacy_POS.Controllers
{
    public class On_Sale_Invoice_Controller : Controller
    {
        private readonly PharmacyPOSContext _dbcontext;

        public On_Sale_Invoice_Controller(PharmacyPOSContext context)
        {
            _dbcontext = context;
        }
        [HttpGet]
        public IActionResult Add_Sale_Invoice()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add_Sale_Invoice(OnSaleInvoice si)
        {
            try
            {

                _dbcontext.OnSaleInvoices.Add(si);
                _dbcontext.SaveChanges();
                ViewBag.SMESSAGE = "Data Saved Successfully";
            }
            catch (AmbiguousMatchException)
            {
                ViewBag.EMESSAGE = "Some Error Occur ! Please Try Again";

            }

            return View(si);
        }

        [HttpGet]
        public IActionResult Update_Sale_Invoice(int id)
        {
            try
            {

                ViewBag.SMESSAGE = TempData["SMESSAGE"];
                ViewBag.EMESSAGE = TempData["EMESSAGE"];
                OnSaleInvoice si = _dbcontext.OnSaleInvoices.Find(id);
                return View(si);


            }
            catch (AmbiguousMatchException)
            {
                return View();
            }


        }
        [HttpPost]
        public IActionResult Update_Sale_Invoice(OnSaleInvoice si)
        {
            try
            {

                _dbcontext.OnSaleInvoices.Update(si);
                _dbcontext.SaveChanges();
                TempData["SMESSAGE"] = "Data Updated Successfully";


            }
            catch (AmbiguousMatchException)
            {
                TempData["EMESSAGE"] = "Some Error Occur ! Please try Agin ";
            }
            return RedirectToAction(nameof(On_Sale_Invoice_Controller.Update_Sale_Invoice), new { si.SaleInvoiceId });
        }

        [HttpGet]
        public IActionResult All_Sale_Invoice()
        {
            try
            {

                ViewBag.DSMESSAGE = TempData["DSMESSAGE"];
                IList<OnSaleInvoice> Si_List = _dbcontext.OnSaleInvoices.ToList();
                return View(Si_List);
            }
            catch (AmbiguousMatchException)
            {

            }
            return View();
        }


        [HttpPost]
        public IActionResult Delete_Sale_Invoice(int id)
        {

            try
            {
                OnSaleInvoice si = _dbcontext.OnSaleInvoices.Find(id);
                if (si != null)
                {
                    _dbcontext.OnSaleInvoices.Remove(si);
                    _dbcontext.SaveChanges();
                    return RedirectToAction("All_Sale_Invoice");
                }
                else if (si == null)
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
        public IActionResult Sale_Invoice_Detail(int id)
        {
            try
            {
               OnSaleInvoice si = _dbcontext.OnSaleInvoices.Find(id);
                if (si != null)
                {
                    return View(si);
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
