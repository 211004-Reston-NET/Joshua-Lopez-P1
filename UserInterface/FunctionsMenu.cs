using System;

namespace UserInterface
{
    //The ":" syntax is used to indicate that you will inherit another class, interface, or abstract class
    public class FunctionMenu : IMenu
    {
        /*
            Since MainMenu has inherited IMenu, it will have all the methods we have created
            in IMenu.
            This is an example of Inheritance, one of the Object Oriented Pillars
        */
        public void Menu()
        {
            Console.WriteLine("\n##################################################################################\n");

            Console.WriteLine("\tThis is the function Menu");
            Console.WriteLine("\tWhat do you want to do?");

            Console.WriteLine("\t[1] - Go Browsing");
            Console.WriteLine("\t[0] - Exit the store");
            Console.WriteLine("\t[a] - MyProfile");
            Console.WriteLine("\t[b] - Replenish inventory");
            Console.WriteLine("\t[c] - Add Products");
            Console.WriteLine("\t[d] - Add StoreFront");
            // Console.WriteLine("\t[e] - show me info");
            // Console.WriteLine("\t[f] -test");
            Console.WriteLine("\t[e] - Add line item/stock table");
            Console.WriteLine("\t[f] - Show Store Order History");

            Console.WriteLine("\n##################################################################################\n");


        }

        public MenuType YourChoice()
        {
            string userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "1":
                    return MenuType.LoginMenu;

                case "0":
                    return MenuType.Exit;
                case "B":
                case "b":
                    return MenuType.ReplenishMenu;

                case "A":
                case "a":
                    return MenuType.MyProfile;
                case "C":
                case "c":
                    return MenuType.AddProduct;
                case "D":
                case "d":
                    return MenuType.AddStore;
                // case "E":
                // case "e":
                //     return MenuType.DisplayDB;
                // case "F":                    
                // case "f":
                //     return MenuType.loginconfirm;
                case "E":
                case "e":
                    return MenuType.AddLineItem;
                case "F":
                case "f":
                    return MenuType.ShowStoreOrders;

                default:
                    Console.WriteLine("Please input a valid response!");
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.MainMenu;
            }
        }
    }
}