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
    public class FunctionController : Controller
    {
        static CustomerVM testing = new CustomerVM();
        private InterfaceBL iObj;
        public FunctionController(InterfaceBL p_Inter)
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

        public ActionResult AllOrders()
        {
            //We got our list of restaurant from our business layer
            //We converted that Model restaurant into RestaurantVM using Select method
            //Finally we changed it to a List with ToList()
            // return View(iObj.GetAllStoreFrontsBL()
            //             .Select(rest => new StoreFrontVM(rest))
            //             .ToList()
            // );
            return View(iObj.GetAllOrdersBL()
                        .Select(rest => new OrdersVM(rest))
                        .ToList());

        }
        public ActionResult AllStores()
        {
            //We got our list of restaurant from our business layer
            //We converted that Model restaurant into RestaurantVM using Select method
            //Finally we changed it to a List with ToList()
            return View(iObj.GetAllStoreFrontsBL()
                        .Select(rest => new StoreFrontVM(rest))
                        .ToList()
            );


        }
        public ActionResult AllProducts()
        {
            //We got our list of restaurant from our business layer
            //We converted that Model restaurant into RestaurantVM using Select method
            //Finally we changed it to a List with ToList()
            return View(iObj.GetAllProductsBL()
                        .Select(rest => new ProductsVM(rest))
                        .ToList()
            );


        }
        public ActionResult AllStock()
        {
            //We got our list of restaurant from our business layer
            //We converted that Model restaurant into RestaurantVM using Select method
            //Finally we changed it to a List with ToList()
            return View(iObj.GetInventory(1)
                        .Select(rest => new LineItemVM(rest))
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
        public IActionResult CreateStore()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateStore(StoreFrontVM restVM)
        {
            //This if statement will check if the current model that is being passed through is valid
            //If not, the asp-validation-for attribute elements will appear and autofill in the proper feedback for the user 
            //to correct themselves
            if (ModelState.IsValid)
            {
                iObj.AddStoreFrontBL(new StoreFront()
                {
                    StoreName = restVM.Name,
                    Location = restVM.Address,
                });

                return RedirectToAction(nameof(Index));
            }

            //Will return back to the create view if the user didn't specify the right input
            return View();
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductsVM restVM)
        {
            //This if statement will check if the current model that is being passed through is valid
            //If not, the asp-validation-for attribute elements will appear and autofill in the proper feedback for the user 
            //to correct themselves
            if (ModelState.IsValid)
            {
                iObj.AddProductsBL(new Products()
                {
                    Name = restVM.Name,
                    Price = restVM.Price,
                    Description = restVM.Description,
                    Category = restVM.Category
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

            //This if statement will check if the current model that is being passed through is valid
            //If not, the asp-validation-for attribute elements will appear and autofill in the proper feedback for the user 
            //to correct themselves
            string s1 = UserName;//
            string s2 = Password;//

            Customer test = new Customer();
            test = iObj.GetCustomer(s1,s2);
            testing.Id = test.Id;
            testing.Name = test.Name;
            testing.Address = test.Address;
            testing.Contact = test.Email;
            ViewBag.testname = testing.Name;

            // iObj.AddCustomersBL(new Customer()
            // {
            //     Name = restVM.Name,
            //     Address = restVM.Address,
            //     Email = restVM.Contact,
            //     UserName = restVM.UserName,
            //     Password=restVM.Password,
            //     Age=restVM.Age,
            //     Category=restVM.Position,
            //     CurrentCurrency=restVM.Currency
            // });



            //Will return back to the create view if the user didn't specify the right input
            return RedirectToAction(nameof(Index));
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