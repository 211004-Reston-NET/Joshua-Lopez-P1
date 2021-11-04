using System;
using Models;
using DataAccessLogic;
using System.Collections.Generic;


namespace BusinessLogic
{
    public class BL : InterfaceBL
    {

        private InterfaceRepository _repo;
        //dependancy
       
        public BL(InterfaceRepository p_repo)
        {
            _repo = p_repo;
        }

        
        public Customer AddCustomersBL(Customer parameterObj)
        {
            return _repo.AddCustomersDL(parameterObj);
        }
        
        public List<Customer> GetAllCustomersBL()
        {
            return _repo.GetAllCustomersDL();
        }


    
        public StoreFront AddStoreFrontBL(StoreFront parameterObj)
        {
            return _repo.AddStoreFrontDL(parameterObj);
        }


        public List<StoreFront> GetAllStoreFrontsBL()
        {
            return _repo.GetAllStoreFrontDL();

        }

     
        public Orders AddOrdersBL(Orders parameterObj,StoreFront store, Customer client)
        {
            return _repo.AddOrdersDL(parameterObj,store,client);
        }

        
        public List<Orders> GetAllOrdersBL()
        {
            return _repo.GetAllOrdersDL();
        }

        
        

        public Products CreateProduct()
        {
            Products obj = new Products();
            Console.WriteLine("Type in the line item/ product name\n");
            obj.Name = Console.ReadLine();
            Console.WriteLine("\nType in the line item/ products price\n");
            obj.Price = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Type in the line item/ product Description\n");
            obj.Description = Console.ReadLine();
            Console.WriteLine("Type in the line item/ product Category\n");
            obj.Category = Console.ReadLine();


            return obj;

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

         
        public Products AddProductsBL(Products parameterObj)
        {
            return _repo.AddProductsDL(parameterObj);
        }


        
        public List<Products> GetAllProductsBL()
        {
           return _repo.GetAllProductsDL();
        }

        public LineItems AddStock(StoreFront id, Products Id, int quantity)
        {
            return _repo.AddStockToDB(id,Id,quantity);
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

        public Orders GetOrderByID(Orders obj)
        {
            return _repo.GetOrderID(obj);
        }

        public void ModifyStockTable(int storenumber, int productnumber,int quantity)
        {
            _repo.ModifyStockTable(storenumber,productnumber,quantity);
        }

        public List<Orders> GetMyOrderHistory(int objId)
        {
            return _repo.GetMyOrderHistory(objId);
        }

        public List<Orders> GetStoreOrderHistory(int objId)
        {
            return _repo.GetStoreOrderHistory(objId);
        }
    }
}