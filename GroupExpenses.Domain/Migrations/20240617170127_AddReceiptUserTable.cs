using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupExpenses.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddReceiptUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDept",
                table: "User");


            migrationBuilder.DropIndex(
                   name: "IX_Receipt_PaidById",
                   table: "Receipt");

         migrationBuilder.DropCheckConstraint(
             name: "FK_Receipt_User_PaidById",
             table: "Receipt");

         migrationBuilder.DropColumn(
                      name: "PaidById",
                      table: "Receipt");


         

         migrationBuilder.DropColumn(
                name: "TotalPaid",
                table: "User");

         migrationBuilder.AlterColumn<decimal>(
                name: "PriceInEur",
                table: "Receipt",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Receipt",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

         migrationBuilder.CreateTable(
                name: "ReceiptUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaidById = table.Column<int>(type: "int", nullable: false),
                    PaidForId = table.Column<int>(type: "int", nullable: false),
                    ReceiptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptUser_Receipt_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptUser_User_PaidById",
                        column: x => x.PaidById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptUser_User_PaidForId",
                        column: x => x.PaidForId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptUser_PaidById",
                table: "ReceiptUser",
                column: "PaidById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptUser_PaidForId",
                table: "ReceiptUser",
                column: "PaidForId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptUser_ReceiptId",
                table: "ReceiptUser",
                column: "ReceiptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptUser");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDept",
                table: "User",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPaid",
                table: "User",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<double>(
                name: "PriceInEur",
                table: "Receipt",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Receipt",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
