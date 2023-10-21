using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthServer.Infrastructure.Persistence.Migrations.AspNetIdentity
{
    public partial class user_creator_modifire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                schema: "Identity",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                schema: "Identity",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyDate",
                schema: "Identity",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                schema: "Identity",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Identity",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                schema: "Identity",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Identity",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyDate",
                schema: "Identity",
                table: "Roles",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifyDate",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                schema: "Identity",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Identity",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                schema: "Identity",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Identity",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ModifyDate",
                schema: "Identity",
                table: "Roles");
        }
    }
}
