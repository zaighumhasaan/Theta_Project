using System;
using System.Collections.Generic;

namespace Pharmacy_POS.Models
{
    public partial class Drug
    {
        public Drug()
        {
            OnPurchaseInvoices = new HashSet<OnPurchaseInvoice>();
            OnSaleInvoices = new HashSet<OnSaleInvoice>();
        }

        public int DrugId { get; set; }
        public string DrugName { get; set; } = null!;
        public int DrugCategory { get; set; }
        public string? ScientificName { get; set; }
        public string Manufacturer { get; set; } = null!;
        public double UnitPrice { get; set; }
        public double NoOfUnitsInPackage { get; set; }
        public double NoOfPackages { get; set; }
        public string? BatchNo { get; set; }
        public DateTime ManufacturedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public double? Quantity { get; set; }

        public virtual ICollection<OnPurchaseInvoice> OnPurchaseInvoices { get; set; }
        public virtual ICollection<OnSaleInvoice> OnSaleInvoices { get; set; }
    }
}
