﻿// <auto-generated />
using System;
using MedFlow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MedFlow.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230822215354_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MedFlow.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("added_by")
                        .HasColumnType("int");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("birth_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("contact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("added_by");

                    b.ToTable("patients");
                });

            modelBuilder.Entity("MedFlow.Models.Payments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("billfilepath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("prescription_id")
                        .HasColumnType("int");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("prescription_id")
                        .IsUnique();

                    b.ToTable("payment");
                });

            modelBuilder.Entity("MedFlow.Models.Prescriptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("filepath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("patient_id")
                        .HasColumnType("int");

                    b.Property<int>("payment_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("patient_id");

                    b.ToTable("prescriptions");
                });

            modelBuilder.Entity("MedFlow.Models.medStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("stocks")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("medStocks");
                });

            modelBuilder.Entity("MedFlow.Models.paidState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("paid_count")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("paidState");
                });

            modelBuilder.Entity("MedFlow.Models.userDetails", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("user_type")
                        .HasColumnType("int");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("user_type");

                    b.ToTable("userdetails");
                });

            modelBuilder.Entity("MedFlow.Models.userType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("usertypes");
                });

            modelBuilder.Entity("MedFlow.Models.Patient", b =>
                {
                    b.HasOne("MedFlow.Models.userDetails", "userDetails")
                        .WithMany("patients")
                        .HasForeignKey("added_by")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("userDetails");
                });

            modelBuilder.Entity("MedFlow.Models.Payments", b =>
                {
                    b.HasOne("MedFlow.Models.Prescriptions", "prescriptions")
                        .WithOne("payments")
                        .HasForeignKey("MedFlow.Models.Payments", "prescription_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("prescriptions");
                });

            modelBuilder.Entity("MedFlow.Models.Prescriptions", b =>
                {
                    b.HasOne("MedFlow.Models.Patient", "patient")
                        .WithMany("prescriptions")
                        .HasForeignKey("patient_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("patient");
                });

            modelBuilder.Entity("MedFlow.Models.userDetails", b =>
                {
                    b.HasOne("MedFlow.Models.userType", "userType")
                        .WithMany("userDetails")
                        .HasForeignKey("user_type")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("userType");
                });

            modelBuilder.Entity("MedFlow.Models.Patient", b =>
                {
                    b.Navigation("prescriptions");
                });

            modelBuilder.Entity("MedFlow.Models.Prescriptions", b =>
                {
                    b.Navigation("payments")
                        .IsRequired();
                });

            modelBuilder.Entity("MedFlow.Models.userDetails", b =>
                {
                    b.Navigation("patients");
                });

            modelBuilder.Entity("MedFlow.Models.userType", b =>
                {
                    b.Navigation("userDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
