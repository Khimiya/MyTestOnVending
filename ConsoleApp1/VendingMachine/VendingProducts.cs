
using MyTestOnVending.Constants;
using System;
using System.Collections.Generic;

namespace MyTestOnVending.VendingMachine
{

    public class VendingProducts : IVendingProducts
    {
        public decimal totalAmount { get; set; }
        public decimal costOfSelection { get; set; }
        public bool IsCoinToBeIserted { get; set; }
        public List<Coins> lstCoins { get; set; }
        public List<ProductDetails> lstProducts { get; set; }
        public IUserInput IUserInput { get; set; }
        public VendingProducts(IUserInput iUserInput)
        {
            IUserInput = iUserInput;
        }
        public void ResetValues()
        {
            lstCoins = VendingMachineConstants.ValidCoinList;
            lstProducts = VendingMachineConstants.ProductList;
            costOfSelection = 0;
            totalAmount = 0;
            IsCoinToBeIserted = false;
        }

        

        public void ShowMenu()
        {
            // set globally used values initially
            ResetValues();
            // show the products along with its price in vending display
            DisplayProductList();
            // check if valid product is selected and its has cost
            if (costOfSelection > 0)
            {
                // allow the user to choose the type of coin they need to insert
                TypeOfCoinInserted();
                int selectedCoinNumber;
                //Loop till inserted coins value is greater than or equals to the cost of selected product
                while (!IsCoinToBeIserted)
                {
                    selectedCoinNumber = IUserInput.GetConsoleInput();
                    //check if amount is sufficient to dispense the product or more coins required
                    SelectedCoinDeposited(selectedCoinNumber);
                }
            }
            else
            {
                Console.WriteLine("----  Wrong Selection. Try Again ----");
                //Restart whole transaction in case wrong selections are made
                ReDisplayMenu();

            }
        }


        public void ReDisplayMenu()
        {
            Console.WriteLine("----  Press 1 to show menu again ----");
            int reDisplayMenu = IUserInput.GetConsoleInput();
            
            if (reDisplayMenu == 1) { ShowMenu(); } else
            {
                throw new Exception();
            }
        }


        public void SelectedCoinDeposited(int c)
        {
            //check if the selection for type of coin is valid
            int idx = lstCoins.FindIndex(x => x.SerialNumber == c);
            // get the coinvalue by selected serial number
            decimal coinValue = idx > -1 ? lstCoins[idx].CoinValue : 0;
            if (idx > -1)
            {
                // set the totalamount deposited
                CoinsDeposited(coinValue);
                // check if product to be dispense or more coins prompt is required
                CheckTotal(lstCoins[idx].SerialNumber);
                // set this property to run the loop approriately (i.e till amount is sufficient to dispense selected product)
                IsCoinToBeIserted = IsTotalAmountEqualsToCost();
            }
            else
            {
                Console.WriteLine("----  Invalid coin. Returning back invalid coin and amount deposited by you {0}  ----", totalAmount);
                // setting to false to break the loop and return
                IsCoinToBeIserted = false;
                // Restart for new selections
                ReDisplayMenu();

            }
        }



        public void DisplayProductList()
        {
            Console.WriteLine("----------   Choose Product   -----------");
            foreach (ProductDetails p in lstProducts)
            {
                Console.WriteLine("----  Press {0} to purchase Product: {1} and Price: {2}    ----", p.SerialNumber, p.ProductName, p.Price);
            }



            int selectedProductNumber = IUserInput.GetConsoleInput();

            // set the cost of product base on selection
            SetCostOfSelectedProduct(selectedProductNumber);
        }



        private void SetCostOfSelectedProduct(int selectedProductNumber)
        {
            int idx = lstProducts.FindIndex(x => x.SerialNumber == selectedProductNumber);
            costOfSelection = idx > -1 ? lstProducts[idx].Price : 0;
        }



        private void TypeOfCoinInserted()
        {
            Console.WriteLine("----  Choose the type of coin you want to insert  ----");
            foreach (Coins c in lstCoins)
            {
                Console.WriteLine("Press {0} to insert coin of type {1}", c.SerialNumber, c.TypeOfCoin);
            }
        }



        public void CoinsDeposited(decimal coin)
        {
            totalAmount += coin;
        }



        public bool IsTotalAmountEqualsToCost()
        {
            return totalAmount >= costOfSelection;
        }



        public void CheckTotal(int selectedCoinNumber)
        {
            if (totalAmount >= costOfSelection)
            {
                Console.WriteLine("Thank you for using machine!!! Your refund is {0}", totalAmount - costOfSelection);
                //ShowMenu();
                ReDisplayMenu();
            }
            else
            {
                Console.WriteLine("DepositedAmount {0} and CostofSelection {1}. Press {2} to continue inserting coins", totalAmount, costOfSelection, selectedCoinNumber);
            }
        }



    }
}
