using System;
using Models;
namespace UserInterface
{
    //The ":" syntax is used to indicate that you will inherit another class, interface, or abstract class
    public class MainMenu : IMenu
    {
        /*
            Since MainMenu has inherited IMenu, it will have all the methods we have created
            in IMenu.
            This is an example of Inheritance, one of the Object Oriented Pillars
        */
        public void Menu()
        {
            
            
            Console.WriteLine("##################################################################################\n");




            if (SingletonUser.currentuser == null)
            {
                Console.WriteLine("\tWelcome to the Main Menu for Lopez Shopping Center");
                Console.WriteLine("\tWhat do you want to do?");
                Console.WriteLine("---------------------------------------------------------------------\n");
                Console.WriteLine("\t[1] - Go Browsing");
                Console.WriteLine("\t[0] - Exit the store\n");

            }
            else
            {
                if (SingletonUser.currentuser.Position == "Manager")
                {
                    Console.WriteLine("\tWelcome " + SingletonUser.currentuser.CustomerName + " to the Main Menu for Lopez Shopping Center");
                    Console.WriteLine("\tWhat do you want to do?");
                    Console.WriteLine("---------------------------------------------------------------------\n");
                    Console.WriteLine("\t[1] - Go Browsing");
                    Console.WriteLine("\t[0] - Exit the store");
                    Console.WriteLine("\t[x] - Functions\n");


                }
                else
                {
                    Console.WriteLine("\tWelcome to the Main Menu for Lopez Shopping Center");
                    Console.WriteLine("\tWhat do you want to do?");
                    Console.WriteLine("--------------------------------------------------------------------\n");
                    Console.WriteLine("\t[1] - Go Browsing");
                    Console.WriteLine("\t[0] - Exit the store");
                    Console.WriteLine("\t[B] - MyProfile");

                }
            }
            Console.WriteLine("\n##################################################################################");









        }

        public MenuType YourChoice()
        {
            string userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "B":
                case "b":
                    return MenuType.MyProfile;
                case "x":
                    return MenuType.FunctionMenu;
                case "1":
                    return MenuType.LoginMenu;

                case "0":
                    return MenuType.Exit;

                default:
                    Console.WriteLine("Please input a valid response!");
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.MainMenu;
            }
        }
    }
}