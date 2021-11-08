using System;
using Xunit;
using DataAccessLogic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Models;

namespace Tests
{
    public class RepositoryTest
    {
        private readonly DbContextOptions<P0DatabaseContext> _options;
        public RepositoryTest()
        {
            _options = new DbContextOptionsBuilder<P0DatabaseContext>()
                        .UseSqlite("Filename = Test.db").Options; //UseSqlite() method will create an inmemory database for use named Test.db

            Seed();
        }

        [Fact]
        public void GetAllCustomerShouldReturnAllCustomer()
        {
            using (var context = new P0DatabaseContext(_options))
            {
                //Arrange
                InterfaceRepository repo = new RespositoryCloud(context);

                //Act
                List<Customer> test = repo.GetAllCustomersDL();

                //Assert
                Assert.Equal(2, test.Count);
                Assert.Equal("Michael Smith", test[1].Name);
            }
        }

        [Fact]
        public void AddCustomerShouldAddACustomer()
        {
            //First using block will add a Customer
            using (var context = new P0DatabaseContext(_options))
            {
                //Arrange
                InterfaceRepository repo = new RespositoryCloud(context);
                Customer createdcustomer = new Customer
                {
                    Name = "Thor",
                    Address = "Dallas",
                    Email = "Thor@yahoo.com",
                    UserName = "TestUsername",
                    Password = "123456abc",
                    Age = 100,
                    CurrentCurrency = 50,
                    Category = "Manager"


                };

                //Act
                repo.AddCustomersDL(createdcustomer);
            }

            //Second using block will find that Customer and see if it is similar to what we added
            //Assert
            using (P0DatabaseContext contexts = new P0DatabaseContext(_options))
            {
                Customer result = contexts.Customers.Find(3);

                Assert.NotNull(result);
                Assert.Equal("Thor", result.Name);
                Assert.Equal("Dallas", result.Address);
                Assert.Equal("Thor@yahoo.com", result.Email);
            }
        }


        [Fact]
        public void GetAllStoreFrontShouldReturnAllStoreFront()
        {
            using (var context = new P0DatabaseContext(_options))
            {
                //Arrange
                InterfaceRepository repo = new RespositoryCloud(context);

                //Act
                List<StoreFront> test = repo.GetAllStoreFrontDL();

                //Assert
                Assert.Equal(2, test.Count);
                Assert.Equal("Test Store 2", test[1].StoreName);
            }
        }

        [Fact]
        public void AddStoreFrontShouldAddAStoreFront()
        {
            //First using block will add a StoreFront
            using (var context = new P0DatabaseContext(_options))
            {
                //Arrange
                InterfaceRepository repo = new RespositoryCloud(context);
                StoreFront createdstore = new StoreFront
                {
                    StoreName = "Walgreens",
                    Location = "123 playground st"



                };

                //Act
                repo.AddStoreFrontDL(createdstore);
            }

            //Second using block will find that Customer and see if it is similar to what we added
            //Assert
            using (P0DatabaseContext contexts = new P0DatabaseContext(_options))
            {
                StoreFront result = contexts.StoreFronts.Find(3);

                Assert.NotNull(result);
                Assert.Equal("Walgreens", result.StoreName);
                Assert.Equal("123 playground st", result.Location);
            }
        }



        [Fact]
        public void GetAllProductShouldReturnAllProduct()
        {
            using (var context = new P0DatabaseContext(_options))
            {
                //Arrange
                InterfaceRepository repo = new RespositoryCloud(context);

                //Act
                List<Products> test = repo.GetAllProductsDL();

                //Assert
                Assert.Equal(2, test.Count);
                Assert.Equal("Second product", test[1].Name);
            }
        }

        [Fact]
        public void AddProductsShouldAddAProducts()
        {
            //First using block will add a Productst
            using (var context = new P0DatabaseContext(_options))
            {
                //Arrange
                InterfaceRepository repo = new RespositoryCloud(context);
                Products createdProduct = new Products
                {
                    Name = "Soap",
                    Price = 10,
                    Description = "Lavender smell",
                    Category = "Cleaning"



                };

                //Act
                repo.AddProductsDL(createdProduct);
            }

            //Second using block will find that Customer and see if it is similar to what we added
            //Assert
            using (P0DatabaseContext contexts = new P0DatabaseContext(_options))
            {
                Products result = contexts.Products.Find(3);

                Assert.NotNull(result);
                Assert.Equal("Soap", result.Name);
                Assert.Equal(10, result.Price);
            }
        }





        [Fact]
        public void GetAllOrderShouldReturnAllOrder()
        {
            using (var context = new P0DatabaseContext(_options))
            {
                //Arrange
                InterfaceRepository repo = new RespositoryCloud(context);

                //Act
                List<Orders> test = repo.GetAllOrdersDL();

                //Assert
                Assert.Equal(2, test.Count);
                // Orders result = test.Find(x => x.OrderId == 2);

                Assert.NotNull(test);

                // Assert.Equal(1, test[0].StoreId);
                // Assert.Equal(2, test[0].CustomerId);
                // Assert.Equal(45, test[0].Total);
            }
        }

        [Fact]
        public void AddOrdersShouldAddAOrders()
        {
            //First using block will add a Orderst
            using (var context = new P0DatabaseContext(_options))
            {
                //Arrange
                InterfaceRepository repo = new RespositoryCloud(context);
                Orders createdOrder = new Orders
                {
                    CustomerId = 1,
                    StoreId = 1,
                    Total = Convert.ToDecimal(3.50)



                };

                //Act
                repo.AddOrdersDL(createdOrder);
            }

            //Second using block will find that Customer and see if it is similar to what we added
            //Assert
            using (P0DatabaseContext contexts = new P0DatabaseContext(_options))
            {
                Orders result = contexts.OrdersRecords.Find(3);

                Assert.NotNull(result);
                Assert.Equal(1, result.CustomerId);
                Assert.Equal(1, result.StoreId);
            }
        }


