using System;
using System.Collections.Generic;
using BusinessLogic;
using Models;

namespace UserInterface
{
    public class AddStock : IMenu
    {
        private static LineItems items = new LineItems();
        private static StoreFront store = new StoreFront();
        private InterfaceBL parameterInter;
         
        public AddStock(InterfaceBL parameterobj)
        {
            parameterInter = parameterobj;
        }

        public void Menu()
        {
            
            Console.WriteLine("##################################################################################\n");

            Console.WriteLine("Adding a new LineItem");
            Console.WriteLine("---------------------------------------------------------------------\n");
            
            Console.WriteLine(store);
            Console.WriteLine(items.ProductEstablish);
            Console.WriteLine("\tAmmount of Item : " + items.Quantity);
            
            Console.WriteLine("\n##################################################################################\n");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\t[4] - Add LineItem/Stock");
            Console.WriteLine("\t[3] - Input value for products name");
            Console.WriteLine("\t[2] - Input value for products quantity");
            Console.WriteLine("\t[1] - select store");
            Console.WriteLine("\t[0] - Go Back");
            
            Console.WriteLine("\n##################################################################################\n");

        }

        public MenuType YourChoice()
        {
            string userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "4":
                    try
                    {
                        parameterInter.AddStock(store, items.ProductEstablish, items.Quantity);
                        Console.WriteLine("\t Product Added to store");
                        Console.WriteLine("\tPress enter to continue");
                        Console.ReadLine();
                        items = new LineItems();
                        store = new StoreFront();
                    }
                    catch (System.Exception)
                    {

                        
                        Console.WriteLine("************************************************\n");
                        Console.WriteLine("The store already has this item in its inventory or information is missing");
                        Console.WriteLine("Press Enter to continue");
                        Console.WriteLine("\n************************************************\n");
                        Console.ReadLine();
                        return MenuType.AddLineItem;
                    }



                    return MenuType.MainMenu;
                case "3":
                    
                    Console.WriteLine("\n##################################################################################\n");
                    Console.WriteLine("---------------------------------------------------------------------\n");
                    List<Products> listOfRestaurants = parameterInter.GetAllProductsBL();
                    
                    foreach (Products rest in listOfRestaurants)
                    {
                        Console.WriteLine("====================");
                        Console.WriteLine(rest);
                        Console.WriteLine("====================");
                    }
                    Console.WriteLine("\n##################################################################################\n");
                    Console.WriteLine("\tType in the product Id u want");
                    int num = 0;
                    try
                    {
                        num = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (System.Exception)
                    {

                        

                        Console.WriteLine("************************************************\n");
                        Console.WriteLine("you have entered something that was not a number please try again");
                        Console.WriteLine("Press Enter to continue");
                        Console.WriteLine("\n************************************************\n");
                        Console.ReadLine();
                        return MenuType.AddLineItem;

                    }

                    Console.WriteLine("\n##################################################################################\n");
                    try
                    {
                        items.ProductEstablish = parameterInter.GetProduct(num);
                    }
                    catch (System.Exception)
                    {

                        
                        items.ProductEstablish = null;
                        Console.WriteLine("\n************************************************\n");
                        Console.WriteLine("\tplease try again you have entered an Id that is not listed");
                        Console.WriteLine("\tPress enter to continue");
                        Console.ReadLine();
                    }
                    return MenuType.AddLineItem;
                case "2":
                    
                    Console.WriteLine("\n##################################################################################\n");
                    Console.WriteLine("\tType in the value for the quantity");
                    try
                    {
                        items.Quantity = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (System.Exception)
                    {

                        
                        items.Quantity = 0;
                        Console.WriteLine("************************************************\n");
                        Console.WriteLine("you have entered something that was not a number please try again");
                        Console.WriteLine("Press Enter to continue");
                        Console.WriteLine("\n************************************************\n");
                        Console.ReadLine(); ;
                    }


                    return MenuType.AddLineItem;
                case "1":

                    
                    Console.WriteLine("\n##################################################################################\n");
                    Console.WriteLine("---------------------------------------------------------------------\n");
                    
                    List<StoreFront> listofstores = parameterInter.GetAllStoreFrontsBL();

                    foreach (StoreFront rest in listofstores)
                    {
                        Console.WriteLine("====================");
                        Console.WriteLine(rest);
                        Console.WriteLine("====================");
                    }

                    
                    Console.WriteLine("\n##################################################################################\n");
                    Console.WriteLine("\tEnter store you want to enter by Id");
                    int number = 0;
                    try
                    {
                        number = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (System.Exception)
                    {

                        
                        Console.WriteLine("************************************************\n");
                        Console.WriteLine("you have entered something that was not a number please try again");
                        Console.WriteLine("Press Enter to continue");
                        Console.WriteLine("\n************************************************\n");
                        Console.ReadLine();
                        return MenuType.AddLineItem;

                    }

                    try
                    {

                        store = parameterInter.GetStoreByID(number);

                    }
                    catch (System.Exception)
                    {
                        
                        SingletonUser.currentstore = null;
                        Console.WriteLine("\tStore was unfortunately not found please enter Id as shown in list above");
                        Console.WriteLine("\tYou will be sent to Store display again ");
                        Console.WriteLine("\tPress Enter to continue");
                        Console.ReadLine();
                    }
                    return MenuType.AddLineItem;
                case "0":
                    return MenuType.MainMenu;
                default:
                    Console.WriteLine("Please input a valid response!");
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.AddLineItem;
            }
        }
    }
}