using System;
using BusinessLogic;
using Models;

namespace UserInterface
{
    public class AddProduct : IMenu
    {
        private static Products temp = new Products();
        private InterfaceBL parameterInter;

        public AddProduct(InterfaceBL parameterobj)
        {
            parameterInter = parameterobj;
        }

        public void Menu()
        {
            
            Console.WriteLine("\n##################################################################################\n");
            Console.WriteLine("\tAdding a new Product");
            Console.WriteLine("\tName - " + temp.Name);
            Console.WriteLine("\tPrice - " + temp.Price);
            Console.WriteLine("\tDescription - " + temp.Description);
            Console.WriteLine("\tCategory - " + temp.Category);
            Console.WriteLine("\n##################################################################################\n");
            
            Console.WriteLine("\t[5] - Add Product");
            Console.WriteLine("\t[4] - Input value for Name");
            Console.WriteLine("\t[3] - Input value for Price");
            Console.WriteLine("\t[2] - Input value for Description");
            Console.WriteLine("\t[1] - Input value for Category");
            Console.WriteLine("\t[0] - Go Back");
            
            Console.WriteLine("\n##################################################################################\n");
        }

        public MenuType YourChoice()
        {
            string userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "5":
                    try
                    {
                        parameterInter.AddProductsBL(temp);
                        Console.WriteLine("\tProduct created");
                        Console.WriteLine("\tPress enter to continue");
                        Console.ReadLine();
                        temp = new Products();
                    }
                    catch (System.Exception)
                    {

                         
                        Console.WriteLine("************************************************\n");
                        Console.WriteLine("Information is missing please make sure all fields are filled in");
                        Console.WriteLine("Press Enter to continue");
                        Console.WriteLine("\n************************************************\n");
                        Console.ReadLine();
                        return MenuType.AddProduct;
                    }

                    return MenuType.MainMenu;
                case "4":
                    Console.WriteLine("\n##################################################################################\n");
                    Console.WriteLine("\tType in the value for the productName");
                    temp.Name = Console.ReadLine();
                    return MenuType.AddProduct;
                case "3":
                    Console.WriteLine("\n##################################################################################\n");

                    Console.WriteLine("\tType in the value for the product");
                    try
                    {

                        temp.Price = Convert.ToDecimal(Console.ReadLine());
                    }
                    catch (System.Exception)
                    {

                        
                        temp.Price = 0;
                        Console.WriteLine("************************************************\n");
                        Console.WriteLine("you have entered something that was not a number please try again");
                        Console.WriteLine("Press Enter to continue");
                        Console.WriteLine("\n************************************************\n");
                        Console.ReadLine();
                    }
                    return MenuType.AddProduct;
                case "2":
                    Console.WriteLine("\n##################################################################################\n");

                    Console.WriteLine("\tType in the value for the Description");
                    temp.Description = Console.ReadLine();
                    return MenuType.AddProduct;
                case "1":
                    Console.WriteLine("\n##################################################################################\n");

                    Console.WriteLine("\tType in the value for the Category");
                    temp.Category = Console.ReadLine();

                    return MenuType.AddProduct;
                case "0":
                    return MenuType.MainMenu;
                default:
                    Console.WriteLine("Please input a valid response!");
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.AddProduct;
            }
        }
    }
}