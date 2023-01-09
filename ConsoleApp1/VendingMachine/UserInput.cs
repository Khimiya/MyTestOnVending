using System;

namespace MyTestOnVending.VendingMachine
{
    
    public class UserInput: IUserInput
    {  
        public UserInput()
        {
            
        }       

        public int GetConsoleInput()
        {
            int selectedProductNumber;
            int.TryParse(Console.ReadLine(), out selectedProductNumber);
            return selectedProductNumber;
        }

        
    }
}
