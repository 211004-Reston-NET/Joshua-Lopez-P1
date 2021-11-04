using System;
using System.Collections.Generic;
using BusinessLogic;
using Models;

namespace UserInterface
{
    public class MyProfile : IMenu
    {
        private static Customer client = SingletonUser.currentuser;
        private InterfaceBL parameterInter;

        public MyProfile(InterfaceBL parameterobj)
        {
            parameterInter = parameterobj;
        }

        public void Menu()
        {

            
            Console.WriteLine("\n##################################################################################\n");
            Console.WriteLine("\tEditing current user");
            Console.WriteLine("\tName - " + client.CustomerName);
            Console.WriteLine("\tAddress - " + client.Address);
            Console.WriteLine("\tContact - " + client.Contact);
            Console.WriteLine("\tusername - " + client.UserName);
            char[]test=client.Password.ToCharArray();
            string pass="";
            foreach (char item in test)
            {
                pass=pass+"*";
            }
            Console.WriteLine("\tPassword:"+pass);
            Console.WriteLine("\tAge - " + client._age);
            Console.WriteLine("\tPosition - " + client.Position);
            Console.WriteLine("\tAccount number - " + client.Id);
            Console.WriteLine("\tCurrent Balance: $" + client.Currency);
            client.MyOrders = parameterInter.GetMyOrderHistory(client.Id);

            Console.WriteLine("\n##################################################################################\n");



            
            Console.WriteLine("\t[x] - Save Changes");
            Console.WriteLine("\t[1] - Change value for Name");
            Console.WriteLine("\t[2] - Change value for Address");
            Console.WriteLine("\t[3] - Change value for contact");
            Console.WriteLine("\t[4] - Change value for age");
            // Console.WriteLine("\t[5] - Change username");
            // Console.WriteLine("\t[6] - Change password");
            Console.WriteLine("\t[5] - View my Order History");
            Console.WriteLine("\t[6] - Add funds");
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
                        //sends the logged in customers info to be updated in the db, if an error occured it will go to the catch block
                        parameterInter.ModifyCustomerRecord(client);
                        Console.WriteLine("\tAccount Modified");
                        Console.WriteLine("\tPress enter to continue");
                        Console.ReadLine();
                    }
                    catch (System.Exception)
                    {

                        
                        Console.WriteLine("************************************************\n");
                        Console.WriteLine("Information is missing please make sure all fields are filled in");
                        Console.WriteLine("Press Enter to continue");
                        Console.WriteLine("\n************************************************\n");
                        Console.ReadLine();
                        return MenuType.MyProfile;
                    }



                    return MenuType.LoginMenu;
                case "1":
                    
                    Console.WriteLine("\n##################################################################################\n");
                    Console.WriteLine("\tType in the value for the Name");
                    client.CustomerName = Console.ReadLine();
                    return MenuType.MyProfile;
                case "2":
                    
                    Console.WriteLine("\n##################################################################################\n");
                    Console.WriteLine("\tType in the value for the Address");
                    client.Address = Console.ReadLine();

                    return MenuType.MyProfile;
                case "3":
                    
                    Console.WriteLine("\n##################################################################################\n");
                    Console.WriteLine("\tType in the value for the Contact");
                    client.Contact = Console.ReadLine();
                    return MenuType.MyProfile;
                case "4":
                    
                    Console.WriteLine("\n##################################################################################\n");
                    Console.WriteLine("\tType in the value for the age");
                    try
                    {
                        client._age = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (System.Exception)
                    {

                        
                        Console.WriteLine("************************************************\n");
                        Console.WriteLine("you have entered something that was not a number please try again");
                        Console.WriteLine("Press Enter to continue");
                        Console.WriteLine("\n************************************************\n");
                        Console.ReadLine();
                    }

                    return MenuType.MyProfile;
                // case "5":
                //     
                //     Console.WriteLine("\n##################################################################################\n");
                //     Console.WriteLine("\tType in the value for the username");
                //     client.UserName = Console.ReadLine();
                //     return MenuType.MyProfile;
                // case "6":
                //     
                //     Console.WriteLine("\n##################################################################################\n");
                //     Console.WriteLine("\tType in the value for the password");

                //     client.Password = Console.ReadLine();
                //     return MenuType.MyProfile;

                case "5":
                    
                    foreach (Orders history in SingletonUser.currentuser.MyOrders)
                    {
                        Console.WriteLine("====================");
                        Console.WriteLine("\tOrder Id number: " + history.Id);
                        Console.WriteLine("\tBought from the store: " + history.Location.Name + " located in " + history.Location.Address);
                        Console.WriteLine("\tPurchase the following:");
                        foreach (LineItems s in history.ItemsList)
                        {
                            Console.WriteLine("\t" + s.ProductEstablish.Name);
                        }
                        Console.WriteLine("\tTotal cost of order was: " + history.TotalPrice);
                        Console.WriteLine("====================");
                    }
                    Console.WriteLine("\tpress enter to continue");
                    Console.ReadLine();
                    return MenuType.MyProfile;

                case "6":
                    
                    Console.WriteLine("\n##################################################################################\n");
                    Console.WriteLine("\tType in the value of funds to add");
                    decimal deposit = 0;
                    try
                    {
                        deposit = Convert.ToDecimal(Console.ReadLine());
                        client.Currency = client.Currency + deposit;
                    }
                    catch (System.Exception)
                    {

                        
                        Console.WriteLine("************************************************\n");
                        Console.WriteLine("you have entered something that was not a number please try again");
                        Console.WriteLine("Press Enter to continue");
                        Console.WriteLine("\n************************************************\n");
                        Console.ReadLine();
                    }

                    return MenuType.MyProfile;

                case "0":
                    return MenuType.MainMenu;
                default:
                    Console.WriteLine("Please input a valid response!");
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.MyProfile;
            }
        }
    }
}