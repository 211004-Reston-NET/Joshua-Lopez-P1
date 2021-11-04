using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Models;

namespace DataAccessLogic
{
    public class Repository : InterfaceRepository
    {
    //The repository class has a bunch of methods that we will use to get or store information from the database
    //Does not actually hold the data itself
    
        //Filepath need to reference , this format locates this specific folder in the project
        private const string _filepath = "./../DataAccessLogic/Database/";
        private string _jsonString;

        /// <summary>
        /// Add customer method which begins with receiving the whole list in the db into the created list.
        /// adds the received parameter into the list at the end.
        /// then stores the file into the designated path with the json extension hardcoded in
        /// </summary>
        /// <param name="p_rest"></param>
        /// <returns></returns>
        public Customer AddCustomersDL(Customer p_rest)
        {
            //The reason why we need to grab all the information back is because File.WriteAllText method will overwrite anything inside the JSON file
            List<Customer> listOfcustomers = GetAllCustomersDL();

            //We added the new customer from the parameter 
            listOfcustomers.Add(p_rest);
            //formats the list to the indented format.
            // serialize formats and converts the list into a json formatted string.
            _jsonString = JsonSerializer.Serialize(listOfcustomers, new JsonSerializerOptions{WriteIndented=true});

            //This is what adds the customer.json
            //adds the entire list containing the previous and new customers.
            File.WriteAllText((_filepath+"Customers.json"),_jsonString);

            //Will return a customer object from the parameter
            return p_rest;
        }


        public List<Customer> GetAllCustomersDL()
        {
            //File class will just read everything in the Resturant.json and put it in a string
            _jsonString = File.ReadAllText(_filepath+"Customers.json");

            //Since we are converting from a string to an object that C# understands we need to deserialize the string to object.
            //Json Serializer has a static method called Deserialize and thats why you don't need to instantiate it
            //The parameter of the Deserialize method needs a string variable that holds the json file
            return JsonSerializer.Deserialize<List<Customer>>(_jsonString);
        }


        /// <summary>
        /// Add storefront method which begins with receiving the whole list in the db into the created list.
        /// adds the received parameter into the list at the end.
        /// then stores the file into the designated path with the json extension hardcoded in
        /// </summary>
        /// <param name="p_rest"></param>
        /// <returns></returns>
            public StoreFront AddStoreFrontDL(StoreFront p_rest)
        {
             //The reason why we need to grab all the information back is because File.WriteAllText method will overwrite anything inside the JSON file
            List<StoreFront> listOfstores = GetAllStoreFrontDL();

            //We added the new storefront from the parameter 
            listOfstores.Add(p_rest);

            _jsonString = JsonSerializer.Serialize(listOfstores, new JsonSerializerOptions{WriteIndented=true});

            //This is what adds the stores.json
            File.WriteAllText(_filepath+"Stores.json",_jsonString);

            //Will return a customer object from the parameter
            return p_rest;
        }

        public List<StoreFront> GetAllStoreFrontDL()
        {
             _jsonString = File.ReadAllText(_filepath+"Stores.json");

            //Since we are converting from a string to an object that C# understands we need to deserialize the string to object.
            //Json Serializer has a static method called Deserialize and thats why you don't need to instantiate it
            //The parameter of the Deserialize method needs a string variable that holds the json file
            return JsonSerializer.Deserialize<List<StoreFront>>(_jsonString);
        }

            /// <summary>
            /// Add products method which begins with receiving the whole list in the db into the created list.
            /// adds the received parameter into the list at the end.
            /// then stores the file into the designated path with the json extension hardcoded in
            /// </summary>
            /// <param name="p_rest"></param>
            /// <returns></returns>
            
