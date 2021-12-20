using Microsoft.AspNetCore.Mvc;
using Pharmacy_POS.Models;
using System.Reflection;

namespace Pharmacy_POS.Controllers
{
    public class Employee_Controller : Controller
    {

        private readonly PharmacyPOSContext _dbcontext;

        public Employee_Controller(PharmacyPOSContext context)
        {
            _dbcontext = context;
        }
        
        [HttpGet]
        public IActionResult Add_Employee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add_Employee(Employee emp)
        {
            try
            {

                _dbcontext.Employees.Add(emp);
                _dbcontext.SaveChanges();
                ViewBag.SMESSAGE = "Data Saved Successfully";
            }
            catch (AmbiguousMatchException)
            {
                ViewBag.EMESSAGE = "Some Error Occur ! Please Try Again";

            }

            return View(emp);
        }

        [HttpGet]
        public IActionResult Update_Employee(int id)
        {
            try
            {

                ViewBag.SMESSAGE = TempData["SMESSAGE"];
                ViewBag.EMESSAGE = TempData["EMESSAGE"];
                Employee emp = _dbcontext.Employees.Find(id);
                return View(emp);


            }
            catch (AmbiguousMatchException)
            {
                return View();
            }


        }
        [HttpPost]
        public IActionResult Update_Employee(Employee emp)
        {
            try
            {

                _dbcontext.Employees.Update(emp);
                _dbcontext.SaveChanges();
                TempData["SMESSAGE"] = "Data Updated Successfully";


            }
            catch (AmbiguousMatchException)
            {
                TempData["EMESSAGE"] = "Some Error Occur ! Please try Agin ";
            }
            return RedirectToAction(nameof(Employee_Controller.Update_Employee), new { emp.EmpId });
        }

        [HttpGet]
        public IActionResult All_Employee()
        {
            try
            {

                ViewBag.DSMESSAGE = TempData["DSMESSAGE"];
                IList<Employee> Emp_List = _dbcontext.Employees.ToList();
                return View(Emp_List);
            }
            catch (AmbiguousMatchException)
            {

            }
            return View();
        }


        [HttpPost]
        public IActionResult Delete_Employee(int id)
        {

            try
            {
                Employee emp = _dbcontext.Employees.Find(id);
                if (emp != null)
                {
                    _dbcontext.Employees.Remove(emp);
                    _dbcontext.SaveChanges();
                    return RedirectToAction("All_Employee");
                }
                else if (emp == null)
                {
                    return View();
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
