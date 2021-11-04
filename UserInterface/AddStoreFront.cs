using System;
using BusinessLogic;
using Models;

namespace UserInterface
{
    public class AddStoreFront : IMenu
    {
        private static StoreFront store = new StoreFront();

        private InterfaceBL parameterInter;

        public AddStoreFront(InterfaceBL parameterobj)
        {
            parameterInter = parameterobj;
        }

        public void Menu()
        {
            
            Console.WriteLine("\n##################################################################################\n");
            Console.WriteLine("\tAdding a new Store Front");
            Console.WriteLine("\tName of store- " + store.Name);
            Console.WriteLine("\tAddress of store - " + store.Address);
            Console.WriteLine("\n##################################################################################\n");
            
            Console.WriteLine("\t[4] - Add StoreFront");
            Console.WriteLine("\t[3] - Input value for Name");
            Console.WriteLine("\t[2] - Input value for Address");
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
                        parameterInter.AddStoreFrontBL(store);
                        Console.WriteLine("\tStore created");
                        Console.WriteLine("\tPress enter to continue");
                        Console.ReadLine();
                        store = new StoreFront();
                    }
                    catch (System.Exception)
                    {

                        
                        Console.WriteLine("************************************************\n");
                        Console.WriteLine("Information is missing please make sure all fields are filled in");
                        Console.WriteLine("Press Enter to continue");
                        Console.WriteLine("\n************************************************\n");
                        Console.ReadLine();
                        return MenuType.AddStore;
                    }



                    return MenuType.MainMenu;
                case "3":
                    Console.WriteLine("\n##################################################################################\n");
                    Console.WriteLine("\tType in the value for the Name");
                    store.Name = Console.ReadLine();
                    return MenuType.AddStore;
                case "2":
                    Console.WriteLine("\n##################################################################################\n");
                    Console.WriteLine("\tType in the value for the Address");
                    store.Address = Console.ReadLine();
                    return MenuType.AddStore;


                case "0":
                    return MenuType.MainMenu;
                default:
                    Console.WriteLine("Please input a valid response!");
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.AddStore;
            }
        }
    }
}