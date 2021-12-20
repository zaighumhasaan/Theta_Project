﻿using System;
using System.Collections.Generic;

namespace Pharmacy_POS.Models
{
    public partial class Customer
    {
        public Customer()
        {
            SaleInvoices = new HashSet<SaleInvoice>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public decimal? MobileNumber { get; set; }
        public string? PharmacyName { get; set; }
        public double? Age { get; set; }

        public virtual ICollection<SaleInvoice> SaleInvoices { get; set; }
    }
}
