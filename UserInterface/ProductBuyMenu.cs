using System;
using BusinessLogic;
using System.Collections.Generic;
using Models;

namespace UserInterface
{
    public class ProductBuyMenu : IMenu
    {
        //keep track of the order
        private static Orders _details = new Orders();
        //used to show at the end of the purchase process
        static List<string> cartResult = new List<string>();
        //used to keep track for the db and establish the final item list
        static List<LineItems> tempdb = new List<LineItems>();

        private InterfaceBL parameterInter;
        public ProductBuyMenu(InterfaceBL parameterobj)
        {
            parameterInter = parameterobj;
        }
        public void Menu()
        {
            
            _details.Location = SingletonUser.currentstore;
            Console.WriteLine("##################################################################################\n");
            Console.WriteLine("\tWelcome to the " + SingletonUser.currentstore.Name + " products menu");
            Console.WriteLine("\tBelow is a list of products");
            Console.WriteLine("---------------------------------------------------------------------\n");
            Console.WriteLine("\nList of Products in " + SingletonUser.currentstore.Name);
            StoreFront test = parameterInter.GetStoreByID(SingletonUser.currentstore.Id);
            
            foreach (LineItems rest in parameterInter.GetInventory(SingletonUser.currentstore.Id))
            {
                Console.WriteLine("====================");
                Console.WriteLine("\t" + rest);
                Console.WriteLine("====================");
            }
            Console.WriteLine("\n##################################################################################\n");
            Console.WriteLine("\tAdding to cart");
            foreach (LineItems x in tempdb)//represents what stock info will be updated in the information
            {
                Console.WriteLine(x.ProductEstablish.Name);

            }

            Console.WriteLine("---------------------------------------------------------------\n");
            Console.WriteLine("\tCheckout cart");//represents the records for the final purchase
            foreach (LineItems x in _details.ItemsList)
            {
                Console.WriteLine(x);

            }
            Console.WriteLine("\tTotal of cart is $" + _details.TotalPrice);
            
            Console.WriteLine("\nCurrent Balance in wallet: $" + SingletonUser.currentuser.Currency);
            Console.WriteLine("\n##################################################################################\n");
            
            Console.WriteLine("\t[5] - Add Items to cart");
            Console.WriteLine("\t[4] - remove items from cart");
            Console.WriteLine("\t[3] - Modify/calculate total of order");
            Console.WriteLine("\t[2] - confirm");
            Console.WriteLine("\t[1] - back");
            Console.WriteLine("\t[0] - go to Main Menu");
            

            Console.WriteLine("\n##################################################################################\n");

        }

