using System;
using System.Collections.Generic;
using BusinessLogic;
using Models;

namespace UserInterface
{
    public class AddCustomerMenu : IMenu
    {
        //static field in order to be used every time the user returns to this menu
        private static Customer client = new Customer();
        private InterfaceBL parameterInter;

        public AddCustomerMenu(InterfaceBL parameterBL)
        {
            parameterInter = parameterBL;
        }

        public void Menu()
        {
            
            Console.WriteLine("##################################################################################\n");
            Console.WriteLine("Adding a new Account");
            Console.WriteLine("---------------------------------------------------------------------\n");
            
            Console.WriteLine("Name - " + client.CustomerName);
            Console.WriteLine("Address - " + client.Address);
            Console.WriteLine("Contact - " + client.Contact);
            Console.WriteLine("username - " + client.UserName);
            Console.WriteLine("password - " + client.Password);
            Console.WriteLine("Age - " + client._age);
            Console.WriteLine("Type: - " + client.Position);
            Console.WriteLine("Added Balance: - $" + client.Currency);
            Console.WriteLine("\n##################################################################################\n");

            
            Console.WriteLine("\t[x] - Add Customer");
            Console.WriteLine("\t[1] - Input value for Name");
            Console.WriteLine("\t[2] - Input value for Address");
            Console.WriteLine("\t[3] - Input value for contact");
            Console.WriteLine("\t[4] - Input value for age");
            Console.WriteLine("\t[5] - Enter username");
            Console.WriteLine("\t[6] - Enter password");
            Console.WriteLine("\t[7] - Enter Type of User");
            Console.WriteLine("\t[8] - Enter Currency");
            Console.WriteLine("\t[0] - Go Back");
            
            Console.WriteLine("\n##################################################################################\n");





        }

        public MenuType YourChoice()
        {
            string userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "x":
                    try
                    {
                       parameterInter.AddCustomersBL(client);
                    Console.WriteLine("\tAccount created");
                    Console.WriteLine("\tPress enter to continue");
                    Console.ReadLine();
                    client = new Customer();  
                    }
                    catch (System.Exception)
                    {

                        
                        Console.WriteLine("************************************************\n");
                        Console.WriteLine("Information is missing or The username is already in use please change username");
                        Console.WriteLine("Press Enter to continue");
                        Console.WriteLine("\n************************************************\n");
                        Console.ReadLine();
                        return MenuType.AddCustomers;
                    }
                    

                    return MenuType.LoginMenu;
                case "1":
                    Console.WriteLine("\n##################################################################################\n");
                    Console.WriteLine("\tType in the value for the Name");
                    client.CustomerName = Console.ReadLine();

                    return MenuType.AddCustomers;
                case "2":
                    Console.WriteLine("\n##################################################################################\n");
                    Console.WriteLine("\tType in the value for the Address");
                    client.Address = Console.ReadLine();
                    return MenuType.AddCustomers;
                case "3":
                    Console.WriteLine("\n##################################################################################\n");
                    Console.WriteLine("\tType in the value for the Contact");
                    client.Contact = Console.ReadLine();
                    return MenuType.AddCustomers;
                case "4":
                    Console.WriteLine("\tType in the value for the age");
                    try
                    {
                        client._age = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (System.Exception)
                    {

                        
                        client._age = 0;
                        Console.WriteLine("************************************************\n");
                        Console.WriteLine("you have entered something that was not a number please try again");
                        Console.WriteLine("Press Enter to continue");
                        Console.WriteLine("\n************************************************\n");
                        Console.ReadLine();
                    }
                    return MenuType.AddCustomers;
                case "5":
                    Console.WriteLine("\n##################################################################################\n");
                    Console.WriteLine("\tType in the value for the username");
                    client.UserName = Console.ReadLine();

                    return MenuType.AddCustomers;
                case "6":
                    Console.WriteLine("\n##################################################################################\n");
                    Console.WriteLine("\tType in the value for the password");
                    client.Password = Console.ReadLine();
                    return MenuType.AddCustomers;
                case "7":
                    Console.WriteLine("\n##################################################################################\n");
                    Console.WriteLine("\tType in the value for the Position");
                    client.Position = Console.ReadLine();
                    return MenuType.AddCustomers;

                case "8":
                    Console.WriteLine("\n##################################################################################\n");
                    Console.WriteLine("\tType in the value for the Currency, do not add dollar sign nor commas");
                    try
                    {
                        client.Currency = Convert.ToDecimal(Console.ReadLine());
                    }
                    catch (System.Exception)
                    {
                        
                        client.Currency = 0;
                        Console.WriteLine("************************************************\n");
                        Console.WriteLine("you have entered something that was not a number please try again");
                        Console.WriteLine("Press Enter to continue");
                        Console.WriteLine("\n************************************************\n");
                        Console.ReadLine();

                    }
                    return MenuType.AddCustomers;
                case "0":
                    return MenuType.MainMenu;
                default:
                    Console.WriteLine("Please input a valid response!");
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.AddCustomers;
            }
        }
    }
}