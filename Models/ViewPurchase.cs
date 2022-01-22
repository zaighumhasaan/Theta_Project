namespace Pharmacy_POS.Models
{
    public class ViewPurchase
    {

        public PurchaseInvoice objPurchase { get; set; }
        public IList<OnPurchaseInvoice> ListPurchaseLine { get; set; }
    }
}
