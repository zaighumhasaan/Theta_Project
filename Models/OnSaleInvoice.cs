using System;
using System.Collections.Generic;

namespace Pharmacy_POS.Models
{
    public partial class OnSaleInvoice
    {
        public int OnSaleInvoiceId { get; set; }
        public int? DrugId { get; set; }
        public int? SaleInvoiceId { get; set; }
        public double DrugQuantity { get; set; }
        public double DrugPriceTotal { get; set; }

        public virtual Drug? Drug { get; set; }
        public virtual SaleInvoice? SaleInvoice { get; set; }
    }
}
