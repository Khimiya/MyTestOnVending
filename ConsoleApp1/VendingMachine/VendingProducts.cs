
using MyTestOnVending.Constants;
using System;
using System.Collections.Generic;

namespace MyTestOnVending.VendingMachine
{

    public class VendingProducts : IVendingProducts
    {
        public decimal totalAmount { get; set; }
        public decimal priceOfProduct { get; set; }
        public bool IsCoinToBeInserted { get; set; }
        public List<Coins> lstCoins { get; set; }
        public List<ProductDetails> lstProducts { get; set; }
        public IUserInput IUserInput { get; set; }
        public VendingProducts(IUserInput iUserInput)
        {
            IUserInput = iUserInput;
            lstCoins = VendingMachineConstants.ValidCoinList;
            lstProducts = VendingMachineConstants.ProductList;
        }
        public void ResetValues()
        {
            priceOfProduct = 0;
            totalAmount = 0;
            IsCoinToBeInserted = false;
        }



        public void ShowMenu()
        {
            // set globally used values initially
            ResetValues();
            // show the products along with its price in vending display
            DisplayProductList();
            // set the cost of product selected by user
            SetCostOfSelectedProduct();
            // check if valid product is selected and it has cost
            if (priceOfProduct > 0)
            {
                // show valid type of coins list
                ShowCoinTypeList();
                int coinSerialNumber;
                //Loop till inserted coins value is greater than or equals to the cost of selected product
                while (!IsCoinToBeInserted)
                {
                    // get the type of coin selected
                    coinSerialNumber = IUserInput.ConvertToIntUserInput();
                    //check if amount is sufficient to dispense the product or more coins required
                    DepositCoin(coinSerialNumber);
                    
                }
                if (IsCoinToBeInserted) { ReDisplayMenu(); }
            }
            else
            {
                Console.WriteLine("----  Wrong Selection. Try Again ----");
                //Restart whole transaction in case wrong selections are made
                ReDisplayMenu();

            }
        }


        private void ReDisplayMenu()
        {
            Console.WriteLine("----  Press 1 to show menu again ----");
            int reDisplayMenu = IUserInput.ConvertToIntUserInput();

            if (reDisplayMenu == 1)
            {
                ShowMenu();
            }
            else
            {
                throw new Exception();
            }
        }


        public void DepositCoin(int coinSerialNumber)
        {
            //check if the selection for type of coin is valid
            int idx = lstCoins.FindIndex(x => x.SerialNumber == coinSerialNumber);
            // get the coinvalue by selected coin serial number
            decimal coinValue = idx > -1 ? lstCoins[idx].CoinValue : 0;
            //valid coin block
            if (idx > -1)
            {
                // set the totalamount deposited
                SumOfCoinsDeposited(coinValue);
                // set this property to run the loop approriately (i.e till amount is sufficient to dispense selected product)
                IsCoinToBeInserted = IsTotalAmountEqualsToCost();
                // check if product to be dispense or more coins prompt is required
                if (IsCoinToBeInserted)
                {
                    DispenseProductAndRefundBalanceIfAny();
                }
                else
                {
                    ContinueInsertingCoins();
                }

            }
            else
            {
                Console.WriteLine("----  Invalid coin. Returning back invalid coin and amount deposited by you {0}  ----", totalAmount);
                // setting to false to break the loop and return
                IsCoinToBeInserted = false;
                // Restart for new selections
                ReDisplayMenu();

            }
        }



        private void DisplayProductList()
        {
            Console.WriteLine("----------   Choose Product   -----------");
            foreach (ProductDetails p in lstProducts)
            {
                Console.WriteLine("----  Press {0} to purchase Product: {1} and Price: {2}    ----", p.SerialNumber, p.ProductName, p.Price);
            }
        }


        public void SetCostOfSelectedProduct()
        {
            int selectedProductNumber = IUserInput.ConvertToIntUserInput();
            int idx = lstProducts.FindIndex(x => x.SerialNumber == selectedProductNumber);
            priceOfProduct = idx > -1 ? lstProducts[idx].Price : 0;
        }



        private void ShowCoinTypeList()
        {
            Console.WriteLine("----  Choose the type of coin you want to insert  ----");
            foreach (Coins c in lstCoins)
            {
                Console.WriteLine("Press {0} to insert coin of type {1}({2})", c.SerialNumber, c.TypeOfCoin, c.CoinValue);
            }
        }



        public void SumOfCoinsDeposited(decimal coinValue)
        {
            totalAmount += coinValue;
        }



        public bool IsTotalAmountEqualsToCost()
        {
            return totalAmount >= priceOfProduct;
        }



        private void DispenseProductAndRefundBalanceIfAny()
        {
            Console.WriteLine("Thank you for using machine!!! Your refund is {0}", totalAmount - priceOfProduct);

        }

        private void ContinueInsertingCoins()
        {
            Console.WriteLine("Insufficient balance. DepositedAmount {0} and CostofProduct {1}. To continue", totalAmount, priceOfProduct);
            ShowCoinTypeList();
        }



    }
}
