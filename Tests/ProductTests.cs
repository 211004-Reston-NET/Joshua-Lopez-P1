using System;
using Xunit;
using Models;

namespace Tests
{
    public class ProductTests
    {
        /// <summary>
        ///Verifies the entered name will set the name value 
        /// </summary>
        [Fact]
       
           public void NameShouldSet()
        {
            //Arrange
            Products tester = new Products();
            string _name = "TestName";
            //Act
            tester.Name = _name;
            //Assert
            Assert.NotNull(tester.Name);
            Assert.Equal(tester.Name, _name);
        }

        

        /// <summary>
        ///Verifies the entered contact will set the contact value 
        /// </summary>
        [Fact]
       
           public void CategoryShouldBeSet()
        {
            //Arrange
           Products tester = new Products();
            string _category = "Food";
            //Act
            tester.Category = _category;
            //Assert
            Assert.NotNull(tester.Category);
            Assert.Equal(tester.Category, _category);
        }
            /// <summary>
        ///Verifies the entered contact will set the contact value 
        /// </summary>
        [Fact]

         public void PriceSHouldbeSet()
        {
            //Arrange
           Products tester = new Products();
            decimal _price = 25;
            //Act
            tester.Price = _price;
            //Assert
            Assert.NotNull(tester.Price);
            Assert.Equal(tester.Price, _price);
        }

         /// <summary>
        ///Verifies the entered contact will set the contact value 
        /// </summary>
        [Fact]
       
           public void DescriptionShouldBeSet()
        {
            //Arrange
           Products tester = new Products();
            string _description = "Test Description";
            //Act
            tester.Description = _description;
            //Assert
            Assert.NotNull(tester.Description);
            Assert.Equal(tester.Description, _description);
        }


        
    }
}