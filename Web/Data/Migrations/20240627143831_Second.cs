using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purchasedtickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketForPurchases",
                table: "TicketForPurchases");

            migrationBuilder.RenameTable(
                name: "TicketForPurchases",
                newName: "Ticket");

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Schedules",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Ticket",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleId",
                table: "Ticket",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_EventId",
                table: "Schedules",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ScheduleId",
                table: "Ticket",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Events_EventId",
                table: "Schedules",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Schedules_ScheduleId",
                table: "Ticket",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Events_EventId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Schedules_ScheduleId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_EventId",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_ScheduleId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Ticket");

            migrationBuilder.RenameTable(
                name: "Ticket",
                newName: "TicketForPurchases");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketForPurchases",
                table: "TicketForPurchases",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Purchasedtickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discount = table.Column<float>(type: "real", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchasedtickets", x => x.Id);
                });
        }
    }
}
