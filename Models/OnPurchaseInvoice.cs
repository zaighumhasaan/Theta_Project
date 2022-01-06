using System;
using System.Collections.Generic;

namespace Pharmacy_POS.Models
{
    public partial class OnPurchaseInvoice
    {
        public int? DrugId { get; set; }
        public int PurchaseInvoiceId { get; set; }
        public int DrugQuantity { get; set; }
        public double DrugPriceTotal { get; set; }
        public string BatchNo { get; set; } = null!;
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime? DateOfEntry { get; set; }

        public virtual Drug? Drug { get; set; }
    }
}
