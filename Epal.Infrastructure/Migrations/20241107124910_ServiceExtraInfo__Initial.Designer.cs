﻿// <auto-generated />
using System;
using Epal.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Epal.Infrastructure.Migrations
{
    [DbContext(typeof(EpalDbContext))]
    [Migration("20241107124910_ServiceExtraInfo__Initial")]
    partial class ServiceExtraInfo__Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Epal.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Epal.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("SellerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ServiceOptionId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<double>("Total")
                        .HasColumnType("double precision");

                    b.Property<DateTimeOffset?>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("SellerId");

                    b.HasIndex("ServiceOptionId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Epal.Domain.Entities.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric");

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("EpalStatusAcquiring")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Languages")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfileType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProfileType");

                    b.HasIndex("Username");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Epal.Domain.Entities.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Avatar")
                        .HasColumnType("text");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Icon")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uuid");

                    b.Property<string>("Tags")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Epal.Domain.Entities.ServiceExtraInfo", b =>
                {
                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.Property<string>("Photo")
                        .HasColumnType("text");

                    b.Property<string>("Platforms")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Positions")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Rank")
                        .HasColumnType("text");

                    b.Property<string>("Servers")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Styles")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ServiceId");

                    b.ToTable("ServiceExtraInfos");
                });

            modelBuilder.Entity("Epal.Domain.Entities.ServiceOption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServiceOptions");
                });

            modelBuilder.Entity("Epal.Domain.Entities.Order", b =>
                {
                    b.HasOne("Epal.Domain.Entities.Profile", "Buyer")
                        .WithMany("BoughtOrders")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Epal.Domain.Entities.Profile", "Seller")
                        .WithMany("SoldOrders")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Epal.Domain.Entities.ServiceOption", "ServiceOption")
                        .WithMany()
                        .HasForeignKey("ServiceOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buyer");

                    b.Navigation("Seller");

                    b.Navigation("ServiceOption");
                });

            modelBuilder.Entity("Epal.Domain.Entities.Service", b =>
                {
                    b.HasOne("Epal.Domain.Entities.Category", "Category")
                        .WithMany("Services")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Epal.Domain.Entities.Profile", "Profile")
                        .WithMany("Services")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Epal.Domain.Entities.ServiceExtraInfo", b =>
                {
                    b.HasOne("Epal.Domain.Entities.Service", "Service")
                        .WithOne("ServiceExtraInfo")
                        .HasForeignKey("Epal.Domain.Entities.ServiceExtraInfo", "ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");
                });

            modelBuilder.Entity("Epal.Domain.Entities.ServiceOption", b =>
                {
                    b.HasOne("Epal.Domain.Entities.Service", "Service")
                        .WithMany("ServiceOptions")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");
                });

            modelBuilder.Entity("Epal.Domain.Entities.Category", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("Epal.Domain.Entities.Profile", b =>
                {
                    b.Navigation("BoughtOrders");

                    b.Navigation("Services");

                    b.Navigation("SoldOrders");
                });

            modelBuilder.Entity("Epal.Domain.Entities.Service", b =>
                {
                    b.Navigation("ServiceExtraInfo")
                        .IsRequired();

                    b.Navigation("ServiceOptions");
                });
#pragma warning restore 612, 618
        }
    }
}