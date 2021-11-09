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
        //static CustomerVM testing = new CustomerVM();
        private InterfaceBL iObj;
        public FunctionController(InterfaceBL p_Inter)
        {
            iObj = p_Inter;
        }

        // GET: RestaurantController
        public ActionResult Index()

        {
            ViewBag.testname = SingletonVM.currentuser.Name;
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
        public ActionResult AllStores(int p_sid)
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
            if (ModelState.IsValid)
            {


                string s1 = UserName;//
                string s2 = Password;//

                Customer test = new Customer();
                test = iObj.GetCustomer(s1, s2);
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


                return RedirectToAction(nameof(Index));
            }



            //Will return back to the create view if the user didn't specify the right input
            return View();
        }



        public ActionResult MyProfile()
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
            return View(iObj.GetMyOrderHistory(SingletonVM.currentuser.Id)
                        .Select(rest => new OrdersVM(rest))
                        .ToList()
            );


        }


        public void SetCurrentCustomer(CustomerVM user)
        {
            // testing.Id = testing.Id;
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

        public ActionResult StoreItems(int p_id,int p_sid)
        {
            //Passing the restaurant to the ReplenishInventory view
            return View(iObj.GetInventory(p_id)
                        .Select(rest => new LineItemVM(rest))
                        .ToList());
        }
        // GET: RestaurantController/Delete/5
        public ActionResult ShowProduct(int p_id,int p_quantity,int p_sid)
        {
            ViewBag.Amount=p_quantity.ToString();
            ViewBag.Store=p_sid;
            
            //Passing the restaurant to the ShowProduct view
            return View(new ProductsVM(iObj.GetProduct(p_id)));
        }

        [HttpGet]
        public IActionResult EditStock(int p_id,int p_quantity,int p_sid)
        {
            ViewBag.Amount=p_quantity;
            ViewBag.Store=p_sid;
            ViewBag.Product=p_id;
            
            return View(new ProductsVM(iObj.GetProduct(p_id)));
        }


        // POST: RestaurantController/ShowProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStock(int p_id,int p_quantity,int p_sid, IFormCollection collection)
        {
            // int store=p_sid;
            // int prod=p_sid;
            // int total=p_quantity;
           
                iObj.ModifyStockTable(p_sid,p_id,p_quantity);
                // Restaurant toBeDeleted = _restBL.GetRestaurantById(Id);
                // _restBL.DeleteRestaurant(toBeDeleted);
                return RedirectToAction(nameof(Index));
            
          
        }

        










    }
}