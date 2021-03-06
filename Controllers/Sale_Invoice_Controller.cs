using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Pharmacy_POS.Models;
using System.Reflection;

namespace Pharmacy_POS.Controllers
{
    public class Sale_Invoice_Controller : Controller
    {

        private readonly PharmacyPOSContext _dbcontext;

        public Sale_Invoice_Controller(PharmacyPOSContext context)
        {
            _dbcontext = context;
        }
        [HttpGet]
        public ActionResult Add_Sale()
        {
            ViewBag.SMeesage = TempData["SMessage"];
            ViewBag.EMessage = TempData["EMessage"];
            ViewBag.ListItems = _dbcontext.Drugs.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Add_Sale(String objData)
        {

            try
            {
                ViewSales objMain = JsonConvert.DeserializeObject<ViewSales>(objData, new IsoDateTimeConverter());
                
                
                objMain.objSale.Date = DateTime.Now;
                _dbcontext.SaleInvoices.Add(objMain.objSale);//Adding Sale Invoice 
                _dbcontext.SaveChanges();
                
                foreach (OnSaleInvoice drug in objMain.ListSaleLine)
                {
                   Drug objdrug = _dbcontext.Drugs.Find(drug.DrugId);
                    objdrug.Quantity = objdrug.Quantity - drug.DrugQuantity;

                    if(objdrug.Quantity < 1)
                    {
                        _dbcontext.Drugs.Remove(objdrug);
                        _dbcontext.SaveChanges();
                    }
                    
                    var saleid = objMain.objSale.SaleInvoiceId;


                    drug.DrugPriceTotal = objMain.objSale.NewPrice;
                    
                    drug.SaleInvoiceId = saleid;
                    
                    _dbcontext.OnSaleInvoices.Add(drug);
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
        public IActionResult Update_Sale_Invoice(int id)
        {
            try
            {

                ViewBag.SMESSAGE = TempData["SMESSAGE"];
                ViewBag.EMESSAGE = TempData["EMESSAGE"];
                SaleInvoice sale = _dbcontext.SaleInvoices.Find(id);
                return View(sale);


            }
            catch (AmbiguousMatchException)
            {
                return View();
            }


        }
        [HttpPost]
        public IActionResult Update_Sale_Invoice(SaleInvoice sale)
        {
            try
            {

                _dbcontext.SaleInvoices.Update(sale);
                _dbcontext.SaveChanges();
                TempData["SMESSAGE"] = "Data Updated Successfully";


            }
            catch (AmbiguousMatchException)
            {
                TempData["EMESSAGE"] = "Some Error Occur ! Please try Agin ";
            }
            return RedirectToAction(nameof(Sale_Invoice_Controller.Update_Sale_Invoice), new { sale.SaleInvoiceId});
        }

        [HttpGet]
        public IActionResult All_Sale_Invoices()
        {
            try
            {

                ViewBag.DSMESSAGE = TempData["DSMESSAGE"];
                IList<SaleInvoice> Sale_Invoice_List = _dbcontext.SaleInvoices.ToList();
                return View(Sale_Invoice_List);
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
                Drug drug = _dbcontext.Drugs.Find(id);
                if (drug != null)
                {
                    _dbcontext.Drugs.Remove(drug);
                    _dbcontext.SaveChanges();
                    return RedirectToAction("All_Drug");
                }
                else if (drug == null)
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
        public IActionResult Drug_Detail(int id)
        {
            try
            {
                Drug drug = _dbcontext.Drugs.Find(id);
                if (drug != null)
                {
                    return View(drug);
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
