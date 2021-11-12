using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Models;

#nullable disable

namespace DataAccessLogic
{
    public partial class P0DatabaseContext : DbContext
    {
        public P0DatabaseContext()
        {
        }

        public P0DatabaseContext(DbContextOptions<P0DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<OrderLines> OrderHistories { get; set; }
        public virtual DbSet<Orders> OrdersRecords { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<LineItems> Stocks { get; set; }
        public virtual DbSet<StoreFront> StoreFronts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.HasIndex(e => e.UserName, "Customer_UN")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("CustomerID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentCurrency)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderLines>(entity =>
            {
                entity.HasKey(e => e.ReferenceId)
                    .HasName("PK__OrderHis__E1A99A79D614F7ED");

                entity.ToTable("OrderHistory");

                entity.Property(e => e.OrderId).HasColumnName("OrderId");
                entity.Property(e => e.ReferenceId).HasColumnName("ReferenceID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.LineQuantity).HasColumnName("line_quantity");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Customer_obj)
                    .WithMany(p => p.orderline_)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderHist__Custo__1AD3FDA4");

                entity.HasOne(d => d.Order_obj)
                    .WithMany(p => p.OrderHistories)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderHist__Order__19DFD96B");

                entity.HasOne(d => d.Product_obj)
                    .WithMany(p => p.OrderLine_)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderHist__Produ__1CBC4616");

                entity.HasOne(d => d.Store_obj)
                    .WithMany(p => p.orderline_)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderHist__Store__1BC821DD");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__OrdersRe__C3905BCF362EFFB7");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.Total).HasColumnType("money");

                entity.HasOne(d => d.Customer_obj)
                    .WithMany(p => p.MyOrders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrdersRec__Custo__160F4887");

                entity.HasOne(d => d.Store_obj)
                    .WithMany(p => p.OrdersRecords)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrdersRec__Store__17036CC0");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ProductID");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(16, 2)")
                    .HasDefaultValueSql("((0.00))");
            });

            modelBuilder.Entity<LineItems>(entity =>
            {
                entity.HasKey(e => new { e.StoreID, e.ProductID })
                    .HasName("PK__Stock__F0C23C8FFE8CD921");

                entity.ToTable("Stock");

                entity.Property(e => e.StoreID).HasColumnName("StoreID");

                entity.Property(e => e.ProductID).HasColumnName("ProductID");
                entity.Property(e => e.Quantity).HasColumnName("InStock");

                entity.HasOne(d => d.Product_obj)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.ProductID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Stock__ProductID__114A936A");

                entity.HasOne(d => d.Store_obj)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.StoreID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Stock__StoreID__10566F31");
            });

            modelBuilder.Entity<StoreFront>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__StoreFro__3B82F0E1A4CC44F6");

                entity.ToTable("StoreFront");

                entity.Property(e => e.Id).HasColumnName("StoreID");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}