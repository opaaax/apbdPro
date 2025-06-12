using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBDProjekt.Migrations
{
    /// <inheritdoc />
    public partial class InitClients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Firma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Krs = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Firma_Client_Id",
                        column: x => x.Id,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OsobaFizyczna",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Imie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsobaFizyczna", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OsobaFizyczna_Client_Id",
                        column: x => x.Id,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Firma");

            migrationBuilder.DropTable(
                name: "OsobaFizyczna");

            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
