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

        private readonly InterfaceBL iObj;
        public CustomerController(InterfaceBL p_Inter)
        {
            iObj = p_Inter;
        }

        // GET: RestaurantController
        public ActionResult Index()

        {
            ViewBag.testname = SingletonVM.currentuser.Name;
            ViewBag.Classified = SingletonVM.currentuser.Position;


            return View();
        }


        public ActionResult LogOut()

        {
            SingletonVM.currentuser = null;

            return RedirectToAction("Index", "Home");

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
                try
                {
                    test = iObj.GetCustomer(s1, s2);
                }
                catch (System.Exception)
                {

                    ViewBag.Message = "Username or password was not found";
                    return View();
                }


                CustomerVM x = new CustomerVM();
                x.Id = test.Id;
                x.Name = test.Name;
                x.Address = test.Address;
                x.Contact = test.Email;
                x.UserName = test.UserName;
                x.Password = test.Password;
                x.Age = test.Age;
                x.Position = test.Category;
                x.Currency = test.CurrentCurrency;
                SingletonVM.currentuser = x;

                ViewBag.testname = SingletonVM.currentuser.Name;


                return RedirectToAction("ShoppingIndex", "StoreFront");
            }



            //Will return back to the create view if the user didn't specify the right input
            return View();
        }



        public ActionResult MyProfile(int p_sort)
        {
            ViewBag.Id = SingletonVM.currentuser.Id;
            ViewBag.Name = SingletonVM.currentuser.Name;
            ViewBag.Address = SingletonVM.currentuser.Address;
            ViewBag.Contact = SingletonVM.currentuser.Contact;
            ViewBag.UserName = SingletonVM.currentuser.UserName;
            ViewBag.Password = SingletonVM.currentuser.Password;
            ViewBag.Age = SingletonVM.currentuser.Age;
            ViewBag.Position = SingletonVM.currentuser.Position;
            ViewBag.Currency = SingletonVM.currentuser.Currency;
            List<OrderLines> myorders = iObj.GetMyOrderHistory(SingletonVM.currentuser.Id);

            if (p_sort == 1)
            {
                IEnumerable<OrderLines> query = myorders.OrderBy(x => x.Order_obj.Total);
                return View(query.Select(rest => new OrdersVM(rest)).ToList());
            }
            else
            {
                return View(myorders
                        .Select(rest => new OrdersVM(rest))
                        .ToList());
            }




        }



        public void SetCurrentCustomer(CustomerVM user)
        {

            SingletonVM.currentuser.Name = user.Name;
            SingletonVM.currentuser.Address = user.Address;
            SingletonVM.currentuser.Contact = user.Contact;
            SingletonVM.currentuser.UserName = user.UserName;
            SingletonVM.currentuser.Password = user.Password;
            SingletonVM.currentuser.Age = user.Age;
            SingletonVM.currentuser.Position = user.Position;
            SingletonVM.currentuser.Currency = user.Currency;

        }

        [HttpGet]
        public IActionResult EditMyProfile()
        {
            ViewBag.Id = SingletonVM.currentuser.Id;
            ViewBag.Name = SingletonVM.currentuser.Name;
            ViewBag.Address = SingletonVM.currentuser.Address;
            ViewBag.Contact = SingletonVM.currentuser.Contact;
            ViewBag.UserName = SingletonVM.currentuser.UserName;
            ViewBag.Password = SingletonVM.currentuser.Password;
            ViewBag.Age = SingletonVM.currentuser.Age;
            ViewBag.Position = SingletonVM.currentuser.Position;
            ViewBag.Currency = SingletonVM.currentuser.Currency;
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
                    Id = SingletonVM.currentuser.Id,
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