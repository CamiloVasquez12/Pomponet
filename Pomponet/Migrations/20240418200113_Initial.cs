using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pomponet.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id_Person = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Names = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id_Person);
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Id_Sensor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sensor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id_Sensor);
                });

            migrationBuilder.CreateTable(
                name: "Types_Fungicides",
                columns: table => new
                {
                    Id_Type_Fungicide = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type_Fungicide = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types_Fungicides", x => x.Id_Type_Fungicide);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id_Player = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Id_Person = table.Column<int>(type: "int", nullable: false),
                    PeopleId_Person = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id_Player);
                    table.ForeignKey(
                        name: "FK_Players_People_PeopleId_Person",
                        column: x => x.PeopleId_Person,
                        principalTable: "People",
                        principalColumn: "Id_Person");
                });

            migrationBuilder.CreateTable(
                name: "Fungicides",
                columns: table => new
                {
                    Id_Fungicide = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Fungicide = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Id_TypeFungicide = table.Column<int>(type: "int", nullable: false),
                    Types_FungicidesId_Type_Fungicide = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fungicides", x => x.Id_Fungicide);
                    table.ForeignKey(
                        name: "FK_Fungicides_Types_Fungicides_Types_FungicidesId_Type_Fungicide",
                        column: x => x.Types_FungicidesId_Type_Fungicide,
                        principalTable: "Types_Fungicides",
                        principalColumn: "Id_Type_Fungicide");
                });

            migrationBuilder.CreateTable(
                name: "Crop",
                columns: table => new
                {
                    Id_Crop = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Crop_Number = table.Column<int>(type: "int", nullable: false),
                    Id_Player = table.Column<int>(type: "int", nullable: false),
                    PlayersId_Player = table.Column<int>(type: "int", nullable: true),
                    Id_Fungicide = table.Column<int>(type: "int", nullable: false),
                    FungicidesId_Fungicide = table.Column<int>(type: "int", nullable: true),
                    Id_Sensor = table.Column<int>(type: "int", nullable: false),
                    SensorsId_Sensor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crop", x => x.Id_Crop);
                    table.ForeignKey(
                        name: "FK_Crop_Fungicides_FungicidesId_Fungicide",
                        column: x => x.FungicidesId_Fungicide,
                        principalTable: "Fungicides",
                        principalColumn: "Id_Fungicide");
                    table.ForeignKey(
                        name: "FK_Crop_Players_PlayersId_Player",
                        column: x => x.PlayersId_Player,
                        principalTable: "Players",
                        principalColumn: "Id_Player");
                    table.ForeignKey(
                        name: "FK_Crop_Sensors_SensorsId_Sensor",
                        column: x => x.SensorsId_Sensor,
                        principalTable: "Sensors",
                        principalColumn: "Id_Sensor");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Crop_FungicidesId_Fungicide",
                table: "Crop",
                column: "FungicidesId_Fungicide");

            migrationBuilder.CreateIndex(
                name: "IX_Crop_PlayersId_Player",
                table: "Crop",
                column: "PlayersId_Player");

            migrationBuilder.CreateIndex(
                name: "IX_Crop_SensorsId_Sensor",
                table: "Crop",
                column: "SensorsId_Sensor");

            migrationBuilder.CreateIndex(
                name: "IX_Fungicides_Types_FungicidesId_Type_Fungicide",
                table: "Fungicides",
                column: "Types_FungicidesId_Type_Fungicide");

            migrationBuilder.CreateIndex(
                name: "IX_Players_PeopleId_Person",
                table: "Players",
                column: "PeopleId_Person");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Crop");

            migrationBuilder.DropTable(
                name: "Fungicides");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "Types_Fungicides");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
