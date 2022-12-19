using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PetShop.Migrations
{
    public partial class ReNamePetModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GioiTinh",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "GiongLoai",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "ThoiGian",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "ThuCung",
                table: "Pet");

            migrationBuilder.RenameColumn(
                name: "Gia",
                table: "Pet",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Pet",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Pet",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Pet",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PetName",
                table: "Pet",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PictureURL",
                table: "Pet",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "PetName",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "PictureURL",
                table: "Pet");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Pet",
                newName: "Gia");

            migrationBuilder.AddColumn<string>(
                name: "GioiTinh",
                table: "Pet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GiongLoai",
                table: "Pet",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Pet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ThoiGian",
                table: "Pet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ThuCung",
                table: "Pet",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