        public MenuType YourChoice()
        {
            string userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "5":
                    /// <summary>
                    /// the process of this case
                    /// 1.user enters number
                    /// 1a.verifies if it is a number
                    /// 2.verifies if the entered product id is in the selected store
                    /// 2a.if it is not found it will go to the catch block
                    /// 2a1.user decideds to cancel or try again
                    /// 2b.if found verifies if it is in stock
                    /// 2c.if found verifies if it is already added to the shopping list
                    /// 3.adds to list
                    /// 4.ends while loop
                    /// </summary>

                    bool decision = true;
                    do
                    {

                        LineItems _lines = new LineItems();

                        bool loop = true;
                        while (loop == true)
                        {
                            

                            Console.WriteLine("##################################################################################\n");
                            Console.WriteLine("\tEnter the Id of the product from the store to add to your cart");
                            int productsname = 0;
                            try
                            {
                                productsname = Convert.ToInt32(Console.ReadLine());

                            }
                            catch (System.Exception)
                            {

                                
                                Console.WriteLine("************************************************\n");
                                Console.WriteLine("you have entered something that was not a number please try again");
                                Console.WriteLine("Press Enter to continue");
                                Console.WriteLine("\n************************************************\n");
                                Console.ReadLine();
                                return MenuType.ProductBuyMenu;
                            }
                            string cancel;
                            try
                            {

                                _lines = parameterInter.VerifyStock(productsname, SingletonUser.currentstore);
                                loop = false;
                                if (_lines.Quantity > 0)
                                {
                                    if (tempdb.Exists(x => x.ProductEstablish.Name == _lines.ProductEstablish.Name))
                                    {
                                        
                                        Console.WriteLine("\n************************************************\n");
                                        Console.WriteLine("\tThis item is already in the cart");
                                        Console.WriteLine("\n************************************************\n");
                                        Console.ReadLine();

                                    }
                                    else
                                    {

                                        tempdb.Add(_lines);
                                        _details.TotalPrice = 0;
                                        _details.ItemsList.Clear();
                                    }
                                }
                                else
                                {
                                    

                                    Console.WriteLine("\n************************************************\n");
                                    Console.WriteLine("\tItem cannot be added because it is out of stock");
                                    Console.WriteLine("\n************************************************\n");
                                    Console.ReadLine();
                                }


                            }
                            catch (System.Exception)
                            {
                                
                                Console.WriteLine("\n************************************************\n");
                                Console.WriteLine("\tPlease try again you have entered an item that is not listed in the store");
                                loop = true;
                                Console.WriteLine("\tWould you like to cancel the item? \n\ttype yes or no?\n");
                                Console.WriteLine("\n************************************************\n");
                                cancel = Console.ReadLine();


                                if (cancel == "yes" || cancel == "Yes" || cancel == "YES")
                                {
                                    loop = false;
                                }
                                else
                                {

                                    loop = true; ;

                                }
                            }//end of catch
                        }//end of while
                        
                        Console.WriteLine("\n##################################################################################\n");
                        decision = false;

                    } while (decision);


                    return MenuType.ProductBuyMenu;


                case "4":
                    /// <summary>
                    /// the process of this case
                    /// 1.user enters number
                    /// 1a.verifies if it is a number
                    /// 2.verifies if the entered product id is in the selected store
                    /// 2a.if it is not found it will go to the catch block
                    /// 2a1.user decideds to cancel or try again
                    /// 2b.if found removes it from shopping list and order list
                    /// 3.ends while loop
                    /// </summary>


                    bool decision3 = true;
                    do
                    {

                        LineItems _lines = new LineItems();

                        bool loop = true;
                        while (loop == true)
                        {
                            

                            Console.WriteLine("##################################################################################\n");
                            Console.WriteLine("\tEnter the Id of the product from the store to Remove from your cart");
                            int productsname = 0;
                            try
                            {
                                productsname = Convert.ToInt32(Console.ReadLine());

                            }
                            catch (System.Exception)
                            {

                                
                                Console.WriteLine("************************************************\n");
                                Console.WriteLine("you have entered something that was not a number please try again");
                                Console.WriteLine("Press Enter to continue");
                                Console.WriteLine("\n************************************************\n");
                                Console.ReadLine();
                                return MenuType.ProductBuyMenu;
                            }

                            string cancel;
                            try
                            {

                                _lines = parameterInter.VerifyStock(productsname, SingletonUser.currentstore);
                                if (tempdb.Exists(x => x.ProductEstablish.Name == _lines.ProductEstablish.Name))
                                {

                                    tempdb.RemoveAll(x => x.ProductEstablish.Name == _lines.ProductEstablish.Name);
                                    _details.ItemsList.RemoveAll(x => x.ProductEstablish.Name == _lines.ProductEstablish.Name);
                                    _details.TotalPrice = 0;

                                }
                                loop = false;

                            }
                            catch (System.Exception)
                            {
                                
                                Console.WriteLine("\n************************************************\n");
                                Console.WriteLine("\tPlease try again you have entered an item that is not listed in the store");
                                loop = true;
                                Console.WriteLine("\tWould you like to cancel the item? \n\ttype yes or no?\n");
                                Console.WriteLine("\n************************************************\n");
                                cancel = Console.ReadLine();


                                if (cancel == "yes" || cancel == "Yes" || cancel == "YES")
                                {
                                    loop = false;
                                }
                                else
                                {

                                    loop = true; ;

                                }
                            }//end of catch
                        }//end of while
                        
                        Console.WriteLine("\n##################################################################################\n");



                        decision3 = false;
                    } while (decision3);

                    Console.WriteLine("\n##################################################################################\n");

                    return MenuType.ProductBuyMenu;

                case "3":
                    /// <summary>
                    /// establish the values used to calculate
                    /// 1.clear orders list first to restablish the final list
                    /// 2.goes into loop in order to modify quantity of each item
                    /// 2a. verifies if user entered number if not cancels it
                    /// 2b.after all calculations it adds it to the orders list/final cart
                    /// 3. calculates total
                    /// 4. the temperary list adds the stock back to remove them in another case.
                    /// </summary>
                    decimal cost = 0;
                    int selectedamount = 0;
                    decimal payment = 0;
                    decimal linecost = 0;
                    decimal total = 0;

                    Console.WriteLine("\n##################################################################################\n");

                    linecost = 0;
                    cartResult.Clear();
                    _details.ItemsList.Clear();

                    foreach (LineItems obj in tempdb)
                    {


                        LineItems filler = new LineItems();

                        filler.ProductEstablish = obj.ProductEstablish;

                        Console.WriteLine(obj);
                        Console.WriteLine("\n\tType in the line item quantity you want to buy\n");
                        try
                        {
                            selectedamount = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (System.Exception)
                        {

                            
                            Console.WriteLine("************************************************\n");
                            Console.WriteLine("\tYou have entered something that was not a number please try again");
                            Console.WriteLine("\t The Cart will now be reset so you must add all items again");
                            Console.WriteLine("\tPress Enter to continue");
                            Console.WriteLine("\n************************************************\n");
                            Console.ReadLine();
                            _details.ItemsList.Clear();
                            tempdb.Clear();
                            _details.TotalPrice = 0;
                            return MenuType.ProductBuyMenu;
                        }
                        // selectedamount = Convert.ToInt32(Console.ReadLine());
                        obj.Quantity = obj.Quantity - selectedamount;
                        filler.Quantity = selectedamount;
                        cost = obj.ProductEstablish.PriceGrab();
                        payment = (cost * selectedamount);
                        linecost = payment;

                        total = total + payment;
                        string temp;
                        temp = "\n\t" + obj.ProductEstablish.Name + "======$" + obj.ProductEstablish.Price + "=========selecting " + selectedamount + " = $" + linecost;
                        cartResult.Add(temp);
                        cost = 0;
                        linecost = 0;
                        _details.ItemsList.Add(filler);

                    }
                    total = decimal.Round(total, 2, MidpointRounding.AwayFromZero);
                    _details.TotalPrice = total;
                    Console.WriteLine("\tTotal of cart is $" + total);
                    List<LineItems> values = _details.ItemsList;
                    for (int i = 0; i < _details.ItemsList.Count; i++)
                    {
                        tempdb[i].Quantity = tempdb[i].Quantity + values[i].Quantity;

                    }
                    Console.WriteLine("\tPress enter to continue");
                    Console.ReadLine();
                    return MenuType.ProductBuyMenu;


                case "2":
                    /// <summary>
                    /// 1. Asks the user if they want to complete the purchase
                    /// 2. if yes then verifies if there is a calculated price
                    /// 3. if true then verifies if the current balance is sufficient for the purchase
                    /// 4.if true then modifies customer record
                    /// 5. creates order
                    /// 6. gets the order id
                    /// 7.modifies the stock to be accurate then inserts into the db
                    /// 8. adds the lineitems to the history table
                    /// 9.prints the cart result values
                    /// 10.Clears list and total price values
                    /// </summary>
                    string confirmation;
                    Console.WriteLine("\tConfirm Purchase enter yes or no\n\tif you enter no or make a mistake, you must restart the order");
                    confirmation = Console.ReadLine();
                    if (confirmation == "yes" || confirmation == "Yes" || confirmation == "YES")
                    {
                        if (_details.TotalPrice == 0)
                        {
                            Console.WriteLine("\tError there is nothing in your cart, you must calculate the price before ordering");
                            Console.WriteLine("\tthe page and order will be reset. press enter to continue");
                            Console.ReadLine();
                            _details.ItemsList.Clear();
                            tempdb.Clear();
                            _details.TotalPrice = 0;
                            return MenuType.ProductBuyMenu;

                        }
                        else if ((SingletonUser.currentuser.Currency - _details.TotalPrice) >= 0)
                        {
                            SingletonUser.currentuser.Currency = SingletonUser.currentuser.Currency - _details.TotalPrice;
                            parameterInter.ModifyCustomerRecord(SingletonUser.currentuser);

                            Orders Test = new Orders();

                            parameterInter.AddOrdersBL(_details, SingletonUser.currentstore, SingletonUser.currentuser);
                            Test = parameterInter.GetOrderByID(Test);
                            List<LineItems> valuesfinal = _details.ItemsList;
                            for (int i = 0; i < _details.ItemsList.Count; i++)
                            {
                                tempdb[i].Quantity = tempdb[i].Quantity - valuesfinal[i].Quantity;

                            }

                            foreach (LineItems s in tempdb)
                            {
                                parameterInter.ModifyStockTable(SingletonUser.currentstore.Id, s.ProductEstablish.Id, s.Quantity);
                            }
                            foreach (LineItems s in _details.ItemsList)
                            {
                                parameterInter.InsertHistory(SingletonUser.currentstore.Id, s.ProductEstablish.Id, Test.Id, SingletonUser.currentuser.Id, s.Quantity);

                            }


                            Console.WriteLine("\nReceite:");
                            Console.WriteLine("\tStore: " + _details.Location.Name + "\n\t Address: " + _details.Location.Address);
                            foreach (string s in cartResult)
                            {
                                Console.WriteLine(s);
                            }

                            Console.WriteLine("\tTotal cost $" + _details.TotalPrice);
                            Console.WriteLine("\n##################################################################################\n");
                            _details.ItemsList.Clear();
                            tempdb.Clear();
                            _details.TotalPrice = 0;
                            Console.ReadLine();
                            return MenuType.ProductBuyMenu;
                        }
                        else
                        {
                            Console.WriteLine("\t We are sorry you currently dont have the sufficiente funds to complete the purchase.");
                            Console.WriteLine("\t You will need to modify your cart by selecting the option.\tPress enter to continue");
                            Console.ReadLine();
                            _details.ItemsList.Clear();
                            _details.TotalPrice = 0;
                            return MenuType.ProductBuyMenu;

                        }
                    }
                    else
                    {
                        _details.ItemsList.Clear();
                        tempdb.Clear();
                        _details.TotalPrice = 0;
                        Console.WriteLine("\tOrder has been canceled because you did not enter yes press enter to continue");
                        Console.ReadLine();
                        return MenuType.ProductBuyMenu;
                    }


                case "1":
                    tempdb.Clear();
                    _details.ItemsList.Clear();
                    _details.TotalPrice = 0;

                    return MenuType.ProductDisplayMenu;
                case "0":
                    tempdb.Clear();
                    _details.ItemsList.Clear();
                    _details.TotalPrice = 0;
                    return MenuType.MainMenu;
                default:
                    Console.WriteLine("Please input a valid response!");
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.ProductBuyMenu;
            }
        }
    }
}