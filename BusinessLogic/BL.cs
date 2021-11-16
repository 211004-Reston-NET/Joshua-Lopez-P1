using System;
using Models;
using DataAccessLogic;
using System.Collections.Generic;


namespace BusinessLogic
{
    public class BL : InterfaceBL
    {

        private readonly InterfaceRepository _repo;
        //dependancy
       
        public BL(InterfaceRepository p_repo)
        {
            _repo = p_repo;
        }

        
        public Customer AddCustomersBL(Customer paramterobj)
        {
            return _repo.AddCustomersDL(paramterobj);
        }
        
        public List<Customer> GetAllCustomersBL()
        {
            return _repo.GetAllCustomersDL();
        }


    
        public StoreFront AddStoreFrontBL(StoreFront paramterobj)
        {
            return _repo.AddStoreFrontDL(paramterobj);
        }


        public List<StoreFront> GetAllStoreFrontsBL()
        {
            return _repo.GetAllStoreFrontDL();

        }

     
        public Orders AddOrdersBL(Orders paramterobj)
        {
            return _repo.AddOrdersDL(paramterobj);
        }

        
        public List<Orders> GetAllOrdersBL()
        {
            return _repo.GetAllOrdersDL();
        }

        public List<StoreFront> SearchStores(string name)
        {
            return _repo.SearchStoresDL(name);
        }


        public LineItems VerifyStock(int productnum, StoreFront chosen)
        {
           return _repo.VerifyStockDL(productnum,chosen);
        }

        public Customer ModifyCustomerRecord(Customer currentSelection)
        {
            return _repo.ModifyCustomerRecordDL(currentSelection);
        
        } 
        public void VerifyCredentials(String name,string password)
        {
            _repo.VerifyCredentials(name,password);
            
        }
        public Customer GetCustomer(string name,string password)
        {
            return _repo.GetCustomerDL(name,password);
        }

         
        public Products AddProductsBL(Products paramterobj)
        {
            return _repo.AddProductsDL(paramterobj);
        }


        
        public List<Products> GetAllProductsBL()
        {
           return _repo.GetAllProductsDL();
        }

        public LineItems AddStock(int storenumber, int productnumber, int quantity)
        {
            return _repo.AddStockToDB(storenumber,productnumber,quantity);
        }

        public bool VerifyProduct(int identification)
        {
            return _repo.VerifyProduct(identification);
        }

        public Products GetProduct(int obj)
        {
            return _repo.GetProduct(obj);
        }

        public List<LineItems> GetInventory(int obj)
        {
           return _repo.GetInventory(obj);
        }

        public StoreFront GetStoreByID(int number)
        {
            return _repo.GetStoreByID(number);
        }

        public bool VerifyStorebyID(int number)
        {
            throw new NotImplementedException();
        }

        public void InsertHistory(int store, int prod, int order, int customer,int quantity)
        {
            _repo.InsertHistory(store,prod,order,customer,quantity);
        }

        public Orders GetOrderID(Orders obj)
        {
            return _repo.GetOrderID(obj);
        }

        public void ModifyStockTable(int storenumber, int productnumber,int quantity)
        {
            _repo.ModifyStockTable(storenumber,productnumber,quantity);
        }

        public List<Orders> GetStoreOrderHistory(int objId)
        {
            return _repo.GetStoreOrderHistory(objId);
        }

        public decimal FindProductPrice(int p_productId)
        {
           return  _repo.FindProductPrice(p_productId);
        }

        public List<OrderLines> GetMyOrderHistory(int objId)
        {
            return _repo.GetMyOrderHistory(objId);
        }
    }
}