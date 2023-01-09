using System.Threading.Tasks;

namespace MyTestOnVending.VendingMachine
{
    public interface IVendingProducts
    {
        
        void ShowMenu();
        void DisplayProductList();
        void ResetValues();
        void SelectedCoinDeposited(int typeOfCoinDeposited);
        
    }
}
