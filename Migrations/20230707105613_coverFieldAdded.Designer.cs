﻿// <auto-generated />
using System;
using AzBook.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AzBook.Migrations
{
    [DbContext(typeof(BookContext))]
    [Migration("20230707105613_coverFieldAdded")]
    partial class coverFieldAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AzBook.Model.Book", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookCoverUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = "21466801-9deb-4fb0-98e5-ff5536ce1609",
                            Author = "Author 1",
                            CreatedAt = new DateTime(2023, 7, 7, 15, 56, 12, 764, DateTimeKind.Local).AddTicks(7407),
                            CreatedBy = "Seeder",
                            Description = "Description 1",
                            Quantity = 1,
                            Title = "Book 1"
                        },
                        new
                        {
                            Id = "47650f11-b4ed-4eda-9c51-e102d42976dc",
                            Author = "Author 2",
                            CreatedAt = new DateTime(2023, 7, 7, 15, 56, 12, 764, DateTimeKind.Local).AddTicks(7411),
                            CreatedBy = "Seeder",
                            Description = "Description 2",
                            Quantity = 1,
                            Title = "Book 2"
                        });
                });

            modelBuilder.Entity("AzBook.Model.Order", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BookId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BookName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Returned")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ReturnedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("AzBook.Model.Order", b =>
                {
                    b.HasOne("AzBook.Model.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });
#pragma warning restore 612, 618
        }
    }
}