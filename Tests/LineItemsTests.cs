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
            Products test=new Products();
            string _name = "TestName";
            //Act
            test.Name=_name;
            tester.ProductEstablish = test;
            //Assert
            Assert.NotNull(tester.ProductEstablish);
            Assert.Equal(tester.ProductEstablish, test);
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
        //     tester.ProductEstablish.Category = _category;
        //     //Assert
        //     Assert.NotNull(tester.ProductEstablish.Category);
        //     Assert.Equal(tester.ProductEstablish.Category, _category);
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
        //     tester.ProductEstablish.Price = _price;
        //     //Assert
        //     Assert.NotNull(tester.ProductEstablish.Price);
        //     Assert.Equal(tester.ProductEstablish.Price, _price);
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
        //     tester.ProductEstablish.Description = _description;
        //     //Assert
        //     Assert.NotNull(tester.ProductEstablish.Description);
        //     Assert.Equal(tester.ProductEstablish.Description, _description);
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
            Assert.NotNull(tester.Quantity);
            Assert.Equal(tester.Quantity, amount);
        }


        
    }
}