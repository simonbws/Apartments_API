// <auto-generated />
using System;
using Apartment_API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Apartment_API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Apartment_API.Models.ApartmentNumber", b =>
                {
                    b.Property<int>("ApartmentNo")
                        .HasColumnType("int");

                    b.Property<int>("ApartmentID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SpecialProperties")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ApartmentNo");

                    b.HasIndex("ApartmentID");

                    b.ToTable("ApartmentNumbers");
                });

            modelBuilder.Entity("Apartment_API.Models.LocalUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LocalUsers");
                });

            modelBuilder.Entity("Apartments_API.Models.Apartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Amenity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Occupancy")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("SquareFeet")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Apartments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenity = "",
                            CreatedDate = new DateTime(2022, 10, 14, 19, 41, 31, 611, DateTimeKind.Local).AddTicks(3532),
                            Details = "Luxury treehouse apartment with lake view.",
                            ImagePath = "https://dotnetmastery.com/bluevillaimages/villa3.jpg",
                            Name = "Luxury Apartment",
                            Occupancy = 4,
                            Rate = 200.0,
                            SquareFeet = 750,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Amenity = "",
                            CreatedDate = new DateTime(2022, 10, 14, 19, 41, 31, 611, DateTimeKind.Local).AddTicks(3579),
                            Details = "Stylish apartment with pool amidst palm trees and with sea view.",
                            ImagePath = "https://dotnetmastery.com/bluevillaimages/villa1.jpg",
                            Name = "Premium Pool Apartment",
                            Occupancy = 4,
                            Rate = 300.0,
                            SquareFeet = 900,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Amenity = "",
                            CreatedDate = new DateTime(2022, 10, 14, 19, 41, 31, 611, DateTimeKind.Local).AddTicks(3581),
                            Details = "Luxury apartment with pool amidst palm trees and with sea view..",
                            ImagePath = "https://dotnetmastery.com/bluevillaimages/villa4.jpg",
                            Name = "Stylish Pool Apartment",
                            Occupancy = 4,
                            Rate = 400.0,
                            SquareFeet = 600,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            Amenity = "",
                            CreatedDate = new DateTime(2022, 10, 14, 19, 41, 31, 611, DateTimeKind.Local).AddTicks(3582),
                            Details = "Luxurious apartment in Mediterranean style, built on a cliff overlooking the sea.",
                            ImagePath = "https://dotnetmastery.com/bluevillaimages/villa5.jpg",
                            Name = "Diamond Apartment",
                            Occupancy = 4,
                            Rate = 550.0,
                            SquareFeet = 950,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            Amenity = "",
                            CreatedDate = new DateTime(2022, 10, 14, 19, 41, 31, 611, DateTimeKind.Local).AddTicks(3584),
                            Details = "Luxury apartment with views of hollywood.",
                            ImagePath = "https://dotnetmastery.com/bluevillaimages/villa2.jpg",
                            Name = "Diamond Pool Villa",
                            Occupancy = 4,
                            Rate = 600.0,
                            SquareFeet = 900,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Apartment_API.Models.ApartmentNumber", b =>
                {
                    b.HasOne("Apartments_API.Models.Apartment", "Apartment")
                        .WithMany()
                        .HasForeignKey("ApartmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Apartment");
                });
#pragma warning restore 612, 618
        }
    }
}
