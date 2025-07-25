﻿// <auto-generated />
using System;
using APIStoreManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APIStoreManagement.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APIStoreManagement.Models.Clothing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PatternId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SizeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatternId");

                    b.HasIndex("SizeId");

                    b.ToTable("Clothings");
                });

            modelBuilder.Entity("APIStoreManagement.Models.Costs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("DailySalesAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Costs");
                });

            modelBuilder.Entity("APIStoreManagement.Models.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClothingId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClothingId");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("APIStoreManagement.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClothingId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Deposit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDelivered")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClothingId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("APIStoreManagement.Models.Pattern", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Patterns");
                });

            modelBuilder.Entity("APIStoreManagement.Models.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClothingId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("InitialDeposit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("OriginatingOrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Soldprice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("TotalSalesAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ClothingId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("APIStoreManagement.Models.Sizes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("SizeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("APIStoreManagement.Models.Clothing", b =>
                {
                    b.HasOne("APIStoreManagement.Models.Pattern", "Pattern")
                        .WithMany("Clothings")
                        .HasForeignKey("PatternId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("APIStoreManagement.Models.Sizes", "Size")
                        .WithMany("Clothings")
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pattern");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("APIStoreManagement.Models.Inventory", b =>
                {
                    b.HasOne("APIStoreManagement.Models.Clothing", "Clothing")
                        .WithMany("Inventories")
                        .HasForeignKey("ClothingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clothing");
                });

            modelBuilder.Entity("APIStoreManagement.Models.Order", b =>
                {
                    b.HasOne("APIStoreManagement.Models.Clothing", "Clothing")
                        .WithMany()
                        .HasForeignKey("ClothingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clothing");
                });

            modelBuilder.Entity("APIStoreManagement.Models.Sale", b =>
                {
                    b.HasOne("APIStoreManagement.Models.Clothing", "Clothing")
                        .WithMany("Sales")
                        .HasForeignKey("ClothingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clothing");
                });

            modelBuilder.Entity("APIStoreManagement.Models.Clothing", b =>
                {
                    b.Navigation("Inventories");

                    b.Navigation("Sales");
                });

            modelBuilder.Entity("APIStoreManagement.Models.Pattern", b =>
                {
                    b.Navigation("Clothings");
                });

            modelBuilder.Entity("APIStoreManagement.Models.Sizes", b =>
                {
                    b.Navigation("Clothings");
                });
#pragma warning restore 612, 618
        }
    }
}
