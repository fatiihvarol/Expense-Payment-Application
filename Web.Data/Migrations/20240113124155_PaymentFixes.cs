using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class PaymentFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDate",
                schema: "dbo",
                table: "Payment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                schema: "dbo",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "Payment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                schema: "dbo",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverIban",
                schema: "dbo",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                schema: "dbo",
                table: "Payment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserId",
                schema: "dbo",
                table: "Payment",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertDate",
                schema: "dbo",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                schema: "dbo",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                schema: "dbo",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "ReceiverIban",
                schema: "dbo",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                schema: "dbo",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                schema: "dbo",
                table: "Payment");
        }
    }
}
