using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstate.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Properties",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 14, 22, 7, 8, 758, DateTimeKind.Local).AddTicks(6217),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 8, 9, 20, 38, 23, 728, DateTimeKind.Local).AddTicks(256));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Properties",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 13, 22, 7, 8, 758, DateTimeKind.Local).AddTicks(7374),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Properties",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 14, 22, 7, 8, 758, DateTimeKind.Local).AddTicks(8877));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateSubmitted",
                table: "News",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 14, 22, 7, 8, 729, DateTimeKind.Local).AddTicks(6101),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 10, 20, 38, 23, 689, DateTimeKind.Local).AddTicks(2589));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Properties");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Properties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 9, 20, 38, 23, 728, DateTimeKind.Local).AddTicks(256),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 14, 22, 7, 8, 758, DateTimeKind.Local).AddTicks(6217));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Properties",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 13, 22, 7, 8, 758, DateTimeKind.Local).AddTicks(7374));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateSubmitted",
                table: "News",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 10, 20, 38, 23, 689, DateTimeKind.Local).AddTicks(2589),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 14, 22, 7, 8, 729, DateTimeKind.Local).AddTicks(6101));
        }
    }
}
