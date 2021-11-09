using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CustomerController : Controller
    {
        static CustomerVM testing = new CustomerVM();
        private InterfaceBL iObj;
        public CustomerController(InterfaceBL p_Inter)
        {
            iObj = p_Inter;
        }

        // GET: RestaurantController
        public ActionResult Index()

        {
            ViewBag.testname = testing.Name;
            return View();
        }

        public ActionResult AllCustomers()
        {
            //We got our list of restaurant from our business layer
            //We converted that Model restaurant into RestaurantVM using Select method
            //Finally we changed it to a List with ToList()
            return View(iObj.GetAllCustomersBL()
                        .Select(rest => new CustomerVM(rest))
                        .ToList()
            );


        }

        

        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCustomer(CustomerVM restVM)
        {
            //This if statement will check if the current model that is being passed through is valid
            //If not, the asp-validation-for attribute elements will appear and autofill in the proper feedback for the user 
            //to correct themselves
            if (ModelState.IsValid)
            {
                iObj.AddCustomersBL(new Customer()
                {
                    Name = restVM.Name,
                    Address = restVM.Address,
                    Email = restVM.Contact,
                    UserName = restVM.UserName,
                    Password = restVM.Password,
                    Age = restVM.Age,
                    Category = restVM.Position,
                    CurrentCurrency = restVM.Currency
                });

                return RedirectToAction(nameof(Index));
            }

            //Will return back to the create view if the user didn't specify the right input
            return View();
        }

        

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string UserName, string Password)
        {
            if (ModelState.IsValid)
            {


                string s1 = UserName;//
                string s2 = Password;//

                Customer test = new Customer();
                test = iObj.GetCustomer(s1, s2);
                testing.Id = test.Id;
                testing.Name = test.Name;
                testing.Address = test.Address;
                testing.Contact = test.Email;
                testing.UserName = test.UserName;
                testing.Password = test.Password;
                testing.Age = test.Age;
                testing.Position = test.Category;
                testing.Currency = test.CurrentCurrency;

                ViewBag.testname = testing.Name;


                return RedirectToAction(nameof(Index));
            }



            //Will return back to the create view if the user didn't specify the right input
            return View();
        }



        public ActionResult MyProfile()
        {
            ViewBag.Id = testing.Id;
            ViewBag.Name = testing.Name;
            ViewBag.Address = testing.Address;
            ViewBag.Contact = testing.Contact;
            ViewBag.UserName = testing.UserName;
            ViewBag.Password = testing.Password;
            ViewBag.Age = testing.Age;
            ViewBag.Position = testing.Position;
            ViewBag.Currency = testing.Currency;
            return View(iObj.GetMyOrderHistory(testing.Id)
                        .Select(rest => new OrdersVM(rest))
                        .ToList()
            );


        }


        public void SetCurrentCustomer(CustomerVM user)
        {
            // testing.Id = testing.Id;
            testing.Name = user.Name;
            testing.Address = user.Address;
            testing.Contact = user.Contact;
            testing.UserName = user.UserName;
            testing.Password = user.Password;
            testing.Age = user.Age;
            testing.Position = user.Position;
            testing.Currency = user.Currency;

        }

        [HttpGet]
        public IActionResult EditMyProfile()
        {
            ViewBag.Id = testing.Id;
            ViewBag.Name = testing.Name;
            ViewBag.Address = testing.Address;
            ViewBag.Contact = testing.Contact;
            ViewBag.UserName = testing.UserName;
            ViewBag.Password = testing.Password;
            ViewBag.Age = testing.Age;
            ViewBag.Position = testing.Position;
            ViewBag.Currency = testing.Currency;
            return View();
        }

        [HttpPost]
        public IActionResult EditMyProfile(CustomerVM restVM)
        {
            //This if statement will check if the current model that is being passed through is valid
            //If not, the asp-validation-for attribute elements will appear and autofill in the proper feedback for the user 
            //to correct themselves
            if (ModelState.IsValid)
            {
                iObj.ModifyCustomerRecord(new Customer()
                {
                    Id = testing.Id,
                    Name = restVM.Name,
                    Address = restVM.Address,
                    Email = restVM.Contact,
                    UserName = restVM.UserName,
                    Password = restVM.Password,
                    Age = restVM.Age,
                    Category = restVM.Position,
                    CurrentCurrency = restVM.Currency
                });
                SetCurrentCustomer(restVM);

                return RedirectToAction(nameof(Index));
            }

            //Will return back to the create view if the user didn't specify the right input
            return View();
        }


        // GET: RestaurantController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RestaurantController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RestaurantController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RestaurantController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RestaurantController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}