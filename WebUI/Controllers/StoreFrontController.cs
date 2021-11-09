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
        // static CustomerVM testing = new CustomerVM();
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


         public ActionResult StoreItems(int p_id,int p_sid)
        {
            //Passing the restaurant to the ReplenishInventory view
            return View(iObj.GetInventory(p_id)
                        .Select(rest => new LineItemVM(rest))
                        .ToList());
        }
        // GET: RestaurantController/Delete/5

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
           try
           {
                iObj.ModifyStockTable(p_sid,p_id,p_quantity);
                // Restaurant toBeDeleted = _restBL.GetRestaurantById(Id);
                // _restBL.DeleteRestaurant(toBeDeleted);
                return RedirectToAction(nameof(Index));
           }
           catch (System.Exception)
           {
               
               return View();
           }
                
            
          
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