using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class BaseFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertUserId",
                schema: "dbo",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                schema: "dbo",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                schema: "dbo",
                table: "ExpenseCategory");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                schema: "dbo",
                table: "ExpenseCategory");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                schema: "dbo",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                schema: "dbo",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                schema: "dbo",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                schema: "dbo",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                schema: "dbo",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                schema: "dbo",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                schema: "dbo",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                schema: "dbo",
                table: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                schema: "dbo",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserId",
                schema: "dbo",
                table: "Payment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                schema: "dbo",
                table: "ExpenseCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserId",
                schema: "dbo",
                table: "ExpenseCategory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                schema: "dbo",
                table: "Expense",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserId",
                schema: "dbo",
                table: "Expense",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                schema: "dbo",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserId",
                schema: "dbo",
                table: "Employee",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                schema: "dbo",
                table: "ApplicationUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserId",
                schema: "dbo",
                table: "ApplicationUser",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                schema: "dbo",
                table: "Address",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserId",
                schema: "dbo",
                table: "Address",
                type: "int",
                nullable: true);
        }
    }
}
