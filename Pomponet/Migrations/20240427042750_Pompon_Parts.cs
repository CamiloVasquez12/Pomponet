using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pomponet.Migrations
{
    /// <inheritdoc />
    public partial class Pompon_Parts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Pompon_Parts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Pompon_Parts");
        }
    }
}
