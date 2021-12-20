using System;
using System.Collections.Generic;

namespace Pharmacy_POS.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            PurchaseInvoices = new HashSet<PurchaseInvoice>();
        }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public decimal MobileNumber { get; set; }

        public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; }
    }
}
