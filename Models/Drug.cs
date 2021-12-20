using System;
using System.Collections.Generic;

namespace Pharmacy_POS.Models
{
    public partial class Drug
    {
        public int DrugId { get; set; }
        public string DrugName { get; set; } = null!;
        public string ScientificName { get; set; } = null!;
        public string DrugCategory { get; set; } = null!;
        public string Manufacturer { get; set; } = null!;
        public double UnitPrice { get; set; }
        public int NoOfUnitsInPackage { get; set; }
    }
}
