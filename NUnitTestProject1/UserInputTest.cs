
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
        public void ConvertToIntUserInput_ValidInput_ConvertsStringInputToInt_Successfully_Test()
        {
            //Arrange
            Console.SetIn(new StringReader("10"));

            //Action
            int result = userInput.ConvertToIntUserInput();

            //Assert
            Assert.AreEqual(10, result);
        }

        [Test]
        public void ConvertToIntUserInput_InvalidInput_WhenFailsToConvertStringToInt_SetsToZero_Test()
        {
            //Arrange
            Console.SetIn(new StringReader("abc"));

            //Action
            int result = userInput.ConvertToIntUserInput();

            //Assert
            Assert.AreEqual(0, result);

        }



    }
}