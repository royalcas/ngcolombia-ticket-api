using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NGColombia.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfirmationDateLimit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionalInformation = table.Column<string>(type: "text", nullable: true),
                    AuthorizationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstallmentsNumber = table.Column<int>(type: "int", nullable: false),
                    LapPaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LapPaymentMethodType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LapResponseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentProviderReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentProviderTransactionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PolPaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PolPaymentMethodType = table.Column<int>(type: "int", nullable: false),
                    PolResponseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProviderMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Taxes = table.Column<double>(type: "float", nullable: false),
                    TransactionState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrazabilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketTypes",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: false),
                    eventId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTypes", x => x.Code);
                    table.ForeignKey(
                        name: "FK_TicketTypes_Events_eventId",
                        column: x => x.eventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentInformationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_PaymentInformation_PaymentInformationId",
                        column: x => x.PaymentInformationId,
                        principalTable: "PaymentInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfirmationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalValue = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTicketDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TicketTypeCode = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTicketDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionTicketDetails_TicketTypes_TicketTypeCode",
                        column: x => x.TicketTypeCode,
                        principalTable: "TicketTypes",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionTicketDetails_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentInformationId",
                table: "Invoices",
                column: "PaymentInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTypes_eventId",
                table: "TicketTypes",
                column: "eventId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerId",
                table: "Transactions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_InvoiceId",
                table: "Transactions",
                column: "InvoiceId",
                unique: true,
                filter: "[InvoiceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTicketDetails_TicketTypeCode",
                table: "TransactionTicketDetails",
                column: "TicketTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTicketDetails_TransactionId",
                table: "TransactionTicketDetails",
                column: "TransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionTicketDetails");

            migrationBuilder.DropTable(
                name: "TicketTypes");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "PaymentInformation");
        }
    }
}
