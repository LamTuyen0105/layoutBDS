using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstate.Data.Migrations
{
    public partial class updateuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "Properties",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 22, 9, 29, 10, 858, DateTimeKind.Local).AddTicks(212),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 14, 22, 7, 8, 758, DateTimeKind.Local).AddTicks(8877));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Properties",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 22, 9, 29, 10, 857, DateTimeKind.Local).AddTicks(8183),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 14, 22, 7, 8, 758, DateTimeKind.Local).AddTicks(6217));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Properties",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 21, 9, 29, 10, 857, DateTimeKind.Local).AddTicks(9277),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 8, 13, 22, 7, 8, 758, DateTimeKind.Local).AddTicks(7374));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateSubmitted",
                table: "News",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 22, 9, 29, 10, 841, DateTimeKind.Local).AddTicks(8664),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 14, 22, 7, 8, 729, DateTimeKind.Local).AddTicks(6101));

            migrationBuilder.AddColumn<string>(
                name: "Provider",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialId",
                table: "AppUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Provider",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "SocialId",
                table: "AppUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "Properties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 14, 22, 7, 8, 758, DateTimeKind.Local).AddTicks(8877),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 22, 9, 29, 10, 858, DateTimeKind.Local).AddTicks(212));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Properties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 14, 22, 7, 8, 758, DateTimeKind.Local).AddTicks(6217),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 22, 9, 29, 10, 857, DateTimeKind.Local).AddTicks(8183));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Properties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 13, 22, 7, 8, 758, DateTimeKind.Local).AddTicks(7374),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 21, 9, 29, 10, 857, DateTimeKind.Local).AddTicks(9277));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateSubmitted",
                table: "News",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 14, 22, 7, 8, 729, DateTimeKind.Local).AddTicks(6101),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 22, 9, 29, 10, 841, DateTimeKind.Local).AddTicks(8664));
        }
    }
}
