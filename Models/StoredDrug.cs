using System;
using System.Collections.Generic;

namespace Pharmacy_POS.Models
{
    public partial class StoredDrug
    {
        public int DrugId { get; set; }
        public string BatchNo { get; set; } = null!;
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Quantity { get; set; }
    }
}