        /// <summary>
        /// Add orders method which begins with receiving the whole list in the db into the created list.
        /// adds the received parameter into the list at the end.
        /// then stores the file into the designated path with the json extension hardcoded in
        /// </summary>
        /// <param name="p_rest"></param>
        /// <returns></returns>
        public Orders AddOrdersDL(Orders p_rest)
        {
             //The reason why we need to grab all the information back is because File.WriteAllText method will overwrite anything inside the JSON file
            List<Orders> listOfOrders = GetAllOrdersDL();

            //We added the new Orders from the parameter 
            listOfOrders.Add(p_rest);

            _jsonString = JsonSerializer.Serialize(listOfOrders, new JsonSerializerOptions{WriteIndented=true});

            //This is what adds the Orders.json
            File.WriteAllText(_filepath+"Orders.json",_jsonString);

            //Will return a Orders object from the parameter
            return p_rest;
        }

        public List<Orders> GetAllOrdersDL()
        {
             _jsonString = File.ReadAllText(_filepath+"Orders.json");

            //Since we are converting from a string to an object that C# understands we need to deserialize the string to object.
            //Json Serializer has a static method called Deserialize and thats why you don't need to instantiate it
            //The parameter of the Deserialize method needs a string variable that holds the json file
            return JsonSerializer.Deserialize<List<Orders>>(_jsonString);
        }
            /// <summary>
            /// Add line items method which begins with receiving the whole list in the db into the created list.
            /// adds the received parameter into the list at the end.
            /// then stores the file into the designated path with the json extension hardcoded in
            /// </summary>
            /// <param name="p_rest"></param>
            /// <returns></returns>


        public bool DLVerifyStore(string name,string address)
        {
            List<StoreFront> listOfStores = GetAllStoreFrontDL();
            bool result = listOfStores.Exists(x => x.Name == name);
            return result;
        }
        // 

        public List<StoreFront> DLSearchStores(string name)
        {
            List<StoreFront> listOfRestaurant = GetAllStoreFrontDL();

            //Select method will give a list of boolean if the condition was true/false
            //Where method will give the actual element itself based on some condition
            //ToList method will convert into List that our method currently needs to return.
            //ToLower will lowercase the string to make it not case sensitive
            listOfRestaurant = listOfRestaurant.Where(rest => rest.Name.ToLower().Contains(name.ToLower())).ToList();
            if (listOfRestaurant.Count < 1)
            {
                throw new Exception("Store Not found");
            }

            return listOfRestaurant;
        }

     
        public LineItems DLVerifyStock(int productnum, StoreFront chosen)
        {
            LineItems obj = new LineItems();
            // List<LineItems> listofline = new List<LineItems>();
            // listofline = DLShowStock(chosen);
            // bool result = listofline.Exists(x => x._product._name == product);
            // if (result == false)
            // {
            //     throw new Exception("Product Not found in store");
            // }
            // obj = listofline.FirstOrDefault(rest => rest._product._name == product);
            return obj;
        }

        public List<LineItems> DLShowStock(StoreFront chosen)
        {
             List<LineItems> listOfProduct = new List<LineItems>();
            chosen = DLGetStore(chosen.Name,chosen.Address);
            foreach (LineItems p in chosen.Stock)
            {
                listOfProduct.Add(p);
            }
            return listOfProduct;
        }
public StoreFront DLGetStore(string name,string address)
        {
           StoreFront obj = new StoreFront();
            List<StoreFront> listOfStores = GetAllStoreFrontDL();
            bool result = DLVerifyStore(name,address);
            if (result == false)
            {
                throw new Exception("Store Not found");
            }

            obj = listOfStores.FirstOrDefault(rest => rest.Name == name);


            return obj;
        }
        public StoreFront DLModifyStoreRecord(StoreFront currentSelection)
        {
            StoreFront test = DLGetStore(currentSelection.Name,currentSelection.Address);
            List<StoreFront> listOfstores = GetAllStoreFrontDL();
            listOfstores.RemoveAll(x => x.Name == test.Name);
            listOfstores.Add(currentSelection);
            var organized=listOfstores.OrderBy(x => x.Name);
            _jsonString = JsonSerializer.Serialize(organized, new JsonSerializerOptions{WriteIndented=true});

            //This is what adds the stores.json
            File.WriteAllText(_filepath+"Stores.json",_jsonString);

            //Will return a customer object from the parameter
            return currentSelection;


        }
        public Customer DLModifyCustomerRecord(Customer currentSelection)
        {
            Customer test = DLGetCustomer(currentSelection.UserName,currentSelection.Password);
            List<Customer> listOfstores = GetAllCustomersDL();
            listOfstores.RemoveAll(x => x.UserName == test.Password);
            listOfstores.Add(currentSelection);
            var organized=listOfstores.OrderBy(x => x.CustomerName);
            _jsonString = JsonSerializer.Serialize(organized, new JsonSerializerOptions{WriteIndented=true});

            //This is what adds the stores.json
            File.WriteAllText(_filepath+"Customers.json",_jsonString);

            //Will return a customer object from the parameter
            return currentSelection;


        }

