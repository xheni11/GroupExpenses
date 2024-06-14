using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupExpenses.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CreateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receipt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    PriceInEur = table.Column<double>(type: "float", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    PaidById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipt_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDept = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    ReceiptId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Receipt_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipt",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    ParticipantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEvent_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEvent_User_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_EventId",
                table: "Receipt",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_PaidById",
                table: "Receipt",
                column: "PaidById");

            migrationBuilder.CreateIndex(
                name: "IX_User_EventId",
                table: "User",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ReceiptId",
                table: "User",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEvent_EventId",
                table: "UserEvent",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEvent_ParticipantId",
                table: "UserEvent",
                column: "ParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_User_PaidById",
                table: "Receipt",
                column: "PaidById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_Event_EventId",
                table: "Receipt");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Event_EventId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_User_PaidById",
                table: "Receipt");

            migrationBuilder.DropTable(
                name: "UserEvent");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Receipt");
        }
    }
}
