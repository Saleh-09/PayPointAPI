using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PayPointAPi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmplyees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "EmployeeEmail", "EmployeeName", "EmployeePhone", "EmployeeStore", "StoreId" },
                values: new object[,]
                {
                    { 1, "ali@example.com", "Ali Khan", 3001234567.0, 0.0, 1 },
                    { 2, "sara@example.com", "Sara Ahmed", 3129876543.0, 0.0, 2 },
                    { 3, "bilal@example.com", "Bilal Sheikh", 3331234567.0, 0.0, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_StoreId",
                table: "Employees",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Stores_StoreId",
                table: "Employees",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Stores_StoreId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_StoreId",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Employees");
        }
    }
}
