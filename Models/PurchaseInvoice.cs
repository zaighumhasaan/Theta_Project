using System;
using System.Collections.Generic;

namespace Pharmacy_POS.Models
{
    public partial class PurchaseInvoice
    {
        public int PurchaseInvoiceId { get; set; }
        public DateTime Date { get; set; }
        public double TotalAmount { get; set; }
        public int? EmpId { get; set; }
        public int? SupplierId { get; set; }
        public double? Discount { get; set; }
        public double NewPrice { get; set; }
        public double PayedAmount { get; set; }
        public double RemainingAmount { get; set; }

        public virtual Employee? Emp { get; set; }
        public virtual Supplier? Supplier { get; set; }
    }
}
