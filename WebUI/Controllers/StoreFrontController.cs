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
    public class StoreFrontController : Controller
    {
        static List<LineItems> test = new List<LineItems>();
        private readonly InterfaceBL iObj;
        public StoreFrontController(InterfaceBL p_Inter)
        {
            iObj = p_Inter;
        }

        // GET: RestaurantController
        public ActionResult Index()

        {
            return View();
        }
        public ActionResult LogOut()

        {
            SingletonVM.currentuser = null;
            test.Clear();

            return RedirectToAction("Index", "Home");

        }
        public ActionResult SelectStore(int p_sid)
        {

            return View(iObj.GetAllStoreFrontsBL()
                        .Select(rest => new StoreFrontVM(rest))
                        .ToList()
            );
        }
        public ActionResult AddItemToStore(int p_sid)
        {
            ViewBag.StoreIdentifier = p_sid;
            //Passing the restaurant to the ReplenishInventory view
            return View(iObj.GetAllProductsBL()
                        .Select(rest => new ProductsVM(rest))
                        .ToList());
        }

        [HttpGet]
        public IActionResult ConfirmAddition(int p_ProductId, int p_quantity, int p_StoreId)
        {
            ViewBag.Amount = p_quantity;
            ViewBag.Store = p_StoreId;
            ViewBag.Product = p_ProductId;

            return View(new ProductsVM(iObj.GetProduct(p_ProductId)));
        }


        // POST: RestaurantController/ShowProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmAddition(int p_id, int p_quantity, int p_sid, IFormCollection collection)
        {


            try
            {
                iObj.AddStock(p_sid, p_id, p_quantity);
                return RedirectToAction("SelectStore", "StoreFront");
            }
            catch (System.Exception)
            {
                ViewBag.Error = "This Store Already has this item";


                return View();
            }

        }





        public ActionResult StoreItems(int p_id, int p_sid)
        {

            return View(iObj.GetInventory(p_id)
                        .Select(rest => new LineItemVM(rest))
                        .ToList());
        }


        [HttpGet]
        public IActionResult EditStock(int p_id, int p_quantity, int p_sid)
        {
            ViewBag.Amount = p_quantity;
            ViewBag.Store = p_sid;
            ViewBag.Product = p_id;

            return View(new ProductsVM(iObj.GetProduct(p_id)));
        }


        // POST: RestaurantController/ShowProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStock(int p_id, int p_quantity, int p_sid, IFormCollection collection)
        {

            try
            {
                iObj.ModifyStockTable(p_sid, p_id, p_quantity);

                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception)
            {

                return View();
            }
        }

        public ActionResult ShowStoreHistory(int p_sid)
        {

            return View(iObj.GetStoreOrderHistory(p_sid)
                        .Select(rest => new OrdersVM(rest))
                        .ToList()
            );
        }




        public ActionResult ShoppingIndex(int p_sid)

        {
            return View(iObj.GetAllStoreFrontsBL()
                        .Select(rest => new StoreFrontVM(rest))
                        .ToList());
        }
        public ActionResult SeeItems(int p_id, int p_sid, double p_price, string p_name)
        {

            return View(iObj.GetInventory(p_id)
                        .Select(rest => new LineItemVM(rest))
                        .ToList());
        }


        [HttpGet]
        public ActionResult Cart(int p_id, int p_sid, double p_price, string p_name, int p_available)
        {
            if (p_sid == 0)
            {
                decimal cost = 0;
                for (int i = 0; i < test.Count; i++)
                {
                    cost = cost + test[i].Quantity * test[i].Product_obj.Price;

                }


                ViewBag.purchase = cost;


                return View(test.Select(rest => new LineItemVM(rest))
                        .ToList());
            }
            else
            {

                LineItems cartitem = new LineItems();
                cartitem.ProductID = p_id;
                cartitem.StoreID = p_sid;
                cartitem.Quantity = 1;
                Products x = new Products();
                x.Name = p_name;
                x.Price = Convert.ToDecimal(p_price);
                cartitem.Product_obj = x;
                bool verified = false;
                for (int i = 0; i < test.Count; i++)
                {
                    LineItems stock = iObj.VerifyStock(test[i].ProductID, test[i].StoreID);

                    if (test[i].ProductID == p_id)
                    {
                        verified = true;
                        ViewBag.Message = "You already have this product in cart";
                    }
                    if (test[i].StoreID != p_sid)
                    {
                        verified = true;
                        ViewBag.Message = "Do not add product from a different store";
                    }
                    if (stock.Quantity - test[i].Quantity < 0)
                    {
                        verified = true;
                        ViewBag.Message = "One of your items has less than what you are asking for, please modify";
                        test[i].Quantity = 1;
                    }


                }
                if (p_available <= 0)
                {
                    verified = true;
                    ViewBag.Message = "This item is currently out of stock";
                }
                if (verified == false)
                {
                    test.Add(cartitem);
                }


                decimal cost = 0;
                for (int i = 0; i < test.Count; i++)
                {
                    cost = cost + test[i].Quantity * test[i].Product_obj.Price;

                }

                ViewBag.purchase = cost;
                if (SingletonVM.currentuser.Currency - cost < 0)
                {
                    ViewBag.Notify = "You do not have sufficient funds in your account select less items or add more money";
                }


                return View(test.Select(rest => new LineItemVM(rest))
                            .ToList());

            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cart(int[] p_quantity, int p_StoreId, int[] p_ProductId, decimal p_total)
        {

            Orders nuevo = new Orders();
            for (int i = 0; i < p_ProductId.Length; i++)
            {
                nuevo.Total = nuevo.Total + (p_quantity[i] * iObj.FindProductPrice(p_ProductId[i]));

            }

            decimal cost = nuevo.Total;
            for (int x = 0; x < p_ProductId.Length; x++)
            {
                test[x].Quantity = p_quantity[x];
            }

            return RedirectToAction("Cart", "StoreFront");

        }

        public IActionResult RemoveCart(int p_StoreId, int p_ProductId)
        {
            test.RemoveAll(x => x.ProductID == p_ProductId);

            return RedirectToAction("Cart", "StoreFront");

        }



        [HttpGet]

        public IActionResult ProceedToCheckout(decimal p_total)
        {
            ViewBag.xyz = p_total;
            if (SingletonVM.currentuser.Currency - p_total < 0)
            {
                return RedirectToAction("Cart", "StoreFront");
            }
            else
            {
                for (int i = 0; i < test.Count; i++)
                {
                    LineItems stock = iObj.VerifyStock(test[i].ProductID, test[i].StoreID);

                    if (stock.Quantity - test[i].Quantity < 0)
                    {

                        ViewBag.Message = "One of your items has less than what you are asking for, please modify";
                        test[i].Quantity = 1;
                    }


                }

                return View(test.Select(rest => new LineItemVM(rest)).ToList());
                                                                    
            }




        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmPurchase(int[] p_quantity, int p_StoreId, int[] p_ProductId)
        {
            Orders nuevo = new Orders();
            for (int i = 0; i < p_ProductId.Length; i++)
            {
                LineItems stock = iObj.VerifyStock(test[i].ProductID, p_StoreId);
                nuevo.Total = nuevo.Total + (p_quantity[i] * iObj.FindProductPrice(p_ProductId[i]));
                stock.Quantity=stock.Quantity-p_quantity[i];
                iObj.ModifyStockTable(p_StoreId,p_ProductId[i],stock.Quantity);

            }
            nuevo.CustomerId = SingletonVM.currentuser.Id;
            nuevo.StoreId = p_StoreId;
            SingletonVM.currentuser.Currency=SingletonVM.currentuser.Currency-nuevo.Total;
            iObj.ModifyCustomerRecord(new Customer()
                {
                    Id = SingletonVM.currentuser.Id,
                    Name = SingletonVM.currentuser.Name,
                    Address = SingletonVM.currentuser.Address,
                    Email = SingletonVM.currentuser.Contact,
                    UserName = SingletonVM.currentuser.UserName,
                    Password = SingletonVM.currentuser.Password,
                    Age = SingletonVM.currentuser.Age,
                    Category = SingletonVM.currentuser.Position,
                    CurrentCurrency = SingletonVM.currentuser.Currency
                });


            iObj.AddOrdersBL(nuevo);
            nuevo = iObj.GetOrderID(nuevo);

            for (int x = 0; x < p_ProductId.Length; x++)
            {
                iObj.InsertHistory(p_StoreId, p_ProductId[x], nuevo.OrderId, SingletonVM.currentuser.Id, p_quantity[x]);
            }
            test.Clear();

            return RedirectToAction("MyProfile", "Customer");

        }




        [HttpGet]
        public IActionResult Search()
        {
            ViewBag.testing = null;
            return View();
        }

        [HttpPost]
        public IActionResult Search(string SearchWord)
        {
            ViewBag.testing = "show";




            return View(iObj.SearchStores(SearchWord)
                    .Select(rest => new StoreFrontVM(rest))
                    .ToList()
        );

        }

        [HttpGet]
        public IActionResult CreateStore()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateStore(StoreFrontVM p_StoreVM)
        {
            //This if statement will check if the current model that is being passed through is valid
            //If not, the asp-validation-for attribute elements will appear and autofill in the proper feedback for the user 
            //to correct themselves
            if (ModelState.IsValid)
            {
                iObj.AddStoreFrontBL(new StoreFront()
                {
                    StoreName = p_StoreVM.Name,
                    Location = p_StoreVM.Address,
                });

                return RedirectToAction(nameof(SelectStore));
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
        public IActionResult CreateProduct(ProductsVM p_ViewModel)
        {
            //This if statement will check if the current model that is being passed through is valid
            //If not, the asp-validation-for attribute elements will appear and autofill in the proper feedback for the user 
            //to correct themselves
            if (ModelState.IsValid)
            {
                iObj.AddProductsBL(new Products()
                {
                    Name = p_ViewModel.Name,
                    Price = p_ViewModel.Price,
                    Description = p_ViewModel.Description,
                    Category = p_ViewModel.Category
                });

                return RedirectToAction(nameof(SelectStore));
            }

            //Will return back to the create view if the user didn't specify the right input
            return View();
        }
    }
}