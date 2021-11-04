using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Entity = DataAccessLogic.Entities;
using Model = Models;

namespace DataAccessLogic
{
    public class RespositoryCloud : InterfaceRepository
    {
        private Entity.P0DatabaseContext _context;
        public RespositoryCloud(Entity.P0DatabaseContext p_context)
        {
            _context = p_context;
        }

        public Model.Customer AddCustomersDL(Model.Customer parameterobj)
        {
            //adds to the database (_context) into table Customers
            //a new customer of type Customer (db version) with the received information
            //setting each column of the customer table
            _context.Customers.Add
            (
                new Entity.Customer()
                {
                    FirstName = parameterobj.CustomerName,
                    Email = parameterobj.Contact,
                    UserName = parameterobj.UserName,
                    Password = parameterobj.Password,
                    Category = parameterobj.Position,
                    Age = parameterobj._age,
                    Address = parameterobj.Address,
                    CurrentCurrency = parameterobj.Currency

                }
            );

            //This method will save the changes made to the database
            _context.SaveChanges();

            return parameterobj;
        }
        public List<Model.Customer> GetAllCustomersDL()
        {
            // this method makes a list Test of customers
            //from the db table
            //where for each item in the select its creating a new customer
            //we connect each column correctly with the property of the customer class
            //add to list at the end to create it as a list after each created customer
            //essentially we are selecting all in this method because we created a new customer
            //_context .customers represents the table with all the rows 
            List<Model.Customer> test = _context.Customers.Select(client =>
                   new Model.Customer()
                   {

                       Id = client.CustomerId,
                       CustomerName = client.FirstName,
                       Contact = client.Email,
                       UserName = client.UserName,
                       Password = client.Password,
                       Position = client.Category,
                       Address = client.Address,
                       _age = client.Age,
                       Currency = client.CurrentCurrency
                   }
            ).ToList();
            return test;
        }
        public Model.Customer GetCustomerDL(string username, string password)
        {
            Model.Customer obj = new Model.Customer();
            List<Model.Customer> listOfStores = GetAllCustomersDL();
            bool result = VerifyCredentials(username, password);//method returns true or false based on entered information
            if (result == false)
            {
                throw new Exception("Customer Not found");
            }

            obj = listOfStores.FirstOrDefault(holder => holder.UserName == username && holder.Password == password);


            return obj;
        }
        public Models.Customer ModifyCustomerRecordDL(Models.Customer currentSelection)
        {
            Models.Customer test = GetCustomerDL(currentSelection.UserName, currentSelection.Password);
            //Here we go to the database customers table
            //creates a new customer but with the purpose of using the received information
            //creates the customer using the received info and then matches the id to avoid any errors

            _context.Customers.Update
            (
                new Entity.Customer()
                {
                    FirstName = currentSelection.CustomerName,
                    Email = currentSelection.Contact,
                    UserName = currentSelection.UserName,
                    Password = currentSelection.Password,
                    Category = currentSelection.Position,
                    Age = currentSelection._age,
                    Address = currentSelection.Address,
                    CustomerId = currentSelection.Id,
                    CurrentCurrency = currentSelection.Currency
                }
            );

            //This method will save the changes made to the database
            _context.SaveChanges();

            //Will return a customer object from the parameter
            return currentSelection;
        }

        public bool VerifyCredentials(string name, string password)
        {
            //gets all customers and makes a list that c# understands
            List<Model.Customer> listOfCustomers = GetAllCustomersDL();
            bool result = true;
            //created customer object and looks for the first match of the username and Id
            Model.Customer obj = listOfCustomers.FirstOrDefault(client => client.UserName == name && client.Password == password);
            if (obj == null)
            {
                result = false;
            }

            return result;
        }




        public Model.StoreFront AddStoreFrontDL(Model.StoreFront parameterobj)
        {
            _context.StoreFronts.Add
           (
               new Entity.StoreFront()
               {
                   StoreName = parameterobj.Name,
                   Location = parameterobj.Address


               }
           );

            //This method will save the changes made to the database
            _context.SaveChanges();

            return parameterobj;
        }





        public List<Model.StoreFront> SearchStoresDL(string name)
        {
            List<Model.StoreFront> listofstores = GetAllStoreFrontDL();

            //Select method will give a list of boolean if the condition was true/false
            //Where method will give the actual element itself based on some condition
            //ToList method will convert into List that our method currently needs to return.
            //ToLower will lowercase the string to make it not case sensitive
            listofstores = listofstores.Where(store => store.Name.ToLower().Contains(name.ToLower())).ToList();
            if (listofstores.Count < 1)
            {
                throw new Exception("Store Not found");
            }

            return listofstores;
        }
        public List<Model.StoreFront> GetAllStoreFrontDL()
        {
            List<Model.StoreFront> test = _context.StoreFronts.Select(storeobj =>
                   new Model.StoreFront()
                   {

                       Id = storeobj.StoreId,
                       Name = storeobj.StoreName,
                       Address = storeobj.Location
                   }
            ).ToList();
            return test;
        }

