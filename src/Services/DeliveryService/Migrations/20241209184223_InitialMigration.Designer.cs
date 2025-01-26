﻿// <auto-generated />
using System;
using DeliveryService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DeliveryService.Migrations
{
    [DbContext(typeof(DeliveryDBContext))]
    [Migration("20241209184223_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DeliveryService.Domain.Domain.Entities.Delivery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Courer_Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTimeestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Delivery_Time")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<string>("ShippingAddress")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.Property<int>("TotalQuantity")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Deliveries");
                });
#pragma warning restore 612, 618
        }
    }
}