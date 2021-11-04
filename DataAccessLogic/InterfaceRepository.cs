using System.Collections.Generic;
using Models;

namespace DataAccessLogic
{
    public interface InterfaceRepository
    {
        /// <summary>
        /// It will add a customer in our database
        /// </summary>
        /// <param name="p_rest">This is the customer we will be adding to the database</param>
        /// <returns>It will just return the customer we are adding</returns>
        Customer AddCustomersDL(Customer parameterobj);

        /// <summary>
        /// This will return a list of customers stored in the database
        /// </summary>
        /// <returns>It will return a list of customers</returns>
        List<Customer> GetAllCustomersDL();


        /// <summary>
        /// It will add a store front in our database
        /// </summary>
        /// <param name="p_rest">This is the store front we will be adding to the database</param>
        /// <returns>It will just return the store front we are adding</returns>
        StoreFront AddStoreFrontDL(StoreFront parameterobj);

        /// <summary>
        /// This will return a list of store front stored in the database
        /// </summary>
        /// <returns>It will return a list of store front</returns>
        List<StoreFront> GetAllStoreFrontDL();

        /// <summary>
        /// It will add a store front in our database
        /// </summary>
        /// <param name="p_rest">This is the store front we will be adding to the database</param>
        /// <returns>It will just return the store front we are adding</returns>
        Orders AddOrdersDL(Orders parameterobj, StoreFront store, Customer client);

        /// <summary>
        /// This will return a list of orders stored in the database
        /// </summary>
        /// <returns>It will return a list of orders</returns>
        List<Orders> GetAllOrdersDL();


        /// <summary>
        /// Verifies in the database if the entered store name and address is located in the database
        /// and retrieves the found store or an exception
        /// </summary>
        /// <param name="name"></param>
        /// <returns>returns the retrieved store information from the db </returns>
        List<StoreFront> SearchStoresDL(string name);
        /// <summary>
        /// This method verifies if the stock is in the specified store as well
        /// </summary>
        /// <param name="product"></param>
        /// <param name="chosen"></param>
        /// <returns>returns the line item from the db that matches</returns>
        LineItems VerifyStockDL(int productnum, StoreFront chosen);

        /// <summary>
        /// Verifies in the database if the entered client user name is located in the database
        /// used for the search and modify Customer front methods.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>returns true or false </returns>
        bool VerifyCredentials(string username, string password);

        /// <summary>
        /// Verifies in the database if the entered information is located in the database
        /// and retrieves the found store or an exception
        /// </summary>
        /// <param name="username"></param>
        ///  <param name="password"></param>
        /// <returns>returns the retrieved Customer information from the db </returns>
        Customer GetCustomerDL(string username, string password);

        /// <summary>
        /// this method takes the Customer  , erases the previous information from the json file
        /// that matches the user information
        /// enters the new information into the json file
        /// also organizes in alphabetical order.
        /// </summary>
        /// <param name="currentSelection"></param>
        /// <returns></returns>
        Customer ModifyCustomerRecordDL(Customer currentSelection);

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
        LineItems AddStockToDB(StoreFront store, Products prod, int quantity);

        /// <summary>
        /// Returns A list of all products currently in the Database
        /// </summary>
        /// <returns>List of products</returns>
        List<Products> GetAllProductsDL();

        /// <summary>
        /// Receives a created Product object and will insert it into the database.
        /// No information is changed
        /// </summary>
        /// <param name="parameterObj"></param>
        /// <returns>created product object that was sent in</returns>
        Products AddProductsDL(Products parameterObj);

        /// <summary>
        /// Verifies product is in db
        /// searches by the product id number that is sent
        /// </summary>
        /// <param name="productidentification"></param>
        /// <returns></returns>
        bool VerifyProduct(int productidentification);

        /// <summary>
        /// Returns a Product with information retrieved from the db
        /// </summary>
        /// <param name="productid"></param>
        /// <returns>Product if not an exception</returns>
        Products GetProduct(int productid);

        /// <summary>
        /// Returns a list of Line Items which contain all products related to the specified store
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns>a list of </returns>

        List<LineItems> GetInventory(int storeid);

        /// <summary>
        /// Sends an id number to be searched for and retrieves the stores saved information
        /// </summary>
        /// <param name="number"></param>
        /// <returns>Returns the store that matches the Id if not an exception</returns>
        StoreFront GetStoreByID(int number);

        /// <summary>
        /// Used in other methods it returns a bool value in order to verify if true or false
        /// it will be used to catch exceptions
        /// it verifies the entered store Id number is in the db
        /// </summary>
        /// <param name="number"></param>
        /// <returns>true or false</returns>
        bool VerifyStorebyID(int number);

        /// <summary>
        /// insert into the database table Order History
        /// Sends the store id, product id (essentially line item)
        /// sends the recently created Order Id and customer Id
        /// </summary>
        /// <param name="store"></param>
        /// <param name="prod"></param>
        /// <param name="order"></param>
        /// <param name="customer"></param>
        void InsertHistory(int store, int prod, int order, int customer, int quantity);

        /// <summary>
        /// Used after the insert into purchases table
        /// it will return the order object with the LAST id in the table
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>obj.Id</returns>
        Orders GetOrderID(Orders obj);

        /// <summary>
        /// This method occurs in replenish inventory and purchasing
        /// essentially takes the selected store id number and line item and send that info to data logic method
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