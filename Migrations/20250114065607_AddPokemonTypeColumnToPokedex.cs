using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanAPIPJ.Migrations
{
    /// <inheritdoc />
    public partial class AddPokemonTypeColumnToPokedex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pokedex",
                columns: table => new
                {
                    PokemonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PokemonType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PokemonDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokedex", x => x.PokemonID);
                });

            migrationBuilder.CreateTable(
                name: "PokemonTypes",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonTypes", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "PokemonTypeRelations",
                columns: table => new
                {
                    PokemonID = table.Column<int>(type: "int", nullable: false),
                    TypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonTypeRelations", x => new { x.PokemonID, x.TypeID });
                    table.ForeignKey(
                        name: "FK_PokemonTypeRelations_Pokedex_PokemonID",
                        column: x => x.PokemonID,
                        principalTable: "Pokedex",
                        principalColumn: "PokemonID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonTypeRelations_PokemonTypes_TypeID",
                        column: x => x.TypeID,
                        principalTable: "PokemonTypes",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTypeRelations_TypeID",
                table: "PokemonTypeRelations",
                column: "TypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonTypeRelations");

            migrationBuilder.DropTable(
                name: "Pokedex");

            migrationBuilder.DropTable(
                name: "PokemonTypes");
        }
    }
}
