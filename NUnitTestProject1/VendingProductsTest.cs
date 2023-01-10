using Moq;
using MyTestOnVending.VendingMachine;
using NUnit.Framework;
using System;

namespace NUnitTestProject1
{
    [TestFixture]
    public class VendingProductsTest
    {
        public VendingProducts vendingProducts;
        public Mock<IUserInput> mockIUserInput;
        [SetUp]
        public void Setup()
        {
            mockIUserInput = new Mock<IUserInput>();
            vendingProducts = new VendingProducts(mockIUserInput.Object);
            vendingProducts.ResetValues();
        }

        [Test]
        public void ResetValues_WhenCalledSetsGloblaValueToDefault_Successfully_Test()
        {
            //Arrage
            vendingProducts.priceOfProduct = 10;
            vendingProducts.totalAmount = 10;
            vendingProducts.IsCoinToBeInserted = true;

            //Action
            vendingProducts.ResetValues();

            //Assert
            Assert.AreEqual(0, vendingProducts.priceOfProduct);
            Assert.AreEqual(0, vendingProducts.totalAmount);
            Assert.AreEqual(false, vendingProducts.IsCoinToBeInserted);
        }

        

        [Test]
        public void SetCostOfSelectedProduct_WhenValidProductNumberSelectedSetspriceOfProductToPrice_Test()
        {
            //Arrange            
            mockIUserInput.Setup(x => x.ConvertToIntUserInput()).Returns(1);
            
            
            //Action            
            vendingProducts.SetCostOfSelectedProduct();

            //Assert
            mockIUserInput.Verify(x => x.ConvertToIntUserInput(), Times.Once);
            Assert.Greater(vendingProducts.priceOfProduct, 0);
        }

        [Test]
        public void SetCostOfSelectedProduct_WhenInValidProductNumberSelectedSetsPriceOfProductToZero_Test()
        {
            //Arrange            
            mockIUserInput.Setup(x => x.ConvertToIntUserInput()).Returns(5);
            

            //Action            
            vendingProducts.SetCostOfSelectedProduct();

            //Assert
            mockIUserInput.Verify(x => x.ConvertToIntUserInput(), Times.Once);
            Assert.AreEqual(vendingProducts.priceOfProduct, 0);
        }

        [Test]
        public void SumOfCoinsDeposited_AddsCoinValueToTotalAmount_Successful_Test()
        {
            //Arrange
            
            vendingProducts.totalAmount = 0.25m;

            //Action
            vendingProducts.SumOfCoinsDeposited(0.25m);

            //Assert
            Assert.AreEqual(vendingProducts.totalAmount, 0.50m);

        }

        [Test]
        public void IsTotalAmountEqualsToCost_ReturnsTrue_WhenSumOfDepositedIsGreaterThanOrEqualsCostOfProduct_Test()
        {
            //Arrange            
            vendingProducts.totalAmount = 0.35m;
            vendingProducts.priceOfProduct = 0.25m;

            //Action
            bool result = vendingProducts.IsTotalAmountEqualsToCost();

            //Assert
            Assert.IsTrue(result);

        }

        [Test]
        public void IsTotalAmountEqualsToCost_ReturnsFalse_WhenSumOfDepositedIsLessThanCostOfProduct_Test()
        {
            //Arrange
            
            vendingProducts.totalAmount = 0.15m;
            vendingProducts.priceOfProduct = 0.25m;

            //Action
            bool result = vendingProducts.IsTotalAmountEqualsToCost();

            //Assert
            Assert.IsFalse(result);

        }

        [Test]
        public void DepositCoin_WhenTotalAmountGreaterThanOrEqualsToCostOfProduct_Test()
        {
            //Arrange 
            vendingProducts.priceOfProduct = 0.20m;
            vendingProducts.totalAmount=0.10m;


            //Action            
            vendingProducts.DepositCoin(3);

            //Assert            
            Assert.GreaterOrEqual(vendingProducts.totalAmount,vendingProducts.priceOfProduct);
            Assert.IsTrue(vendingProducts.IsCoinToBeInserted);

        }

        [Test]
        public void DepositCoin_WhenTotalAmountLessThanToCostOfProduct_Test()
        {
            //Arrange 
            vendingProducts.priceOfProduct = 0.20m;
            vendingProducts.totalAmount = 0.10m;


            //Action            
            vendingProducts.DepositCoin(1);

            //Assert            
            Assert.Less(vendingProducts.totalAmount, vendingProducts.priceOfProduct);
            Assert.IsFalse(vendingProducts.IsCoinToBeInserted);

        }

        [Test]
        public void DepositCoin_WhenInvalidCoinInsertedThenInvalidArgumentPassedToShowMenu_Test()
        {
            //Arrange 
            mockIUserInput.Setup(x => x.ConvertToIntUserInput()).Returns(0);
            vendingProducts.priceOfProduct = 0.20m;
            vendingProducts.totalAmount = 0.10m;


            //Action            
            var ex= Assert.Throws<Exception>(()=> { vendingProducts.DepositCoin(5); });

            //Assert   
            mockIUserInput.Verify(x => x.ConvertToIntUserInput(), Times.Once);            
            Assert.IsFalse(vendingProducts.IsCoinToBeInserted);

        }

    }
}