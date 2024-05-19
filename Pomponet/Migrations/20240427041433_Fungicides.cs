using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pomponet.Migrations
{
    /// <inheritdoc />
    public partial class Fungicides : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Fungicides",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Fungicides");
        }
    }
}
