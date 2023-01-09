using Moq;
using MyTestOnVending.VendingMachine;
using NUnit.Framework;
using System;
//using Xunit.Sdk;

namespace NUnitTestProject1
{
    [TestFixture]
    public class VendingProductsTest
    {
        public VendingProducts vp;
        public Mock<IVendingProducts> mockVp;
        public Mock<IUserInput> mockIUserInput;
        [SetUp]
        public void Setup()
        {
            mockIUserInput = new Mock<IUserInput>();
            vp = new VendingProducts(mockIUserInput.Object);
            vp.ResetValues();
        }

        [Test]
        public void ResetValues_Successful_Test()
        {
            //Arrage
            vp.costOfSelection = 10;
            vp.totalAmount = 10;
            vp.IsCoinToBeIserted = true;

            //Action
            vp.ResetValues();

            //Assert
            Assert.AreEqual(0, vp.costOfSelection);
            Assert.AreEqual(0, vp.totalAmount);
            Assert.AreEqual(false, vp.IsCoinToBeIserted);
        }

        [Test]
        public void SelectedCoinDeposited_Calls_GetConsoleInput_WhenAmountIsInSufficient_Successful_Test()
        {
            //Arrange            
            mockIUserInput.Setup(x => x.GetConsoleInput()).Returns(0);
            
            vp.costOfSelection = 0.25m;

            //Action            
            var ex= Assert.Throws<Exception>(()=> vp.SelectedCoinDeposited(9));

            //Assert
            mockIUserInput.Verify(x => x.GetConsoleInput(), Times.AtLeastOnce);
        }

        [Test]
        public void SelectedCoinDeposited_Calls_GetConsoleInput_InValidCoin_Successful_Test()
        {
            //Arrange            
            mockIUserInput.Setup(x => x.GetConsoleInput()).Returns(0);
            
            vp.costOfSelection = 0.50m;
            vp.totalAmount = 0.25m;

            //Action            
            
            var ex= Assert.Throws<Exception>(()=> vp.SelectedCoinDeposited(10));

            //Assert
            mockIUserInput.Verify(x => x.GetConsoleInput(), Times.Once);
            Assert.AreEqual(vp.IsCoinToBeIserted, false);
        }

        [Test]
        public void DisplayProductList_ValidSelection_Sets_CostOfSelection_Successful_Test()
        {
            //Arrange            
            mockIUserInput.Setup(x => x.GetConsoleInput()).Returns(1);
            
            
            //Action            
            vp.DisplayProductList();

            //Assert
            mockIUserInput.Verify(x => x.GetConsoleInput(), Times.Once);
            Assert.Greater(vp.costOfSelection, 0);
        }

        [Test]
        public void DisplayProductList_InValidSelection_Sets_CostOfSelectionToZero_Successful_Test()
        {
            //Arrange            
            mockIUserInput.Setup(x => x.GetConsoleInput()).Returns(5);
            

            //Action            
            vp.DisplayProductList();

            //Assert
            mockIUserInput.Verify(x => x.GetConsoleInput(), Times.Once);
            Assert.AreEqual(vp.costOfSelection, 0);
        }

        [Test]
        public void CoinsDeposited_Sets_TotalAmount_Successful_Test()
        {
            //Arrange
            
            vp.totalAmount = 0.25m;

            //Action
            vp.CoinsDeposited(0.25m);

            //Assert
            Assert.AreEqual(vp.totalAmount, 0.50m);

        }

        [Test]
        public void IsTotalAmountEqualsToCost_Returns_True_Test()
        {
            //Arrange
            
            vp.totalAmount = 0.35m;
            vp.costOfSelection = 0.25m;

            //Action
            bool result = vp.IsTotalAmountEqualsToCost();

            //Assert
            Assert.IsTrue(result);

        }

        [Test]
        public void IsTotalAmountEqualsToCost_Returns_False_Test()
        {
            //Arrange
            
            vp.totalAmount = 0.15m;
            vp.costOfSelection = 0.25m;

            //Action
            bool result = vp.IsTotalAmountEqualsToCost();

            //Assert
            Assert.IsFalse(result);

        }

        [Test]
        public void CheckTotal_DoesNotCall_RestartMenu_Test()
        {
            //Arrange
            mockIUserInput.Setup(x => x.GetConsoleInput()).Verifiable();            
            vp.totalAmount = 0.15m;
            vp.costOfSelection = 0.25m;

            //Action
            vp.CheckTotal(9);

            //Assert
            mockIUserInput.Verify(x=>x.GetConsoleInput(), Times.Never);
        }


    }
}