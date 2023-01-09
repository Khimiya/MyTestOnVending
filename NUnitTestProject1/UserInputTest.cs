
using MyTestOnVending.VendingMachine;
using NUnit.Framework;
using System;
using System.IO;

namespace NUnitTestProject1
{
    [TestFixture]
    public class UserInputTest
    {
        public UserInput userInput;
        [SetUp]
        public void Setup()
        {
            userInput = new UserInput();
        }

        [Test]
        public void GetConsoleInput_Successful()
        {
            //Arrage
            Console.SetIn(new StringReader("10"));

            //Action
            int result = userInput.GetConsoleInput();

            //Assert
            Assert.AreEqual(10, result);
        }

        [Test]
        public void GetConsoleInput_InvalidInput()
        {
            //Arrage
            Console.SetIn(new StringReader("abc"));

            //Action & Assert
            Assert.DoesNotThrow(()=> userInput.GetConsoleInput());
            
        }



    }
}