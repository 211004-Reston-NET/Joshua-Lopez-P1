using System;
using System.Collections.Generic;
using BusinessLogic;
using Models;

namespace UserInterface
{
    public class ReplenishInventoryMenu : IMenu
    {

        private InterfaceBL parameterInter;

        public ReplenishInventoryMenu(InterfaceBL parameterobj)
        {
            parameterInter = parameterobj;
        }

        public void Menu()
        {

            ///Process
            /// 1. Displays list of stores
            /// 2. Asks user for a number
            /// 3. verifies if number is entered if not then makes entered number equal 0
            /// 4. checks if number is not 0
            /// 5. verifies if that store is in the db
            /// 6. displays list of products in that found store
            /// 7. user enters the number for the product they want to modify
            /// 8. verifies if they entered a number
            /// 9. if numbered not entered then message appears
            /// 10.if number entered then verifies if it is in store
            /// 11. if not in store then it advises the user it was not found from the displayed list
            /// 12. user says if they wish to try again if no then they repeat steps 7 and onward
            /// 13. user asks if they want to add more to modify
            /// 14. modifies if said no
        
            Console.WriteLine("\n##################################################################################\n");
            Console.WriteLine("==========Store Replenish Menu=========");
            Console.WriteLine("\tList of stores");
            List<StoreFront> listOfstores = parameterInter.GetAllStoreFrontsBL();
            foreach (StoreFront s in listOfstores)
            {
                Console.WriteLine("\t\t\t\t" + s);
            }

            Console.WriteLine("Enter the Store Id number you want to see inventory for");
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
            }
            StoreFront store = new StoreFront();
            try
            {
                store = parameterInter.GetStoreByID(num);
            }
            catch (System.Exception)
            {
                store = null;
                Console.WriteLine("\t store not found/selected");

            }






            if (store != null)
            {
                Console.WriteLine("\tYour store is : " + store + "\n ");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("\tYour store current Line Products :\n ");
                foreach (LineItems p in parameterInter.GetInventory(store.Id))
                {
                    Console.WriteLine(p);
                }

                Console.WriteLine("----------------------------------");
                int amount = 0;
                LineItems _lines = new LineItems();
                List<LineItems> mod = new List<LineItems>();
                bool loop = true;
                while (loop == true)
                {
                    
                    Console.WriteLine("\n##################################################################################\n");
                    Console.WriteLine("\tEnter the Id of the product from the store to add stock");
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
                    }



                    string cancel;
                    try
                    {

                        _lines = parameterInter.VerifyStock(productsname, store);
                        loop = false;
                        if (mod.Exists(x => x.ProductEstablish.Name == _lines.ProductEstablish.Name))
                        {
                            Console.WriteLine("\tThis item is already in the queue to restock");

                        }
                        else
                        {
                            Console.WriteLine("\n##################################################################################\n");
                            Console.WriteLine("\tEnter the amount of the product you want to add to your inventory");
                            try
                            {
                                amount = Convert.ToInt32(Console.ReadLine());
                                _lines.Quantity = _lines.Quantity + amount;
                                mod.Add(_lines);
                            }
                            catch (System.Exception)
                            {

                                
                                Console.WriteLine("************************************************\n");
                                Console.WriteLine("you have entered something that was not a number please try again");
                                Console.WriteLine("Press Enter to continue");
                                Console.WriteLine("\n************************************************\n");
                                Console.ReadLine();
                            }

                        }

                    }
                    catch (System.Exception)
                    {
                        
                        Console.WriteLine("\n##################################################################################\n");
                        Console.WriteLine("\tplease try again you have entered the information wrong please enter from the products available");
                        loop = true;
                        Console.WriteLine("\tWould you like to cancel this add? \ttype yes or no\n");
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

                    string checkout;
                    Console.WriteLine("\nDo you wish to add more items to modify? \t type yes or no\n");
                    checkout = Console.ReadLine();


                    if (checkout == "yes" || checkout == "Yes" || checkout == "YES")
                    {
                        loop = true;
                    }
                    else
                    {

                        loop = false;

                    }
                }//end of while
                foreach (LineItems s in mod)
                {
                    parameterInter.ModifyStockTable(store.Id, s.ProductEstablish.Id, s.Quantity);
                    Console.WriteLine("Record has been updated");
                }



                Console.WriteLine("\n##################################################################################\n");


            }
            else
            {

            }





            Console.WriteLine("\t[5] - Main Menu");
            Console.WriteLine("\t[4] - start again");

        }

        public MenuType YourChoice()
        {
            string userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "5":

                    return MenuType.MainMenu;
                case "4":
                    return MenuType.ReplenishMenu;

                default:
                    Console.WriteLine("Please input a valid response!");
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.MainMenu;
            }
        }
    }
}