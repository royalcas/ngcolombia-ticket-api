﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using NGColombia.Api.Persistence;
using System;

namespace NGColombia.Api.Migrations
{
    [DbContext(typeof(NGColombiaDbContext))]
    partial class NGColombiaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NGColombia.Api.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("IdentificationNumber");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("NGColombia.Api.Models.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ConfirmationDateLimit");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("NGColombia.Api.Models.PaymentConfirmationLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ProcessDate");

                    b.Property<string>("RawData")
                        .HasColumnType("text");

                    b.Property<string>("ReferencePayU");

                    b.Property<string>("ResponseCodePol");

                    b.Property<string>("ResponseMessagePol");

                    b.Property<string>("StatePol");

                    b.Property<Guid>("TransactionId");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.ToTable("PaymentConfirmationLogs");
                });

            modelBuilder.Entity("NGColombia.Api.Models.PaymentResponseLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LapResponseCode");

                    b.Property<string>("LapTransationState");

                    b.Property<string>("PolResponseCode");

                    b.Property<string>("PolTransactionState");

                    b.Property<DateTime>("ProcessDate");

                    b.Property<string>("RawData")
                        .HasColumnType("text");

                    b.Property<Guid>("TransactionId");

                    b.Property<string>("TransactionState");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.ToTable("PaymentResponseLogs");
                });

            modelBuilder.Entity("NGColombia.Api.Models.TicketType", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("TicketQuantity");

                    b.Property<double>("Value");

                    b.Property<Guid?>("eventId");

                    b.HasKey("Code");

                    b.HasIndex("eventId");

                    b.ToTable("TicketTypes");
                });

            modelBuilder.Entity("NGColombia.Api.Models.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Approved");

                    b.Property<bool>("Closed");

                    b.Property<DateTime>("ConfirmationDate");

                    b.Property<Guid?>("CustomerId");

                    b.Property<string>("Ip");

                    b.Property<bool>("IsTestTransaction");

                    b.Property<string>("Signature");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Status");

                    b.Property<double>("TotalValue");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("NGColombia.Api.Models.TransactionTicketDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Quantity");

                    b.Property<string>("TicketTypeCode");

                    b.Property<Guid?>("TransactionId");

                    b.HasKey("Id");

                    b.HasIndex("TicketTypeCode");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionTicketDetails");
                });

            modelBuilder.Entity("NGColombia.Api.Models.PaymentConfirmationLog", b =>
                {
                    b.HasOne("NGColombia.Api.Models.Transaction", "Transaction")
                        .WithMany("Confirmations")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NGColombia.Api.Models.PaymentResponseLog", b =>
                {
                    b.HasOne("NGColombia.Api.Models.Transaction", "Transaction")
                        .WithMany("Responses")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NGColombia.Api.Models.TicketType", b =>
                {
                    b.HasOne("NGColombia.Api.Models.Event", "event")
                        .WithMany("TicketTypes")
                        .HasForeignKey("eventId");
                });

            modelBuilder.Entity("NGColombia.Api.Models.Transaction", b =>
                {
                    b.HasOne("NGColombia.Api.Models.Customer", "Customer")
                        .WithMany("Transactions")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("NGColombia.Api.Models.TransactionTicketDetail", b =>
                {
                    b.HasOne("NGColombia.Api.Models.TicketType", "TicketType")
                        .WithMany("Details")
                        .HasForeignKey("TicketTypeCode");

                    b.HasOne("NGColombia.Api.Models.Transaction", "Transaction")
                        .WithMany("Details")
                        .HasForeignKey("TransactionId");
                });
#pragma warning restore 612, 618
        }
    }
}
