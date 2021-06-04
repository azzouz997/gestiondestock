﻿// <auto-generated />
using System;
using ElamanaTakaful.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ElamanaTakaful.Infrastructure.Migrations
{
    [DbContext(typeof(ElamanaTakafulContext))]
    [Migration("20210602065425_CheckMig")]
    partial class CheckMig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.Function", b =>
                {
                    b.Property<int>("FunctionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("route")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FunctionId");

                    b.ToTable("Functions");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("int");

                    b.Property<string>("OrderReceiptId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropositionId")
                        .HasColumnType("int");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<DateTime>("ValidationEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ValidationStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ValidatorId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("PropositionId");

                    b.HasIndex("ValidatorId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.OrderHistory", b =>
                {
                    b.Property<int>("OrderHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("int");

                    b.Property<string>("OrderStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PropositionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidationEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ValidationStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ValidatorId")
                        .HasColumnType("int");

                    b.HasKey("OrderHistoryId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderHistory");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.PasswordHistory", b =>
                {
                    b.Property<int>("PasswordHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PasswordHistoryId");

                    b.HasIndex("UserId");

                    b.ToTable("PasswordHistory");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastBuyingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("QuantityInStock")
                        .HasColumnType("real");

                    b.Property<float>("QuantityUsed")
                        .HasColumnType("real");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.ProductHistory", b =>
                {
                    b.Property<int>("ProductHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastBuyingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("QuantityInStock")
                        .HasColumnType("real");

                    b.Property<float>("QuantityUsed")
                        .HasColumnType("real");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("ProductHistoryId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductHistory");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.Proposition", b =>
                {
                    b.Property<int>("PropositionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("AmountHT")
                        .HasColumnType("real");

                    b.Property<float>("AmountTTC")
                        .HasColumnType("real");

                    b.Property<string>("Direction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("PropositionNumber")
                        .HasColumnType("int");

                    b.Property<bool>("PropositionStatus")
                        .HasColumnType("bit");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<string>("QuoteId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ValidatorId")
                        .HasColumnType("int");

                    b.HasKey("PropositionId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SupplierId");

                    b.HasIndex("ValidatorId");

                    b.ToTable("Propositions");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.PropositionHistory", b =>
                {
                    b.Property<int>("PropositionHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("AmountHT")
                        .HasColumnType("real");

                    b.Property<float>("AmountTTC")
                        .HasColumnType("real");

                    b.Property<string>("Direction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("PropositionId")
                        .HasColumnType("int");

                    b.Property<int>("PropositionNumber")
                        .HasColumnType("int");

                    b.Property<bool>("PropositionStatus")
                        .HasColumnType("bit");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<string>("QuoteId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ValidatorId")
                        .HasColumnType("int");

                    b.HasKey("PropositionHistoryId");

                    b.HasIndex("PropositionId");

                    b.ToTable("PropositionHistory");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.QuoteHistory", b =>
                {
                    b.Property<int>("QuoteHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PropositionId")
                        .HasColumnType("int");

                    b.Property<string>("QuoteFileId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UptadeDate")
                        .HasColumnType("datetime2");

                    b.HasKey("QuoteHistoryId");

                    b.HasIndex("PropositionId");

                    b.ToTable("QuoteHistory");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.Supplier", b =>
                {
                    b.Property<int>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastBuyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrdersNumber")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SupplierId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.SupplierHistory", b =>
                {
                    b.Property<int>("SupplierHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastBuyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrdersNumber")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("SupplierHistoryId");

                    b.HasIndex("SupplierId");

                    b.ToTable("SupplierHistory");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeactivateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<bool>("UserStatus")
                        .HasColumnType("bit");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FunctionRole", b =>
                {
                    b.Property<int>("FunctionsFunctionId")
                        .HasColumnType("int");

                    b.Property<int>("RolesRoleId")
                        .HasColumnType("int");

                    b.HasKey("FunctionsFunctionId", "RolesRoleId");

                    b.HasIndex("RolesRoleId");

                    b.ToTable("FunctionRole");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.Order", b =>
                {
                    b.HasOne("ElamanaTakaful.Domain.Entities.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("ElamanaTakaful.Domain.Entities.Proposition", "Proposition")
                        .WithMany()
                        .HasForeignKey("PropositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ElamanaTakaful.Domain.Entities.User", "Validator")
                        .WithMany()
                        .HasForeignKey("ValidatorId");

                    b.Navigation("Creator");

                    b.Navigation("Proposition");

                    b.Navigation("Validator");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.OrderHistory", b =>
                {
                    b.HasOne("ElamanaTakaful.Domain.Entities.Order", "Order")
                        .WithMany("OrderHistory")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.PasswordHistory", b =>
                {
                    b.HasOne("ElamanaTakaful.Domain.Entities.User", null)
                        .WithMany("PasswordsHistory")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.Product", b =>
                {
                    b.HasOne("ElamanaTakaful.Domain.Entities.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.ProductHistory", b =>
                {
                    b.HasOne("ElamanaTakaful.Domain.Entities.Product", "Product")
                        .WithMany("ProductHistory")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.Proposition", b =>
                {
                    b.HasOne("ElamanaTakaful.Domain.Entities.Product", "Product")
                        .WithMany("Propositions")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("ElamanaTakaful.Domain.Entities.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ElamanaTakaful.Domain.Entities.User", "Validator")
                        .WithMany()
                        .HasForeignKey("ValidatorId");

                    b.Navigation("Product");

                    b.Navigation("Supplier");

                    b.Navigation("Validator");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.PropositionHistory", b =>
                {
                    b.HasOne("ElamanaTakaful.Domain.Entities.Proposition", "Proposition")
                        .WithMany("PropositionHistory")
                        .HasForeignKey("PropositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proposition");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.QuoteHistory", b =>
                {
                    b.HasOne("ElamanaTakaful.Domain.Entities.Proposition", null)
                        .WithMany("QuoteHistory")
                        .HasForeignKey("PropositionId");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.SupplierHistory", b =>
                {
                    b.HasOne("ElamanaTakaful.Domain.Entities.Supplier", "Supplier")
                        .WithMany("SupplierHistory")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.User", b =>
                {
                    b.HasOne("ElamanaTakaful.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("FunctionRole", b =>
                {
                    b.HasOne("ElamanaTakaful.Domain.Entities.Function", null)
                        .WithMany()
                        .HasForeignKey("FunctionsFunctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ElamanaTakaful.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderHistory");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.Product", b =>
                {
                    b.Navigation("ProductHistory");

                    b.Navigation("Propositions");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.Proposition", b =>
                {
                    b.Navigation("PropositionHistory");

                    b.Navigation("QuoteHistory");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.Supplier", b =>
                {
                    b.Navigation("SupplierHistory");
                });

            modelBuilder.Entity("ElamanaTakaful.Domain.Entities.User", b =>
                {
                    b.Navigation("PasswordsHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
