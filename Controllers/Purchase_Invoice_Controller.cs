using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        public IActionResult Add_Purchase_Invoice(String objData)
        {

            try
            {
                ViewPurchase objMain = JsonConvert.DeserializeObject<ViewPurchase>(objData, new IsoDateTimeConverter());


                objMain.objPurchase.Date = DateTime.Now;
                _dbcontext.PurchaseInvoices.Add(objMain.objPurchase);//Adding Sale Invoice 
                _dbcontext.SaveChanges();

                foreach (OnPurchaseInvoice drug in objMain.ListPurchaseLine)
                {
                    Drug objdrug = _dbcontext.Drugs.Find(drug.DrugId);

                    //objdrug.Quantity = objdrug.Quantity - drug.DrugQuantity; -->This statement was only for Sale 

             

                    //var saleid = objMain.objPurchase.PurchaseInvoiceId;


                    //drug.DrugPriceTotal = objMain.objPurchase.NewPrice;

                    //drug.SaleInvoiceId = saleid;

                    _dbcontext.OnPurchaseInvoices.Add(drug);
                    _dbcontext.SaveChanges();

                }


                TempData["SMessage"] = "Sale Added Successfully";
            }
            catch (Exception ex)
            {
                TempData["EMessage"] = "Some error occured. please try again";
            }

            return RedirectToAction(nameof(Sale_Invoice_Controller.Add_Sale));
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