        [Fact]
        public void GetStoreInventory()
        {
            using (var context = new P0DatabaseContext(_options))
            {
                //Arrange
                InterfaceRepository repo = new RespositoryCloud(context);

                //Act
                List<LineItems> test = repo.GetInventory(1);

                //Assert
                Assert.Equal(2, test.Count);
                Assert.NotNull(test);

                Assert.Equal(1, test[0].StoreID);
                Assert.Equal(1, test[0].ProductID);
                Assert.Equal(10, test[0].Quantity);
            }
        }



        [Fact]
        public void GetProductbyId()
        {
            //First using block will add a Customer
            using (var context = new P0DatabaseContext(_options))
            {
                //Arrange
                InterfaceRepository repo = new RespositoryCloud(context);

                //Assert
                Products result = repo.GetProduct(2);

                Assert.NotNull(result);
                Assert.Equal("Second product", result.Name);
                Assert.Equal(Convert.ToDecimal(60.8), result.Price);
                Assert.Equal("Big", result.Description);
                Assert.Equal("Clothing", result.Category);
            }
        }



        [Fact]

        public void VerifyStorebyIDMustConfirm()
        {
            //First using block will add a Customer
            using (var context = new P0DatabaseContext(_options))
            {
                //Arrange
                InterfaceRepository repo = new RespositoryCloud(context);

                //Act
                bool result = repo.VerifyStorebyID(2);
                //Assert
                Assert.NotNull(result);
                Assert.Equal(true, result);


            }
        }

        [Fact]

        public void GetStoreByIDMustFind()
        {
            //First using block will add a Customer
            using (var context = new P0DatabaseContext(_options))
            {
                //Arrange
                InterfaceRepository repo = new RespositoryCloud(context);

                //Act
                StoreFront result = repo.GetStoreByID(2);
                //Assert
                Assert.NotNull(result);
                Assert.Equal("Test Store 2", result.StoreName);
                Assert.Equal("Location NY", result.Location);


            }
        }

        [Fact]

        public void GetMyOrderHistoryMustfind()
        {
            //First using block will add a Customer
            using (var context = new P0DatabaseContext(_options))
            {
                //Arrange
                InterfaceRepository repo = new RespositoryCloud(context);

                //Act
                List<Orders> result= repo.GetMyOrderHistory(2);
                //Assert
                Assert.NotNull(result);
                
                Assert.Equal(1,result.Count);
                Assert.Equal(1, result[0].StoreId);
                Assert.Equal(2, result[0].CustomerId);
                // Assert.Equal(20,result[0].Total);


            }
        }
































        private void Seed()
        {
            //using block to automatically close the resource that is used to connect to this db after seeding data to it
            using (var context = new P0DatabaseContext(_options))
            {
                //We want to make sure that our inmemory db gets deleted and recreated to not have any data from previous tests
                //We want a pristine database to ensure that every tests will have the exact same database being used to execute the test
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Customers.AddRange
                (
                    new Customer
                    {
                        Name = "John Doe",
                        Address = "Aguada",
                        Email = "tester@yahoo.com",
                        UserName = "Tester1",
                        Password = "testpass1",
                        Age = 10,
                        CurrentCurrency = 30,
                        Category = "Manager",
                    },
                    new Customer
                    {
                        Name = "Michael Smith",
                        Address = "New York",
                        Email = "tester2@outlook.com",
                        UserName = "ManTester2",
                        Password = "password2",
                        Age = 32,
                        CurrentCurrency = 69,
                        Category = "Client",
                    }

                );

                context.StoreFronts.AddRange(

                        new StoreFront
                        {
                            StoreName = "Store Test 1",
                            Location = "PR Location",

                        },
                    new StoreFront
                    {
                        StoreName = "Test Store 2",
                        Location = "Location NY",

                    }
                );

                context.Products.AddRange(
                    new Products
                    {
                        Name = "Item 1",
                        Price = Convert.ToDecimal(1.99),
                        Description = "small",
                        Category = "Miscellaneous"


                    },
                    new Products
                    {
                        Name = "Second product",
                        Price = Convert.ToDecimal(60.8),
                        Description = "Big",
                        Category = "Clothing"
                    }
                );
                context.SaveChanges();

                context.OrdersRecords.AddRange(
                    new Orders
                    {
                        CustomerId = 1,
                        StoreId = 2,
                        Total = 20
                    },
                    new Orders
                    {
                        CustomerId = 2,
                        StoreId = 1,
                        Total = 45
                    }
                );

                context.Stocks.AddRange(
                    new LineItems
                    {
                        StoreID = 1,
                        ProductID = 1,
                        Quantity = 10
                    },

                    new LineItems
                    {
                        StoreID = 1,
                        ProductID = 2,
                        Quantity = 33
                    },
                     new LineItems
                     {
                         StoreID = 2,
                         ProductID = 2,
                         Quantity = 40
                     }



                );


                context.SaveChanges();

                context.OrderHistories.AddRange(
                    new OrderLines
                    {
                        OrderId = 1,
                        CustomerId = 1,
                        StoreId = 2,
                        ProductId = 2,
                        LineQuantity = 4

                    },
                      new OrderLines
                      {
                          OrderId = 2,
                          CustomerId = 2,
                          StoreId = 1,
                          ProductId = 2,
                          LineQuantity = 6

                      }

                );
                context.SaveChanges();

            }
        }
    }
}