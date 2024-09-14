using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pets.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class OwnerProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Pets",
                newName: "LastModifiedDate");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerEmail",
                table: "Pets",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "Pets",
                type: "TEXT",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "OwnerEmail",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "Pets");

            migrationBuilder.RenameColumn(
                name: "LastModifiedDate",
                table: "Pets",
                newName: "UpdatedDate");
        }
    }
}