        public Model.LineItems VerifyStockDL(int productnum, Model.StoreFront chosen)
        {
            Model.LineItems obj = new LineItems();
            List<Model.LineItems> listofline = new List<Model.LineItems>();
            //Gets all information in the Stock table related to the received store id 
            listofline = GetInventory(chosen.Id);
            //checks the now filled list of the stores if the store contains a line item with the received product number
            bool result = listofline.Exists(x => x.ProductEstablish.Id == productnum);//exists returns a boolean value
            if (result == false)
            {
                throw new Exception("Product Not found in store");
            }
            //if an exception was never thrown then it will look for that received identification number and set it to the created line item
            obj = listofline.FirstOrDefault(prodobj => prodobj.ProductEstablish.Id == productnum);
            return obj;
        }


        public Orders AddOrdersDL(Orders parameterobj, StoreFront store, Customer client)
        {
            _context.OrdersRecords.Add
            (
                new Entity.OrdersRecord()
                {
                    Total = parameterobj.TotalPrice,
                    CustomerId = client.Id,
                    StoreId = store.Id


                });
            _context.SaveChanges();
            return parameterobj;
        }


        public List<Orders> GetAllOrdersDL()
        {
            List<Orders> test = _context.OrdersRecords.Select(rest =>
                   new Model.Orders()
                   {

                       Id = rest.OrderId,
                       TotalPrice = rest.Total,
                   }
            ).ToList();
            return test;
        }






        public LineItems AddStockToDB(StoreFront store, Products prod, int quantity)
        {  //this line item is just to return something for test purposes
            LineItems test = new LineItems();
            _context.Stocks.Add
           (
               new Entity.Stock()
               {
                   StoreId = store.Id,
                   ProductId = prod.Id,
                   InStock = quantity


               }
           );

            //This method will save the changes made to the database
            _context.SaveChanges();

            return test;
        }

        public List<Models.Products> GetAllProductsDL()
        {
            List<Models.Products> test = _context.Products.Select(prodobj =>
                   new Model.Products()
                   {

                       Id = prodobj.ProductId,
                       Name = prodobj.Name,
                       Price = prodobj.Price,
                       Description = prodobj.Description,
                       Category = prodobj.Category

                   }
            ).ToList();
            return test;
        }

        public Products AddProductsDL(Products parameterObj)
        {
            _context.Products.Add
            (
                new Entity.Product()
                {
                    Name = parameterObj.Name,
                    Price = parameterObj.Price,
                    Description = parameterObj.Description,
                    Category = parameterObj.Category,


                }
            );

            //This method will save the changes made to the database
            _context.SaveChanges();

            return parameterObj;
        }

        public bool VerifyProduct(int identification)
        {
            //creates a Product table object of the db and intializes it
            //initialized with a db search of products table with parameter of product id
            Entity.Product looking = _context.Products.Find(identification);
            bool result = true;
            if (looking == null)//if value was not found then it will return a false value for whatever method called it
            {
                result = false;
            }
            return result;

        }
        public Products GetProduct(int obj)
        {
            Models.Products test = new Models.Products();
            bool result = VerifyProduct(obj);
            if (result == false)
            {
                throw new Exception("Product Was not found with entered ID number");
            }
            else
            { //finds the product in the db and then we fill out our Model.Product with that found information
                Entity.Product looking = _context.Products.Find(obj);
                test.Name = looking.Name;
                test.Id = looking.ProductId;
                test.Price = looking.Price;
                test.Category = looking.Category;
                test.Description = looking.Description;
                return test;
            }



        }

        public List<LineItems> GetInventory(int obj)
        {
            //this query is to receive all rows in the Stocks table that match the received store id
            //returns the product information and the quantity in stock

            var result = from compl in _context.Stocks
                         where compl.StoreId == obj
                         select new { compl.Product, compl.InStock };


            //Mapping the Queryable<Entity.Stock> into a list<Model.LineItem>
            List<Model.LineItems> listofItems = new List<Model.LineItems>();

            foreach (var row in result)
            {
                LineItems test = new LineItems();
                //mapping /creating a new models product with the current info of the query
                test.ProductEstablish = new Model.Products()
                {
                    Price = row.Product.Price,
                    Name = row.Product.Name,
                    Id = row.Product.ProductId,
                    Description = row.Product.Description,
                    Category = row.Product.Category

                };
                test.Quantity = row.InStock;

                //adding to the list after establishing all values a line item needed
                listofItems.Add(test);
            }

            return listofItems;
        }

