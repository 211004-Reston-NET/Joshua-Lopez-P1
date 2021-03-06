using System;
using Xunit;
using Models;

namespace Tests
{
    public class LineItemsTests
    {
        /// <summary>
        ///Verifies the entered name will set the name value 
        /// </summary>
        [Fact]

        public void NameShouldSet()
        {
            //Arrange
            LineItems tester = new LineItems();
            Products test = new Products();
            string _name = "TestName";
            //Act
            test.Name = _name;
            tester.Product_obj = test;
            //Assert
            Assert.NotNull(tester.Product_obj);
            Assert.Equal(tester.Product_obj, test);
        }



        // /// <summary>
        // ///Verifies the entered contact will set the contact value 
        // /// </summary>
        // [Fact]

        //    public void CategoryShouldBeSet()
        // {
        //     //Arrange
        //    LineItems tester = new LineItems();
        //     string _category = "Food";
        //     //Act
        //     tester.Product_obj.Category = _category;
        //     //Assert
        //     Assert.NotNull(tester.Product_obj.Category);
        //     Assert.Equal(tester.Product_obj.Category, _category);
        // }
        //     /// <summary>
        // ///Verifies the entered contact will set the contact value 
        // /// </summary>
        // [Fact]

        //  public void PriceSHouldbeSet()
        // {
        //     //Arrange
        //    LineItems tester = new LineItems();
        //     decimal _price = 25;
        //     //Act
        //     tester.Product_obj.Price = _price;
        //     //Assert
        //     Assert.NotNull(tester.Product_obj.Price);
        //     Assert.Equal(tester.Product_obj.Price, _price);
        // }

        //  /// <summary>
        // ///Verifies the entered contact will set the contact value 
        // /// </summary>
        // [Fact]

        //    public void DescriptionShouldBeSet()
        // {
        //     //Arrange
        //    LineItems tester = new LineItems();
        //     string _description = "Test Description";
        //     //Act
        //     tester.Product_obj.Description = _description;
        //     //Assert
        //     Assert.NotNull(tester.Product_obj.Description);
        //     Assert.Equal(tester.Product_obj.Description, _description);
        // }

        /// <summary>
        ///Verifies the entered contact will set the contact value 
        /// </summary>
        [Fact]

        public void QuantityShouldBeSet()
        {
            //Arrange
            LineItems tester = new LineItems();
            int amount = 5;
            //Act
            tester.Quantity = amount;
            //Assert
            Assert.Equal(tester.Quantity, amount);
        }



    }
}