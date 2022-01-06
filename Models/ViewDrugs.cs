namespace Pharmacy_POS.Models
{
    public class ViewDrugs
    {
        public int DrugId { get; set; }
        
        public string DrugName { get; set; } = null!;
        public string ScientificName { get; set; }
        public string Manufacturer { get; set; } = null!;
        public double UnitPrice { get; set; }
        public double NoOfUnitsInPackage { get; set; }
        public double NoOfPackages { get; set; }
        public string BatchNo { get; set; }
        public string CategoryName { get; set; }
        public DateTime ManufacturedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public double Quantity { get; set; }
    }
}
