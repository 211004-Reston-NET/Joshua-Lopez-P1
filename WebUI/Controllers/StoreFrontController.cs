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
        private InterfaceBL iObj;
        public StoreFrontController(InterfaceBL p_Inter)
        {
            iObj = p_Inter;
        }

        // GET: RestaurantController
        public ActionResult Index()

        {
            return View();
        }
        public ActionResult SelectStore(int p_sid)
        {
            //We got our list of restaurant from our business layer
            //We converted that Model restaurant into RestaurantVM using Select method
            //Finally we changed it to a List with ToList()
            return View(iObj.GetAllStoreFrontsBL()
                        .Select(rest => new StoreFrontVM(rest))
                        .ToList()
            );
        }
        public ActionResult AddItemToStore(int p_sid)
        {
            ViewBag.StoreIdentifier=p_sid;
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
            
            
           
                iObj.AddStock(p_sid, p_id, p_quantity);
                // Restaurant toBeDeleted = _restBL.GetRestaurantById(Id);
                // _restBL.DeleteRestaurant(toBeDeleted);
                return RedirectToAction("SelectStore", "StoreFront");
            



        }





        public ActionResult StoreItems(int p_id, int p_sid)
        {
            //Passing the restaurant to the ReplenishInventory view
            return View(iObj.GetInventory(p_id)
                        .Select(rest => new LineItemVM(rest))
                        .ToList());
        }
        // GET: RestaurantController/Delete/5

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
            // int store=p_sid;
            // int prod=p_sid;
            // int total=p_quantity;
            try
            {
                iObj.ModifyStockTable(p_sid, p_id, p_quantity);
                // Restaurant toBeDeleted = _restBL.GetRestaurantById(Id);
                // _restBL.DeleteRestaurant(toBeDeleted);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception)
            {

                return View();
            }
        }




        public ActionResult ShoppingIndex(int p_sid)

        {
            return View(iObj.GetAllStoreFrontsBL()
                        .Select(rest => new StoreFrontVM(rest))
                        .ToList());
        }
        public ActionResult SeeItems(int p_id, int p_sid, double p_price, string p_name)
        {
            //Passing the restaurant to the ReplenishInventory view
            return View(iObj.GetInventory(p_id)
                        .Select(rest => new LineItemVM(rest))
                        .ToList());
        }


        [HttpGet]
        public ActionResult Cart(int p_id, int p_sid, double p_price, string p_name)
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
                Products x = new Products();
                x.Name = p_name;
                x.Price = Convert.ToDecimal(p_price);
                cartitem.Product_obj = x;

                test.Add(cartitem);
                decimal cost = 0;
                for (int i = 0; i < test.Count; i++)
                {
                    cost = cost + test[i].Quantity * test[i].Product_obj.Price;

                }


                ViewBag.purchase = cost;

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


            decimal cost = 0;
            cost = nuevo.Total;
            for (int x = 0; x < p_ProductId.Length; x++)
            {
                test[x].Quantity = p_quantity[x];
            }
            p_total = cost;

            return RedirectToAction("Cart", "StoreFront");

        }

        [HttpGet]

        public IActionResult ProceedToCheckout(decimal p_total)
        {
            ViewBag.xyz = p_total;




            // test = iObj.GetOrderID(test);
            return View(test.Select(rest => new LineItemVM(rest))
                        .ToList());

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmPurchase(int[] p_quantity, int p_StoreId, int[] p_ProductId)
        {
            Orders nuevo = new Orders();
            for (int i = 0; i < p_ProductId.Length; i++)
            {
                nuevo.Total = nuevo.Total + (p_quantity[i] * iObj.FindProductPrice(p_ProductId[i]));

            }
            nuevo.CustomerId = SingletonVM.currentuser.Id;
            nuevo.StoreId = p_StoreId;

            iObj.AddOrdersBL(nuevo);
            nuevo = iObj.GetOrderID(nuevo);
            for (int x = 0; x < p_ProductId.Length; x++)
            {
                iObj.InsertHistory(p_StoreId, p_ProductId[x], nuevo.OrderId, SingletonVM.currentuser.Id, p_quantity[x]);
            }
            decimal cost = nuevo.Total;
            ViewBag.purchase = cost;
            test.Clear();

            // test = iObj.GetOrderID(test);
            return RedirectToAction("MyProfile", "Customer");

        }


        // POST: RestaurantController/ShowProduct/5







        // [HttpGet]
        // public IActionResult Purchase(int p_id, int p_quantity, int p_sid, double p_price)
        // {
        //     ViewBag.Amount = p_quantity;
        //     ViewBag.Store = p_sid;
        //     ViewBag.Product = p_id;
        //     ViewBag.Price = p_price;


        //     return View(new ProductsVM(iObj.GetProduct(p_id)));
        // }


        // // POST: RestaurantController/ShowProduct/5
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public ActionResult Purchase(int p_id, int p_quantity, int p_sid, double p_price, IFormCollection collection)
        // {
        //     // int store=p_sid;
        //     // int prod=p_sid;
        //     // int total=p_quantity;
        //     try
        //     {


        //         Orders test = new Orders();
        //         test.StoreId = p_sid;
        //         test.CustomerId = SingletonVM.currentuser.Id;
        //         test.Total = Convert.ToDecimal(p_quantity * p_price);

        //         iObj.AddOrdersBL(test);

        //         iObj.InsertHistory(p_sid, p_id, test.OrderId, SingletonVM.currentuser.Id, p_quantity);
        //         // Restaurant toBeDeleted = _restBL.GetRestaurantById(Id);
        //         // _restBL.DeleteRestaurant(toBeDeleted);
        //         return RedirectToAction("Index", "Customer");
        //     }
        //     catch (System.Exception)
        //     {

        //         return View();
        //     }

        // }





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