using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NGColombia.Api.Migrations
{
    public partial class TicketQuantityByTypeAndPaymentConfirmationLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Invoices_InvoiceId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "PaymentInformation");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_InvoiceId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Transactions");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TicketQuantity",
                table: "TicketTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PaymentConfirmationLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RawData = table.Column<string>(type: "text", nullable: true),
                    ReferencePayU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseCodePol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseMessagePol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatePol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentConfirmationLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentConfirmationLogs_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentResponseLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LapResponseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LapTransationState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PolResponseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PolTransactionState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RawData = table.Column<string>(type: "text", nullable: true),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionState = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentResponseLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentResponseLogs_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentConfirmationLogs_TransactionId",
                table: "PaymentConfirmationLogs",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentResponseLogs_TransactionId",
                table: "PaymentResponseLogs",
                column: "TransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentConfirmationLogs");

            migrationBuilder.DropTable(
                name: "PaymentResponseLogs");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TicketQuantity",
                table: "TicketTypes");

            migrationBuilder.AddColumn<Guid>(
                name: "InvoiceId",
                table: "Transactions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AdditionalInformation = table.Column<string>(type: "text", nullable: true),
                    AuthorizationCode = table.Column<string>(nullable: true),
                    Bank = table.Column<string>(nullable: true),
                    BuyerEmail = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    Cus = table.Column<string>(nullable: true),
                    InstallmentsNumber = table.Column<int>(nullable: false),
                    LapPaymentMethod = table.Column<string>(nullable: true),
                    LapPaymentMethodType = table.Column<string>(nullable: true),
                    LapResponseCode = table.Column<string>(nullable: true),
                    PaymentProviderReference = table.Column<string>(nullable: true),
                    PaymentProviderTransactionId = table.Column<string>(nullable: true),
                    PolPaymentMethod = table.Column<string>(nullable: true),
                    PolPaymentMethodType = table.Column<int>(nullable: false),
                    PolResponseCode = table.Column<string>(nullable: true),
                    ProcessingDate = table.Column<DateTime>(nullable: false),
                    ProviderMessage = table.Column<string>(nullable: true),
                    Taxes = table.Column<double>(nullable: false),
                    TransactionState = table.Column<string>(nullable: true),
                    TrazabilityCode = table.Column<string>(nullable: true),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PaymentInformationId = table.Column<Guid>(nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_InvoiceId",
                table: "Transactions",
                column: "InvoiceId",
                unique: true,
                filter: "[InvoiceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentInformationId",
                table: "Invoices",
                column: "PaymentInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Invoices_InvoiceId",
                table: "Transactions",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
