using System;

namespace MyTestOnVending.VendingMachine
{
    
    public class UserInput: IUserInput
    {  
        public UserInput()
        {
            
        }       

        public int ConvertToIntUserInput()
        {
            int selection;
            int.TryParse(Console.ReadLine(), out selection);
            return selection;
        }

        
    }
}
