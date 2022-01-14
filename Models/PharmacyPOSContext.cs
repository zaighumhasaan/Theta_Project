using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Pharmacy_POS.Models
{
    public partial class PharmacyPOSContext : DbContext
    {
        public PharmacyPOSContext()
        {
        }

        public PharmacyPOSContext(DbContextOptions<PharmacyPOSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Drug> Drugs { get; set; } = null!;
        public virtual DbSet<DrugCategory> DrugCategories { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<OnPurchaseInvoice> OnPurchaseInvoices { get; set; } = null!;
        public virtual DbSet<OnSaleInvoice> OnSaleInvoices { get; set; } = null!;
        public virtual DbSet<PurchaseInvoice> PurchaseInvoices { get; set; } = null!;
        public virtual DbSet<SaleInvoice> SaleInvoices { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-ID1N12D;Database=PharmacyPOS;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .HasColumnName("First_Name")
                    .IsFixedLength();

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .HasColumnName("Last_Name")
                    .IsFixedLength();

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("Mobile_Number")
                    .IsFixedLength();

                entity.Property(e => e.PharmacyName)
                    .HasMaxLength(25)
                    .HasColumnName("Pharmacy_Name")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Drug>(entity =>
            {
                entity.ToTable("Drug");

                entity.Property(e => e.DrugId).HasColumnName("Drug_ID");

                entity.Property(e => e.BatchNo)
                    .HasMaxLength(20)
                    .HasColumnName("Batch_No")
                    .IsFixedLength();

                entity.Property(e => e.DrugCategory).HasColumnName("Drug_Category");

                entity.Property(e => e.DrugName)
                    .HasMaxLength(30)
                    .HasColumnName("Drug_Name")
                    .IsFixedLength();

                entity.Property(e => e.ExpiryDate)
                    .HasColumnType("date")
                    .HasColumnName("Expiry_Date");

                entity.Property(e => e.ManufacturedDate)
                    .HasColumnType("date")
                    .HasColumnName("Manufactured_Date");

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.NoOfPackages).HasColumnName("No_Of_Packages");

                entity.Property(e => e.NoOfUnitsInPackage).HasColumnName("No_Of_Units_in_Package");

                entity.Property(e => e.ScientificName)
                    .HasMaxLength(30)
                    .HasColumnName("Scientific_Name")
                    .IsFixedLength();

                entity.Property(e => e.UnitPrice).HasColumnName("Unit_Price");
            });

            modelBuilder.Entity<DrugCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("Drug_Category");

                entity.Property(e => e.CategoryId).HasColumnName("Category_ID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(20)
                    .HasColumnName("Category_Name")
                    .IsFixedLength();

                entity.Property(e => e.DangerousLevel)
                    .HasMaxLength(15)
                    .HasColumnName("Dangerous_Level")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.ToTable("Employee");

                entity.Property(e => e.EmpId).HasColumnName("Emp_ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.DateOfWork)
                    .HasColumnType("date")
                    .HasColumnName("Date_Of_Work");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .HasColumnName("First_Name")
                    .IsFixedLength();

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .HasColumnName("Last_Name")
                    .IsFixedLength();

                entity.Property(e => e.MobileNumber)
                    .HasColumnType("numeric(11, 0)")
                    .HasColumnName("Mobile_Number");
            });

            modelBuilder.Entity<OnPurchaseInvoice>(entity =>
            {
                entity.HasKey(e => e.PurchaseInvoiceId)
                    .HasName("PK_On_Purchase_Invoice_1");

                entity.ToTable("On_Purchase_Invoice");

                entity.Property(e => e.PurchaseInvoiceId).HasColumnName("Purchase_Invoice_ID");

                entity.Property(e => e.BatchNo)
                    .HasMaxLength(20)
                    .HasColumnName("Batch_No")
                    .IsFixedLength();

                entity.Property(e => e.DateOfEntry)
                    .HasColumnType("date")
                    .HasColumnName("Date_Of_Entry");

                entity.Property(e => e.DrugId).HasColumnName("Drug_ID");

                entity.Property(e => e.DrugPriceTotal).HasColumnName("Drug_Price_Total");

                entity.Property(e => e.DrugQuantity).HasColumnName("Drug_Quantity");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnType("date")
                    .HasColumnName("Expiry_Date");

                entity.Property(e => e.ManufactureDate)
                    .HasColumnType("date")
                    .HasColumnName("Manufacture_Date");

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.OnPurchaseInvoices)
                    .HasForeignKey(d => d.DrugId)
                    .HasConstraintName("FK_On_Purchase_Invoice_Drug");
            });

            modelBuilder.Entity<OnSaleInvoice>(entity =>
            {
                entity.ToTable("On_Sale_Invoice");

                entity.Property(e => e.OnSaleInvoiceId).HasColumnName("On_Sale_Invoice_Id");

                entity.Property(e => e.DrugId).HasColumnName("Drug_ID");

                entity.Property(e => e.DrugPriceTotal).HasColumnName("_Drug_Price_Total");

                entity.Property(e => e.DrugQuantity).HasColumnName("Drug_Quantity");

                entity.Property(e => e.SaleInvoiceId).HasColumnName("Sale_Invoice_ID");

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.OnSaleInvoices)
                    .HasForeignKey(d => d.DrugId)
                    .HasConstraintName("FK_On_Sale_Invoice_Drug");

                entity.HasOne(d => d.SaleInvoice)
                    .WithMany(p => p.OnSaleInvoices)
                    .HasForeignKey(d => d.SaleInvoiceId)
                    .HasConstraintName("FK_On_Sale_Invoice_Sale_Invoice");
            });

            modelBuilder.Entity<PurchaseInvoice>(entity =>
            {
                entity.ToTable("Purchase_Invoice");

                entity.Property(e => e.PurchaseInvoiceId).HasColumnName("Purchase_Invoice_ID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.EmpId).HasColumnName("Emp_ID");

                entity.Property(e => e.NewPrice).HasColumnName("New_Price");

                entity.Property(e => e.PayedAmount).HasColumnName("Payed_Amount");

                entity.Property(e => e.RemainingAmount).HasColumnName("Remaining_Amount");

                entity.Property(e => e.SupplierId).HasColumnName("Supplier_ID");

                entity.Property(e => e.TotalAmount).HasColumnName("Total_Amount");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.PurchaseInvoices)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK_Purchase_Invoice_Employee");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.PurchaseInvoices)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Purchase_Invoice_Supplier");
            });

            modelBuilder.Entity<SaleInvoice>(entity =>
            {
                entity.ToTable("Sale_Invoice");

                entity.Property(e => e.SaleInvoiceId).HasColumnName("Sale_Invoice_ID");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.EmpId).HasColumnName("Emp_ID");

                entity.Property(e => e.NewPrice).HasColumnName("New_Price");

                entity.Property(e => e.TotalAmount).HasColumnName("Total_Amount");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SaleInvoices)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Sale_Invoice_Customer");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.SaleInvoices)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK_Sale_Invoice_Employee1");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.Property(e => e.SupplierId).HasColumnName("Supplier_ID");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(25)
                    .HasColumnName("Company_Name")
                    .IsFixedLength();

                entity.Property(e => e.MobileNumber)
                    .HasColumnType("numeric(11, 0)")
                    .HasColumnName("Mobile_Number");

                entity.Property(e => e.SupplierName)
                    .HasMaxLength(25)
                    .HasColumnName("Supplier_Name")
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
