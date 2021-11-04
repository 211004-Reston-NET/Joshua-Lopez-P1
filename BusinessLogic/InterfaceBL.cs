using System.Collections.Generic;
using Models;

namespace BusinessLogic
{
    public interface InterfaceBL
    {
        /// <summary>
        /// This will return a list of customers stored in the database
        /// </summary>
        /// <returns>It will return a list of customers</returns>
        List<Customer> GetAllCustomersBL();

        /// <summary>
        /// It will add a customer in our database
        /// </summary>
        /// <param name="p_rest">This is the customer we will be adding to the database</param>
        /// <returns>It will just return the customer we are adding</returns>
        Customer AddCustomersBL(Customer parameterobj);


        /// <summary>
        /// It will add a store front in our database
        /// </summary>
        /// <param name="p_rest">This is the store front we will be adding to the database</param>
        /// <returns>It will just return the store front we are adding</returns>
        StoreFront AddStoreFrontBL(StoreFront parameterobj);

        /// <summary>
        /// This will return a list of store front stored in the database
        /// </summary>
        /// <returns>It will return a list of store front</returns>
        List<StoreFront> GetAllStoreFrontsBL();



        /// <summary>
        /// Adds a customer to the orders
        /// </summary>
        /// <param name="p_rest">This is the product we are adding</param>
        /// <returns>It returns the product customer</returns>     
        Orders AddOrdersBL(Orders parameterobj, StoreFront store, Customer client);





        /// <summary>
        /// This creates a Product and returns the value 
        /// This method is more for establishing the payment of the order
        /// </summary>
        /// <returns>It will return a  product</returns>
        Products CreateProduct();


        /// <summary>
        /// Verifies in the database if the entered store name and address is located in the database
        /// and retrieves the found store or an exception
        /// </summary>
        /// <param name="name"></param>
        /// <returns>returns the retrieved store information from the db </returns>
        List<StoreFront> SearchStores(string name);


        /// <summary>
        /// This method verifies if the stock is in the specified store as well
        /// </summary>
        /// <param name="product"></param>
        /// <param name="chosen"></param>
        /// <returns>returns the line item from the db that matches</returns>
        LineItems VerifyStock(int productnum, StoreFront chosen);


        /// <summary>
        /// Verifies in the database if the entered information is located in the database
        /// and retrieves the found store or an exception
        /// </summary>
        /// <param name="name"></param>
        /// <returns>returns the retrieved Customer information from the db </returns>
        Customer GetCustomer(string name, string password);

        /// <summary>
        /// this method takes the Customer  , erases the previous information from the json file
        /// that matches the user information
        /// enters the new information into the json file
        /// also organizes in alphabetical order.
        /// </summary>
        /// <param name="currentSelection"></param>
        /// <returns></returns>
        Customer ModifyCustomerRecord(Customer currentSelection);

        /// <summary>
        /// Receives a created Product object and will insert it into the database.
        /// No information is changed
        /// </summary>
        /// <param name="parameterObj"></param>
        /// <returns>created product object that was sent in</returns>
        Products AddProductsBL(Products parameterObj);

        /// <summary>
        /// Receives a created Product object and will insert it into the database.
        /// No information is changed
        /// </summary>
        /// <param name="parameterObj"></param>
        /// <returns>created product object that was sent in</returns>
        List<Products> GetAllProductsBL();

        /// <summary>
        /// Adds a Line Item essentially into the stock db table
        /// It receives a storefront which will utilize only its ID 
        /// It recieves a product will utilize only the ID
        /// Receives a integer to represent the amount of items
        /// </summary>
        /// <param name="store"></param>
        /// <param name="prod"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        LineItems AddStock(StoreFront id, Products Id, int quantity);


        /// <summary>
        /// Verifies product is in db
        /// searches by the product id number that is sent
        /// </summary>
        /// <param name="productidentification"></param>
        /// <returns></returns>
        bool VerifyProduct(int identification);
        /// <summary>
        /// Returns a Product with information retrieved from the db
        /// </summary>
        /// <param name="productid"></param>
        /// <returns>Product if not an exception</returns>

        Products GetProduct(int obj);
        /// <summary>
        /// Returns a list of Line Items which contain all products related to the specified store
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns>a list of </returns>
        List<LineItems> GetInventory(int obj);

        /// <summary>
        /// Sends an id number to be searched for and retrieves the stores saved information
        /// </summary>
        /// <param name="number"></param>
        /// <returns>Returns the store that matches the Id if not an exception</returns>

        StoreFront GetStoreByID(int number);

        /// <summary>
        /// insert into the database table Order History
        /// Sends the store id, product id (essentially line item)
        /// sends the recently created Order Id and customer Id
        /// Receives the actual result from a method in the repositry project
        /// </summary>
        /// <param name="store"></param>
        /// <param name="prod"></param>
        /// <param name="order"></param>
        /// <param name="customer"></param>
        void InsertHistory(int store, int prod, int order, int customer, int quantity);

        /// <summary>
        /// Used after the insert into purchases table
        /// it will return the order object with the LAST id in the table
        /// Receives the actual result from a method in the repositry project
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>obj.Id</returns>
        Orders GetOrderByID(Orders obj);

        /// <summary>
        /// This method occurs in replenish inventory and purchasing
        /// essentially takes the selected store id number and line item and send that info to data logic method
        /// Receives the actual result from a method in the repositry project
        /// </summary>
        /// <param name="storenumber"></param>
        /// <param name="productnumber"></param>
        /// <param name="quantity"></param>
        void ModifyStockTable(int storenumber, int productnumber, int quantity);

        /// <summary>
        /// Receives the Id of the Customer/current user in order to search for that users order history in the db
        /// </summary>
        /// <param name="objId"></param>
        /// <returns>a list of all orders related to the received id</returns>
        List<Orders> GetMyOrderHistory(int objId);
        /// <summary>
        /// Receives the Id of the store in order to search for that stores order history in the db
        /// </summary>
        /// <param name="objId"></param>
        /// <returns>a list of all orders related to the received id</returns>
        List<Orders> GetStoreOrderHistory(int objId);

    }
}