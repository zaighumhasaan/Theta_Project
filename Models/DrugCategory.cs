using System;
using System.Collections.Generic;

namespace Pharmacy_POS.Models
{
    public partial class DrugCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? DangerousLevel { get; set; }
    }
}
