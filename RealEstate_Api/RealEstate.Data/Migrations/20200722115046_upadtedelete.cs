using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstate.Data.Migrations
{
    public partial class upadtedelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "Properties",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 22, 18, 50, 44, 989, DateTimeKind.Local).AddTicks(3467),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 22, 9, 29, 10, 858, DateTimeKind.Local).AddTicks(212));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Properties",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 22, 18, 50, 44, 989, DateTimeKind.Local).AddTicks(2163),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 22, 9, 29, 10, 857, DateTimeKind.Local).AddTicks(8183));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Properties",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 21, 18, 50, 44, 989, DateTimeKind.Local).AddTicks(2756),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 8, 21, 9, 29, 10, 857, DateTimeKind.Local).AddTicks(9277));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Properties",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateSubmitted",
                table: "News",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 22, 18, 50, 44, 974, DateTimeKind.Local).AddTicks(2801),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 22, 9, 29, 10, 841, DateTimeKind.Local).AddTicks(8664));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Properties");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "Properties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 22, 9, 29, 10, 858, DateTimeKind.Local).AddTicks(212),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 22, 18, 50, 44, 989, DateTimeKind.Local).AddTicks(3467));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Properties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 22, 9, 29, 10, 857, DateTimeKind.Local).AddTicks(8183),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 22, 18, 50, 44, 989, DateTimeKind.Local).AddTicks(2163));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Properties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 21, 9, 29, 10, 857, DateTimeKind.Local).AddTicks(9277),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 21, 18, 50, 44, 989, DateTimeKind.Local).AddTicks(2756));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateSubmitted",
                table: "News",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 22, 9, 29, 10, 841, DateTimeKind.Local).AddTicks(8664),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 22, 18, 50, 44, 974, DateTimeKind.Local).AddTicks(2801));
        }
    }
}
