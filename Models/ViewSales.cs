namespace Pharmacy_POS.Models
{
    public class ViewSales
    {
        //Drugs properties 
        public SaleInvoice objSale { get; set; }
        public IList<OnSaleInvoice> ListSaleLine { get; set; }
    }
}
