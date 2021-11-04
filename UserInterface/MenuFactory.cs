using BusinessLogic;
using DataAccessLogic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DataAccessLogic.Entities;
namespace UserInterface
{
    /// <summary>
    /// Designed to create menu objects
    /// </summary>
    public class MenuFactory : IFactoryPattern
    {
        public IMenu GetMenu(MenuType p_menu)
        {
            var configuration = new ConfigurationBuilder() //Configurationbuilder is the class that came from the Microsoft.extensions.configuration package
                .SetBasePath(Directory.GetCurrentDirectory()) //Gets the current directory of the Userinterface file path
                .AddJsonFile("appsetting.json") //Adds the appsetting.json file in our userinterface folder
                .Build(); //Builds our configuration

            DbContextOptions<P0DatabaseContext> options = new DbContextOptionsBuilder<P0DatabaseContext>()
                .UseSqlServer(configuration.GetConnectionString("Reference2DB"))
                .Options;
            switch (p_menu)
            {


                case MenuType.MainMenu:
                    //If user choosed to go back to the MainMenu
                    //page will start pointing to a MainMenu object instead
                    return new MainMenu();

                case MenuType.LoginMenu:
                    //This will point the page reference variable to a new MenuType Object
                    //Since Menu Type Object has different implementation/function of the Menu Method
                    //It will have different implementations/functions when the while loop goes back and
                    //repeat itself
                    return new LoginMenu(new BL(new RespositoryCloud(new P0DatabaseContext(options))));


                case MenuType.AddCustomers:
                //add customer menu has a constructor with a BL type object
                //That BL object has a constructor with a IRepository type object
                //that datalogic type object has a constructor that needs a P0DatabaseContext object
                //the PoDatabaseContext object needs a DbContextOptions<P0DatabaseContext> type object
                    return new AddCustomerMenu(new BL(new RespositoryCloud(new P0DatabaseContext(options))));

                case MenuType.StoreMenu:

                    return new StoreMenu(new BL(new RespositoryCloud(new P0DatabaseContext(options))));

                case MenuType.ProductDisplayMenu:

                    return new ProductMenuDisplay(new BL(new RespositoryCloud(new P0DatabaseContext(options))));

                case MenuType.ProductBuyMenu:
                    return new ProductBuyMenu(new BL(new RespositoryCloud(new P0DatabaseContext(options))));

                case MenuType.ReplenishMenu:
                    return new ReplenishInventoryMenu(new BL(new RespositoryCloud(new P0DatabaseContext(options))));

                case MenuType.MyProfile:
                    return new MyProfile(new BL(new RespositoryCloud(new P0DatabaseContext(options))));




                // case MenuType.loginconfirm:
                //     return new LoginConfirmationMenu(new BL(new RespositoryCloud(new P0DatabaseContext(options))));
                // case MenuType.DisplayDB:
                //     return new DisplayDBinfo(new BL(new RespositoryCloud(new P0DatabaseContext(options))));
                // case MenuType.ShowCustomers:
                //     return new ShowCustomers(new BL(new RespositoryCloud(new P0DatabaseContext(options))));
                case MenuType.AddStore:
                    return new AddStoreFront(new BL(new RespositoryCloud(new P0DatabaseContext(options))));


                case MenuType.AddProduct:
                    return new AddProduct(new BL(new RespositoryCloud(new P0DatabaseContext(options))));
                case MenuType.FunctionMenu:
                    return new FunctionMenu();
                case MenuType.AddLineItem:
                    return new AddStock(new BL(new RespositoryCloud(new P0DatabaseContext(options))));
                case MenuType.ShowStoreOrders:
                    return new ShowStoreOrders(new BL(new RespositoryCloud(new P0DatabaseContext(options))));


                default:
                    return null;
            }
        }
    }
}