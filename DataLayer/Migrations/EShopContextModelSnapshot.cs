﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLayer.Migrations
{
    [DbContext(typeof(EShopContext))]
    partial class EShopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataLayer.Models.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("DataLayer.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(8,2)");

                    b.Property<int?>("TagId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("TagId");

                    b.ToTable("Product");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Product");
                });

            modelBuilder.Entity("DataLayer.Models.StockStatus", b =>
                {
                    b.Property<int>("StockStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("NextStock")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("StockStatusId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("StockStatus");
                });

            modelBuilder.Entity("DataLayer.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("ImageProduct", b =>
                {
                    b.Property<int>("ImagesImageId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsProductId")
                        .HasColumnType("int");

                    b.HasKey("ImagesImageId", "ProductsProductId");

                    b.HasIndex("ProductsProductId");

                    b.ToTable("ImageProduct");
                });

            modelBuilder.Entity("DataLayer.Models.Products.DigitalDecoder", b =>
                {
                    b.HasBaseType("DataLayer.Models.Product");

                    b.Property<bool>("Sound")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("DigitalDecoder");
                });

            modelBuilder.Entity("DataLayer.Models.Products.Locomotive", b =>
                {
                    b.HasBaseType("DataLayer.Models.Product");

                    b.Property<bool>("AutoCoupling")
                        .HasColumnType("bit");

                    b.Property<int>("Control")
                        .HasColumnType("int");

                    b.Property<int?>("DigitalDecoderId")
                        .HasColumnType("int");

                    b.Property<int>("Epoch")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("int");

                    b.Property<float>("Length")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("real");

                    b.Property<int>("LocoType")
                        .HasColumnType("int");

                    b.Property<int>("NumOfAxels")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("int");

                    b.Property<int>("NumOfDrivenAxels")
                        .HasColumnType("int");

                    b.Property<int>("Scale")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("int");

                    b.HasIndex("DigitalDecoderId");

                    b.HasDiscriminator().HasValue("Locomotive");
                });

            modelBuilder.Entity("DataLayer.Models.Products.RailCar", b =>
                {
                    b.HasBaseType("DataLayer.Models.Product");

                    b.Property<int>("Epoch")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("int");

                    b.Property<float>("Length")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("real");

                    b.Property<int>("NumOfAxels")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("int");

                    b.Property<int>("Scale")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("RailCar");
                });

            modelBuilder.Entity("DataLayer.Models.Product", b =>
                {
                    b.HasOne("DataLayer.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("DataLayer.Models.StockStatus", b =>
                {
                    b.HasOne("DataLayer.Models.Product", "Product")
                        .WithOne("StockStatus")
                        .HasForeignKey("DataLayer.Models.StockStatus", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ImageProduct", b =>
                {
                    b.HasOne("DataLayer.Models.Image", null)
                        .WithMany()
                        .HasForeignKey("ImagesImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataLayer.Models.Products.Locomotive", b =>
                {
                    b.HasOne("DataLayer.Models.Products.DigitalDecoder", "DigitalDecoder")
                        .WithMany()
                        .HasForeignKey("DigitalDecoderId");

                    b.Navigation("DigitalDecoder");
                });

            modelBuilder.Entity("DataLayer.Models.Product", b =>
                {
                    b.Navigation("StockStatus");
                });
#pragma warning restore 612, 618
        }
    }
}
