using MyTestOnVending.VendingMachine;
using System;

namespace MyTestOnVending
{
    class Program
    {
        static IVendingProducts IVendingProducts { get; set; }
        static IUserInput IUserInput { get; set; }
        
        
        static void Main(string[] args)
        {
            IUserInput = new UserInput();
            IVendingProducts = new VendingProducts(IUserInput);
            //Console.WriteLine("Hello World!");
            try
            {
                IVendingProducts.ShowMenu();
               // userDisplay.ShowMenu();
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong, try again later");
                Environment.Exit(0);
            }
        }
    }
}
