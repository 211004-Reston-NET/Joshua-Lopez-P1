using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace DataAccessLogic
{
    public class RespositoryCloud : InterfaceRepository
    {
        private readonly P0DatabaseContext _context;
        public RespositoryCloud(P0DatabaseContext p_context)
        {
            _context = p_context;
        }

        public Customer AddCustomersDL(Customer parameterobj)
        {
            //adds to the database (_context) into table Customers
            //a new customer of type Customer (db version) with the received information
            //setting each column of the customer table
            _context.Customers.Add
            (parameterobj
            );

            //This method will save the changes made to the database
            _context.SaveChanges();

            return parameterobj;
        }
        public List<Customer> GetAllCustomersDL()
        {
            // this method makes a list Test of customers
            //from the db table
            //where for each item in the select its creating a new customer
            //we connect each column correctly with the property of the customer class
            //add to list at the end to create it as a list after each created customer
            //essentially we are selecting all in this method because we created a new customer
            //_context .customers represents the table with all the rows 
            List<Customer> test = _context.Customers.ToList();
            return test;
        }
        public Customer GetCustomerDL(string username, string password)
        {
           
            List<Customer> listOfStores = GetAllCustomersDL();
            bool result = VerifyCredentials(username, password);//method returns true or false based on entered information
            if (result == false)
            {
                throw new Exception("Customer Not found");
            }

            Customer obj = listOfStores.FirstOrDefault(holder => holder.UserName == username && holder.Password == password);


            return obj;
        }
        public Customer ModifyCustomerRecordDL(Customer currentSelection)
        {


            //Here we go to the database customers table
            //creates a new customer but with the purpose of using the received information
            //creates the customer using the received info and then matches the id to avoid any errors

            _context.Customers.Update(currentSelection);

            //This method will save the changes made to the database
            _context.SaveChanges();

            //Will return a customer object from the parameter
            return currentSelection;
        }

        public bool VerifyCredentials(string username, string password)
        {
            //gets all customers and makes a list that c# understands
            List<Customer> listOfCustomers = GetAllCustomersDL();
            bool result = true;
            //created customer object and looks for the first match of the username and Id
            Customer obj = listOfCustomers.FirstOrDefault(client => client.UserName == username && client.Password == password);
            if (obj == null)
            {
                result = false;
            }

            return result;
        }




        public StoreFront AddStoreFrontDL(StoreFront parameterobj)
        {
            _context.StoreFronts.Add
           (
               parameterobj
           );

            //This method will save the changes made to the database
            _context.SaveChanges();

            return parameterobj;
        }





        public List<StoreFront> SearchStoresDL(string name)
        {
            List<StoreFront> listofstores = GetAllStoreFrontDL();

            //Select method will give a list of boolean if the condition was true/false
            //Where method will give the actual element itself based on some condition
            //ToList method will convert into List that our method currently needs to return.
            //ToLower will lowercase the string to make it not case sensitive
            listofstores = listofstores.Where(store => store.StoreName.ToLower().Contains(name.ToLower())).ToList();
            if (listofstores.Count < 1)
            {
                throw new Exception("Store Not found");
            }

            return listofstores;
        }
        public List<StoreFront> GetAllStoreFrontDL()
        {
            List<StoreFront> test = _context.StoreFronts.ToList();
            return test;
        }

        public LineItems VerifyStockDL(int productnum, StoreFront chosen)
        {
           
            
            //Gets all information in the Stock table related to the received store id 
            List<LineItems> listofline = GetInventory(chosen.Id);
            //checks the now filled list of the stores if the store contains a line item with the received product number
            bool result = listofline.Exists(x => x.Product_obj.Id == productnum);//exists returns a boolean value
            if (result == false)
            {
                throw new Exception("Product Not found in store");
            }
            //if an exception was never thrown then it will look for that received identification number and set it to the created line item
            LineItems obj = listofline.FirstOrDefault(prodobj => prodobj.Product_obj.Id == productnum);
            return obj;
        }


        public Orders AddOrdersDL(Orders parameterobj)
        {
            _context.OrdersRecords.Add
            (
                parameterobj);
            _context.SaveChanges();
            return parameterobj;
        }


        public List<Orders> GetAllOrdersDL()
        {
            List<Orders> test = _context.OrdersRecords.ToList();
            return test;
        }






        public LineItems AddStockToDB(int storenumber, int productnumber, int quantity)
        {  //this line item is just to return something for test purposes
            LineItems test = new LineItems();
            test.StoreID=storenumber;
            test.ProductID=productnumber;
            test.Quantity=quantity;
            _context.Stocks.Add
           (test

           );

            //This method will save the changes made to the database
            _context.SaveChanges();

            return test;
        }

        public List<Products> GetAllProductsDL()
        {
            List<Products> test = _context.Products.ToList();
            return test;
        }

        public Products AddProductsDL(Products parameterObj)
        {
            _context.Products.Add
            (
                parameterObj
            );

            //This method will save the changes made to the database
            _context.SaveChanges();

            return parameterObj;
        }

        public bool VerifyProduct(int productidentification)
        {
            //creates a Product table object of the db and intializes it
            //initialized with a db search of products table with parameter of product id
            Products looking = _context.Products.Find(productidentification);
            bool result = true;
            if (looking == null)//if value was not found then it will return a false value for whatever method called it
            {
                result = false;
            }
            return result;

        }
        public Products GetProduct(int productid)
        {
            Products test = new Products();
            bool result = VerifyProduct(productid);
            if (result == false)
            {
                throw new Exception("Product Was not found with entered ID number");
            }
            else
            { //finds the product in the db and then we fill out our Product with that found information
                Products looking = _context.Products.Find(productid);
                test.Name = looking.Name;
                test.Id = looking.Id;
                test.Price = looking.Price;
                test.Category = looking.Category;
                test.Description = looking.Description;
                return test;
            }



        }

        public List<LineItems> GetInventory(int storeid)
        {
            //this query is to receive all rows in the Stocks table that match the received store id
            //returns the product information and the quantity in stock

            var result = from compl in _context.Stocks
                         where compl.StoreID == storeid
                         select new { compl.Product_obj, compl.Quantity };


            //Mapping the Queryable<Entity.Stock> into a list<LineItem>
            List<LineItems> listofItems = new List<LineItems>();

            foreach (var row in result)
            {
                LineItems test = new LineItems();
                //mapping /creating a new models product with the current info of the query
                test.Product_obj = new Products()
                {
                    Price = row.Product_obj.Price,
                    Name = row.Product_obj.Name,
                    Id = row.Product_obj.Id,
                    Description = row.Product_obj.Description,
                    Category = row.Product_obj.Category

                };
                test.StoreID = storeid;
                test.ProductID = row.Product_obj.Id;
                test.Quantity = row.Quantity;

                //adding to the list after establishing all values a line item needed
                listofItems.Add(test);
            }

            return listofItems;
        }

        public StoreFront GetStoreByID(int number)
        {
            //first calls the verify store id method to see if it is in the database
            bool result = VerifyStorebyID(number);
            if (result == false)
            {
                throw new Exception("Product Was not found with entered ID number");
            }
            else
            {
                //if it reached then the store id is in the db so we create a new obj of Storefront db table type
                //then match all the information needed to be a Model storefront
                StoreFront looking = _context.StoreFronts.Find(number);
                return looking;
            }

        }
        public bool VerifyStorebyID(int number)
        {
            //creates a Storefront table object of the db and intializes it
            //initialized with a db search of Storefront table with parameter of product id
            StoreFront looking = _context.StoreFronts.Find(number);

            bool result = true;
            if (looking == null)
            {
                result = false;
            }
            return result;

        }

        public void InsertHistory(int store, int prod, int order, int customer, int quantity)
        {
            OrderLines test = new OrderLines();
            test.CustomerId = customer;
            test.LineQuantity = quantity;
            test.OrderId = order;
            test.ProductId = prod;
            test.StoreId = store;
            //essentially representing the line items of an order it is adding it to the db
            _context.OrderHistories.Add
           (test);

            //This method will save the changes made to the database
            _context.SaveChanges();
        }

        public Orders GetOrderID(Orders obj)
        {
            //method used to receive the last created id from the orders table and return that value

            List<Orders> test = GetAllOrdersDL();//sent all of the ordrs from the db into this list obj
            IEnumerable<Orders> query = test.OrderBy(x => x.OrderId);//inorder to use the last method we made an Inumerable list
            //it had to be sorted first in order for this to function
            Orders temp = query.Last();//looked for the last object in the last and gave it these values.
            obj.OrderId = temp.OrderId;
            obj.CustomerId = temp.CustomerId;
            obj.StoreId = temp.StoreId;
            obj.Total = temp.Total;//set the received orders information and set it with the last id.
            return obj;
        }

        public void ModifyStockTable(int storenumber, int productnumber, int quantity)
        {
            LineItems test = new LineItems();
            test.Quantity = quantity;
            test.ProductID = productnumber;
            test.StoreID = storenumber;


            _context.Stocks.Update
            (
               test


            );

            //This method will save the changes made to the database
            _context.SaveChanges();
        }

        public List<Orders> GetStoreOrderHistory(int objId)
        {
            var result = from compl in _context.OrdersRecords
                         where compl.StoreId == objId
                         select new { compl.CustomerId, compl.OrderId, compl.StoreId,compl.Total };


            //Mapping the Queryable<Entity.Order> into a list<Orders>
            List<Orders> listofItems = new List<Orders>();

            foreach (var rev in result)
            {
                Orders mine = new Orders();
                mine.CustomerId=rev.CustomerId;
                mine.OrderId=rev.OrderId;
                mine.Total=rev.Total;
                mine.StoreId=rev.StoreId;

                listofItems.Add(mine);
            }


            return listofItems;
        }

        public decimal FindProductPrice(int p_productId)
        { 
            decimal itemPrice=0;

            
            bool result = VerifyProduct(p_productId);
            if (result == false)
            {
                throw new Exception("Product Was not found with entered ID number");
            }
            else
            { //finds the product in the db and then we fill out our Product with that found information
                Products looking = _context.Products.Find(p_productId);
                itemPrice = looking.Price;
           
                return itemPrice;
            }
        }

        public List<OrderLines> GetMyOrderHistory(int objId)
        {
            var searchresult = from compl in _context.OrderHistories
                               where compl.CustomerId == objId
                               select new { compl.Order_obj, compl.Product_obj, compl.Store_obj, compl.LineQuantity };


            //Mapping the Queryable<Entity.Orders> into a list<Orders>
            List<OrderLines> listofItems = new List<OrderLines>();

            foreach (var row in searchresult)
            {
                LineItems test = new LineItems();
                OrderLines mine = new OrderLines();

                test.Product_obj = new Products()
                {
                    Price = row.Product_obj.Price,
                    Name = row.Product_obj.Name,
                    Id = row.Product_obj.Id,
                    Description = row.Product_obj.Description,
                    Category = row.Product_obj.Category

                };
          
                mine.CustomerId = objId;
                mine.StoreId = row.Store_obj.Id;
                mine.Order_obj = row.Order_obj;
                mine.OrderId = row.Order_obj.OrderId;
                mine.LineQuantity = row.LineQuantity;

                mine.Product_obj=row.Product_obj;

                listofItems.Add(mine);
            }


            return listofItems;
        }
        }
    }