using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pomponet.Migrations
{
    /// <inheritdoc />
    public partial class Pest_X_Fungicide : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Pest_X_Fungicide",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Pest_X_Fungicide");
        }
    }
}
