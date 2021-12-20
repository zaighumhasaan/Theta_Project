using Microsoft.AspNetCore.Mvc;
using Pharmacy_POS.Models;
using System.Reflection;

namespace Pharmacy_POS.Controllers
{
    public class Stored_Drug_Controller : Controller
    {
        private readonly PharmacyPOSContext _dbcontext;

        public Stored_Drug_Controller(PharmacyPOSContext context)
        {
            _dbcontext = context;
        }

        [HttpGet]
        public IActionResult Store_Drugs()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Store_Drugs(StoredDrug drug)
        {
            try
            {
                _dbcontext.StoredDrugs.Add(drug);
                _dbcontext.SaveChanges();
                ViewBag.SMESSAGE = "New Drug Saved Successfully";
            }
            catch (AmbiguousMatchException)
            {
                ViewBag.EMESSAGE = "Some Error Occur ! Please Try Again";

            }

            return View(drug);
        }
        [HttpGet]
        public IActionResult Update_Stored_Drugs(int id)
        {
            try
            {
                StoredDrug drug = _dbcontext.StoredDrugs.Find(id);
                if(drug != null)
                {
                    return View(drug);
                }
                else
                {
                    return View();
                }
            }
            catch(AmbiguousMatchException)
            {

            }

            return View();
        }
        [HttpPost]
        public IActionResult Update_Stored_Drugs(StoredDrug drug)
        {
            try
            {
                _dbcontext.StoredDrugs.Update(drug);
                _dbcontext.SaveChanges();
                TempData["SMESSAGE"] = "Data Updated Successfully";
            }
            catch(AmbiguousMatchException)
            {
                TempData["EMESSAGE"] = "Some Error Occur ! Please Try Again. ";
            }

            return RedirectToAction(nameof(Stored_Drug_Controller.Update_Stored_Drugs), new { drug.DrugId });
        }
        [HttpGet]
        public IActionResult All_Stored_Drug()
        {
            try
            {

                ViewBag.DSMESSAGE = TempData["DSMESSAGE"];
                IList<StoredDrug> Drug_List = _dbcontext.StoredDrugs.ToList();
                return View(Drug_List);
            }
            catch (AmbiguousMatchException)
            {

            }
            return View();
        }
        [HttpPost]
        public IActionResult Delete_Stored_Drug(int id)
        {

            try
            {
                StoredDrug drug = _dbcontext.StoredDrugs.Find(id);
                if (drug != null)
                {
                    _dbcontext.StoredDrugs.Remove(drug);
                    _dbcontext.SaveChanges();
                    return RedirectToAction("All_Stored_Drug");
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
        public IActionResult Stored_Drug_Detail(int id)
        {
            try
            {
                StoredDrug drug = _dbcontext.StoredDrugs.Find(id);
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