        public StoreFront GetStoreByID(int number)
        {
            Models.StoreFront test = new Models.StoreFront();
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
                Entity.StoreFront looking = _context.StoreFronts.Find(number);
                test.Name = looking.StoreName;
                test.Id = looking.StoreId;
                test.Address = looking.Location;
                return test;
            }

        }
        public bool VerifyStorebyID(int number)
        {
            //creates a Storefront table object of the db and intializes it
            //initialized with a db search of Storefront table with parameter of product id
            Entity.StoreFront looking = _context.StoreFronts.Find(number);

            bool result = true;
            if (looking == null)
            {
                result = false;
            }
            return result;

        }

        public void InsertHistory(int store, int prod, int order, int customer, int quantity)
        {
            //essentially representing the line items of an order it is adding it to the db
            _context.OrderHistories.Add
           (
               new Entity.OrderHistory()
               {
                   StoreId = store,
                   ProductId = prod,
                   OrderId = order,
                   CustomerId = customer,
                   LineQuantity = quantity

               }
           );

            //This method will save the changes made to the database
            _context.SaveChanges();
        }

        public Orders GetOrderID(Orders obj)
        {
            //method used to receive the last created id from the orders table and return that value

            List<Model.Orders> test = new List<Model.Orders>();
            test = GetAllOrdersDL();//sent all of the ordrs from the db into this list obj
            IEnumerable<Orders> query = test.OrderBy(x => x.Id);//inorder to use the last method we made an Inumerable list
            //it had to be sorted first in order for this to function
            Models.Orders temp = query.Last();//looked for the last object in the last and gave it these values.
            obj.Id = temp.Id;//set the received orders information and set it with the last id.
            return obj;
        }

        public void ModifyStockTable(int storenumber, int productnumber, int quantity)
        {

            _context.Stocks.Update
            (
                new Entity.Stock()
                {
                    StoreId = storenumber,
                    ProductId = productnumber,
                    InStock = quantity

                }
            );

            //This method will save the changes made to the database
            _context.SaveChanges();
        }

        public List<Orders> GetMyOrderHistory(int objId)
        {
            //searchs the essentially Line Items table and takes all the rows where the customer id is equal to the received obj number
            var searchresult = from compl in _context.OrderHistories
                               where compl.CustomerId == objId
                               select new { compl.Order, compl.Product, compl.Store };


            //Mapping the Queryable<Entity.Orders> into a list<Model.Orders>
            List<Model.Orders> listofItems = new List<Model.Orders>();

            foreach (var row in searchresult)
            {
                LineItems test = new LineItems();
                Model.Orders mine = new Orders();

                test.ProductEstablish = new Model.Products()
                {
                    Price = row.Product.Price,
                    Name = row.Product.Name,
                    Id = row.Product.ProductId,
                    Description = row.Product.Description,
                    Category = row.Product.Category

                };
                mine.Id = row.Order.OrderId;
                mine.TotalPrice = row.Order.Total;
                mine.ItemsList.Add(test);
                mine.Location = new Model.StoreFront()
                {
                    Name = row.Store.StoreName,
                    Address = row.Store.Location,
                    Id = row.Store.StoreId
                };

                listofItems.Add(mine);
            }


            return listofItems;
        }

        public List<Orders> GetStoreOrderHistory(int objId)
        {
            var result = from compl in _context.OrderHistories
                         where compl.StoreId == objId
                         select new { compl.Order, compl.Product, compl.Store };


            //Mapping the Queryable<Entity.Order> into a list<Model.Orders>
            List<Model.Orders> listofItems = new List<Model.Orders>();

            foreach (var rev in result)
            {
                LineItems test = new LineItems();
                Model.Orders mine = new Orders();

                test.ProductEstablish = new Model.Products()
                {
                    Price = rev.Product.Price,
                    Name = rev.Product.Name,
                    Id = rev.Product.ProductId,
                    Description = rev.Product.Description,
                    Category = rev.Product.Category

                };
                mine.Id = rev.Order.OrderId;
                mine.TotalPrice = rev.Order.Total;
                mine.ItemsList.Add(test);
                mine.Location = new Model.StoreFront()
                {
                    Name = rev.Store.StoreName,
                    Address = rev.Store.Location,
                    Id = rev.Store.StoreId
                };

                listofItems.Add(mine);
            }


            return listofItems;
        }
    }
}