using System;
using BusinessLogic;
using Models;
using System.Collections.Generic;

namespace UserInterface
{
    public class StoreMenu : IMenu
    {
        private InterfaceBL parameterInter;
        public static List<StoreFront> searchresult;
        public StoreMenu(InterfaceBL parameterobj)
        {
            parameterInter = parameterobj;
        }
        public void Menu()
        {
            
            Console.WriteLine("\n##################################################################################\n");
            Console.WriteLine("\tWelcome to the store select menu " + SingletonUser.currentuser.CustomerName.ToUpper());
            Console.WriteLine("\tPlease select the store");
            Console.WriteLine("---------------------------------------------------------------------\n");

            Console.WriteLine("\tList of StoreFronts");
            List<StoreFront> listOfStoreFront = parameterInter.GetAllStoreFrontsBL();
            
            if (searchresult != null)
            {
                foreach (StoreFront rest in searchresult)
                {
                    Console.WriteLine("============================================");
                    Console.WriteLine("\t" + rest);
                    Console.WriteLine("============================================");
                }
            }
            else
            {
                foreach (StoreFront rest in listOfStoreFront)
                {
                    Console.WriteLine("========================================");
                    Console.WriteLine("\t" + rest);
                    Console.WriteLine("========================================");
                }
            }

            
            Console.WriteLine("\t[3] - Search the store ");
            Console.WriteLine("\t[2] - Select the store (goes to products display");
            Console.WriteLine("\t[1] - goes to login");
            Console.WriteLine("\t[0] - exit to Main menu");
            

            Console.WriteLine("\n##################################################################################\n");

        }

        public MenuType YourChoice()
        {

            string userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "3":

                    Console.WriteLine("\tEnter store you want to search for");
                    string storen = Console.ReadLine();
                    List<StoreFront> test = new List<StoreFront>();

                    try
                    {
                        
                        foreach (StoreFront s in parameterInter.SearchStores(storen))
                        {
                            test.Add(s);

                        }
                        searchresult = test;
                        Console.WriteLine("\tClick Enter to see results");
                        Console.ReadLine();
                    }
                    catch (System.Exception)
                    {
                        

                        searchresult = null;
                        Console.WriteLine("\tStore was unfortunately not found please enter name as shown in list above");
                        Console.WriteLine("\tYou will be sent to Store display again ");
                        Console.WriteLine("\tPress Enter to continue");
                        Console.ReadLine();
                        return MenuType.StoreMenu;
                    }


                    return MenuType.StoreMenu;

                case "2":
                    
                    Console.WriteLine("\tEnter the id number of store you want to enter");
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
                        return MenuType.StoreMenu;

                    }
                    try
                    {

                        SingletonUser.currentstore = parameterInter.GetStoreByID(num);
                        Console.WriteLine("\tWelcome to " + SingletonUser.currentstore.Name + "\t press enter to continue");

                        Console.ReadLine();
                    }
                    catch (System.Exception)
                    {
                        
                        SingletonUser.currentstore = null;
                        Console.WriteLine("\tStore was unfortunately not found please enter name as shown in list above");
                        Console.WriteLine("\tYou will be sent to Store display again ");
                        Console.WriteLine("\tPress Enter to continue");
                        Console.ReadLine();
                        return MenuType.StoreMenu;
                    }


                    return MenuType.ProductDisplayMenu;
                case "1":
                    searchresult = null;
                    return MenuType.LoginMenu;
                case "0":
                    searchresult = null;
                    return MenuType.MainMenu;
                default:
                    Console.WriteLine("Please input a valid response!");
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.StoreMenu;
            }
        }
    }
}