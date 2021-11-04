using System;
using Xunit;
using Models;

namespace Tests
{
    public class CustomerTests
    {
        /// <summary>
        ///Verifies the entered name will set the name value 
        /// </summary>
        [Fact]
       
           public void NameShouldSetData()
        {
            //Arrange
            Customer tester = new Customer();
            string _name = "TestName";
            //Act
            tester.CustomerName = _name;
            //Assert
            Assert.NotNull(tester.CustomerName);
            Assert.Equal(tester.CustomerName, _name);
        }

        /// <summary>
        ///Verifies the entered address will set the address value 
        /// </summary>
        [Fact]
       
           public void AddressShouldSetData()
        {
            //Arrange
            Customer tester = new Customer();
            string _address = "TestAddress";
            //Act
            tester.Address = _address;
            //Assert
            Assert.NotNull(tester.Address);
            Assert.Equal(tester.Address, _address);
        }

        /// <summary>
        ///Verifies the entered contact will set the contact value 
        /// </summary>
        [Fact]
       
           public void ContactShouldSetData()
        {
            //Arrange
            Customer tester = new Customer();
            string _contact = "TestContact";
            //Act
            tester.Contact = _contact;
            //Assert
            Assert.NotNull(tester.Contact);
            Assert.Equal(tester.Contact, _contact);
        }

        /// <summary>
        ///Verifies the entered contact will set the contact value 
        /// </summary>
        [Fact]
       
           public void UsernameShouldSetData()
        {
            //Arrange
            Customer tester = new Customer();
            string _username = "TestUser";
            //Act
            tester.UserName = _username;
            //Assert
            Assert.NotNull(tester.UserName);
            Assert.Equal(tester.UserName, _username);
        }

        /// <summary>
        ///Verifies the entered contact will set the contact value 
        /// </summary>
        [Fact]
       
           public void PassWordShouldSetData()
        {
            //Arrange
            Customer tester = new Customer();
            string _password = "TestUser";
            //Act
            tester.Password = _password;
            //Assert
            Assert.NotNull(tester.Password);
            Assert.Equal(tester.Password, _password);
        }

        /// <summary>
        ///Verifies the entered contact will set the contact value 
        /// </summary>
        [Fact]
       
           public void OrderslistShouldAdd()
        {
            //Arrange
            Customer tester = new Customer();
            Orders item=new Orders();
            item.TotalPrice=Convert.ToDecimal(10.2);

            //Act
            tester.MyOrders.Add(item);
            //Assert
            Assert.NotNull(tester.MyOrders);
            Assert.Equal(tester.MyOrders[0], item);
        }


        
    }
}