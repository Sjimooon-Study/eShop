﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLayer.Migrations
{
    [DbContext(typeof(EShopContext))]
    [Migration("20211021202254_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataLayer.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressId");

                    b.HasIndex("CountryId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("DataLayer.Models.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryId");

                    b.ToTable("Country");

                    b.HasData(
                        new
                        {
                            CountryId = 1,
                            Name = "Denmark"
                        },
                        new
                        {
                            CountryId = 2,
                            Name = "Germany"
                        },
                        new
                        {
                            CountryId = 3,
                            Name = "Switzerland"
                        });
                });

            modelBuilder.Entity("DataLayer.Models.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageId");

                    b.ToTable("Image");

                    b.HasData(
                        new
                        {
                            ImageId = 1,
                            Path = "2021_10_20_b51acd9b-05bc-415d-86ac-c3c2e17b0012.jpeg"
                        },
                        new
                        {
                            ImageId = 2,
                            Path = "2021_10_20_c74b3599-6852-459a-9ee7-1b9129dcf3e0.jpeg"
                        },
                        new
                        {
                            ImageId = 3,
                            Path = "2021_10_20_06e1d3fa-8751-4c0e-9fac-b830384b0051.jpeg"
                        },
                        new
                        {
                            ImageId = 4,
                            Path = "2021_10_20_8aa35a49-50cd-4950-8689-38d547c35c91.jpeg"
                        },
                        new
                        {
                            ImageId = 5,
                            Path = "2021_10_20_a6c0169e-1d21-47ce-acef-b0ac88de5990.jpeg"
                        },
                        new
                        {
                            ImageId = 6,
                            Path = "2021_10_20_de2a7fc8-702f-4965-893e-816a22a4f403.jpeg"
                        },
                        new
                        {
                            ImageId = 7,
                            Path = "2021_10_20_eb718ad5-202f-4fe7-9f36-772da6d3b9a4.jpeg"
                        },
                        new
                        {
                            ImageId = 8,
                            Path = "2021_10_20_fa6c395b-d19f-44bc-9afc-d61d0905fe81.jpeg"
                        });
                });

            modelBuilder.Entity("DataLayer.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AmountInStock")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(8,2)");

                    b.Property<string>("TagId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ProductId");

                    b.HasIndex("TagId");

                    b.ToTable("Products");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Product");
                });

            modelBuilder.Entity("DataLayer.Models.RailwayCompany", b =>
                {
                    b.Property<int>("RailwayCompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RailwayCompanyId");

                    b.HasIndex("CountryId");

                    b.ToTable("RailwayCompany");

                    b.HasData(
                        new
                        {
                            RailwayCompanyId = 1,
                            CountryId = 1,
                            Name = "DSB"
                        },
                        new
                        {
                            RailwayCompanyId = 2,
                            CountryId = 2,
                            Name = "DB"
                        },
                        new
                        {
                            RailwayCompanyId = 3,
                            CountryId = 2,
                            Name = "DR"
                        },
                        new
                        {
                            RailwayCompanyId = 4,
                            CountryId = 3,
                            Name = "SBB"
                        });
                });

            modelBuilder.Entity("DataLayer.Models.Tag", b =>
                {
                    b.Property<string>("TagId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TagId");

                    b.ToTable("Tag");

                    b.HasData(
                        new
                        {
                            TagId = "Sale"
                        },
                        new
                        {
                            TagId = "New"
                        });
                });

            modelBuilder.Entity("DataLayer.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("AddressId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            IsAdmin = true,
                            Password = "admin",
                            UserName = "admin"
                        });
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

                    b.HasData(
                        new
                        {
                            ImagesImageId = 2,
                            ProductsProductId = 2
                        },
                        new
                        {
                            ImagesImageId = 1,
                            ProductsProductId = 3
                        },
                        new
                        {
                            ImagesImageId = 3,
                            ProductsProductId = 4
                        },
                        new
                        {
                            ImagesImageId = 4,
                            ProductsProductId = 4
                        },
                        new
                        {
                            ImagesImageId = 5,
                            ProductsProductId = 4
                        },
                        new
                        {
                            ImagesImageId = 6,
                            ProductsProductId = 4
                        },
                        new
                        {
                            ImagesImageId = 7,
                            ProductsProductId = 4
                        });
                });

            modelBuilder.Entity("DataLayer.Models.Products.DigitalDecoder", b =>
                {
                    b.HasBaseType("DataLayer.Models.Product");

                    b.Property<int>("Interface")
                        .HasColumnType("int");

                    b.Property<bool>("Sound")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("DigitalDecoder");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            AmountInStock = 23L,
                            Description = "Suitable for Gauge H0. The decoder is equipped with the RailCom® function. Maximum motor current: 1.2 A.",
                            Name = "PluX22 sound decoder (NEM 658)",
                            Price = 92.4m,
                            Interface = 0,
                            Sound = true
                        });
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

                    b.Property<int?>("RailwayCompanyId")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("int");

                    b.Property<int>("Scale")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("int");

                    b.HasIndex("DigitalDecoderId");

                    b.HasIndex("RailwayCompanyId");

                    b.HasDiscriminator().HasValue("Locomotive");

                    b.HasData(
                        new
                        {
                            ProductId = 2,
                            AmountInStock = 5L,
                            Description = "The 023 series was a true all-round genius. The locomotive hauled commuter trains, fast and express trains. Sometimes they hauled even freight trains. The newly designed locomotive of the class 023 (which until 1968 was designated class 23) was being used even in the epoch IV. On Dec. 31 1971, 76 locomotives were a permanent part of the rolling stock of the DB and without exception they were stationed at the three railway depots Saarbrücken, Kaiserslautern and Crailsheim.",
                            Name = "BR 023 040-9",
                            Price = 229.9m,
                            TagId = "New",
                            AutoCoupling = false,
                            Control = 2,
                            Epoch = 3,
                            Length = 24.5f,
                            LocoType = 0,
                            NumOfAxels = 9,
                            NumOfDrivenAxels = 4,
                            RailwayCompanyId = 2,
                            Scale = 0
                        },
                        new
                        {
                            ProductId = 3,
                            AmountInStock = 2L,
                            Description = "In 1992, the first locomotive Re 460 of the Swiss Federal Railways rolled out of the factory halls of the companies SLM and BBC in Oerlikon, Switzerland. The locomotive became known to the public as \"Lok 2000\". It stands for fast and modern passenger transport in Switzerland. An eye-catching and particularly aerodynamic design with a large front window, roof cladding and beads on the side wall make the class 460 visually an unbeatable rail vehicle.",
                            Name = "Re 460 068-0",
                            Price = 321.9m,
                            AutoCoupling = false,
                            Control = 3,
                            DigitalDecoderId = 1,
                            Epoch = 5,
                            Length = 21.2f,
                            LocoType = 2,
                            NumOfAxels = 4,
                            NumOfDrivenAxels = 4,
                            RailwayCompanyId = 4,
                            Scale = 0
                        },
                        new
                        {
                            ProductId = 4,
                            AmountInStock = 1L,
                            Description = "In the period between 1942 to 1950, over 7000 units of the class 52 war locomotive were built. These were constructed with as little effort as possible and savings were also made on the material wherever possible. With a weight of 84 tons, the loco achieved an output of 1,192 kW and a top speed of 80 km / h. The Deutsche Bundesbahn mainly got rid of the locomotives as early as 1953 - since it had sufficient machines of the series 50 and series 44 to haul the heavy goods trains. Only a few locomotives built in 1945 remained with the DB until 1962.",
                            Name = "BR 52",
                            Price = 319.9m,
                            TagId = "New",
                            AutoCoupling = false,
                            Control = 2,
                            Epoch = 2,
                            Length = 26.5f,
                            LocoType = 0,
                            NumOfAxels = 10,
                            NumOfDrivenAxels = 4,
                            RailwayCompanyId = 2,
                            Scale = 0
                        });
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

                    b.Property<int?>("RailwayCompanyId")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("int");

                    b.Property<int>("Scale")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("int");

                    b.HasIndex("RailwayCompanyId");

                    b.HasDiscriminator().HasValue("RailCar");
                });

            modelBuilder.Entity("DataLayer.Models.Address", b =>
                {
                    b.HasOne("DataLayer.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("DataLayer.Models.Product", b =>
                {
                    b.HasOne("DataLayer.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("DataLayer.Models.RailwayCompany", b =>
                {
                    b.HasOne("DataLayer.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("DataLayer.Models.User", b =>
                {
                    b.HasOne("DataLayer.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.Navigation("Address");
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

                    b.HasOne("DataLayer.Models.RailwayCompany", "RailwayCompany")
                        .WithMany()
                        .HasForeignKey("RailwayCompanyId");

                    b.Navigation("DigitalDecoder");

                    b.Navigation("RailwayCompany");
                });

            modelBuilder.Entity("DataLayer.Models.Products.RailCar", b =>
                {
                    b.HasOne("DataLayer.Models.RailwayCompany", "RailwayCompany")
                        .WithMany()
                        .HasForeignKey("RailwayCompanyId");

                    b.Navigation("RailwayCompany");
                });
#pragma warning restore 612, 618
        }
    }
}
