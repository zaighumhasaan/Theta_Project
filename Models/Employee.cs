using System;
using System.Collections.Generic;

namespace Pharmacy_POS.Models
{
    public partial class Employee
    {
        public Employee()
        {
            PurchaseInvoices = new HashSet<PurchaseInvoice>();
            SaleInvoices = new HashSet<SaleInvoice>();
        }

        public int EmpId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime? DateOfWork { get; set; }
        public decimal MobileNumber { get; set; }
        public double? Salary { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; }
        public virtual ICollection<SaleInvoice> SaleInvoices { get; set; }
    }
}
