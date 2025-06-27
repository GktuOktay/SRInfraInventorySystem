using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SRInfraInventorySystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class delete_isactiveCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personnel_Departments_DepartmentId",
                table: "Personnel");

            migrationBuilder.DropColumn(
                name: "ActiveConnections",
                table: "ServerUsageHistories");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Personnel");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "ExitTime",
                table: "AccessLogs");

            migrationBuilder.RenameColumn(
                name: "RecordDate",
                table: "ServerUsageHistories",
                newName: "UsageDate");

            migrationBuilder.RenameColumn(
                name: "AccessTime",
                table: "AccessLogs",
                newName: "AccessDate");

            migrationBuilder.AddColumn<double>(
                name: "NetworkUsage",
                table: "ServerUsageHistories",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<Guid>(
                name: "DepartmentId",
                table: "Personnel",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Personnel_Departments_DepartmentId",
                table: "Personnel",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personnel_Departments_DepartmentId",
                table: "Personnel");

            migrationBuilder.DropColumn(
                name: "NetworkUsage",
                table: "ServerUsageHistories");

            migrationBuilder.RenameColumn(
                name: "UsageDate",
                table: "ServerUsageHistories",
                newName: "RecordDate");

            migrationBuilder.RenameColumn(
                name: "AccessDate",
                table: "AccessLogs",
                newName: "AccessTime");

            migrationBuilder.AddColumn<int>(
                name: "ActiveConnections",
                table: "ServerUsageHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "DepartmentId",
                table: "Personnel",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Personnel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Departments",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExitTime",
                table: "AccessLogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Personnel_Departments_DepartmentId",
                table: "Personnel",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