        public Customer DLGetCustomer(string name,string password)
        {
            Customer obj = new Customer();
            List<Customer> listOfStores = GetAllCustomersDL();
            bool result = VerifyCredentials(name,password);
            if (result == false)
            {
                throw new Exception("Store Not found");
            }

            obj = listOfStores.FirstOrDefault(rest => rest.UserName == name);


            return obj;
        }

        public bool VerifyCredentials(string name,string password)
        {
            List<Customer> listOfCustomers = GetAllCustomersDL();
            bool result = listOfCustomers.Exists(x => x.UserName == name);
            if (result == false)
            {
                throw new Exception("User Not found");
            }
            return result;
        }

        public LineItems AddStockToDB(StoreFront id, Products Id, int quantity)
        {
            throw new NotImplementedException();
        }

        public List<Products> GetAllProductsDL()
        {
            throw new NotImplementedException();
        }

        public Products AddProductsDL(Products parameterObj)
        {
            throw new NotImplementedException();
        }

        public bool VerifyProduct(int identification)
        {
            throw new NotImplementedException();
        }

        public Products GetProduct(int obj)
        {
            throw new NotImplementedException();
        }

        public bool DLVerifyStore(int identification)
        {
            throw new NotImplementedException();
        }

        public StoreFront DLGetStore(int obj)
        {
            throw new NotImplementedException();
        }

        public List<LineItems> GetInventory(int obj)
        {
            throw new NotImplementedException();
        }

        public StoreFront GetStoreByID(int number)
        {
            throw new NotImplementedException();
        }

        public bool VerifyStorebyID(int number)
        {
            throw new NotImplementedException();
        }

        public void InsertHistory(int store, int prod, int order, int customer)
        {
            throw new NotImplementedException();
        }

        public Orders GetOrderID(Orders obj)
        {
            throw new NotImplementedException();
        }

        public void ModifyStockTable(int storenumber, int productnumber,int quantity)
        {
            throw new NotImplementedException();
        }

        public List<Orders> GetMyOrderHistory(int objId)
        {
            throw new NotImplementedException();
        }

        public string testmethod(int obj)
        {
            throw new NotImplementedException();
        }

        public List<Orders> GetStoreOrderHistory(int objId)
        {
            throw new NotImplementedException();
        }

        public Orders AddOrdersDL(Orders parameterobj, StoreFront store, Customer client)
        {
            throw new NotImplementedException();
        }

        public void InsertHistory(int store, int prod, int order, int customer, int quantity)
        {
            throw new NotImplementedException();
        }

        public List<StoreFront> SearchStoresDL(string name)
        {
            throw new NotImplementedException();
        }

        public LineItems VerifyStockDL(int productnum, StoreFront chosen)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerDL(string name, string password)
        {
            throw new NotImplementedException();
        }

        public Customer ModifyCustomerRecordDL(Customer currentSelection)
        {
            throw new NotImplementedException();
        }
    }
}