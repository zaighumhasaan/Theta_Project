using System;
using System.Collections.Generic;

namespace Pharmacy_POS.Models
{
    public partial class SaleInvoice
    {
        public SaleInvoice()
        {
            OnSaleInvoices = new HashSet<OnSaleInvoice>();
        }

        public int SaleInvoiceId { get; set; }
        public int? EmpId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime Date { get; set; }
        public double TotalAmount { get; set; }
        public double? Discount { get; set; }
        public double NewPrice { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Employee? Emp { get; set; }
        public virtual ICollection<OnSaleInvoice> OnSaleInvoices { get; set; }
    }
}
