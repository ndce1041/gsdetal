using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gsdetal.Migrations
{
    /// <inheritdoc />
    public partial class initcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Itemdetails",
                columns: table => new
                {
                    url = table.Column<string>(type: "TEXT", nullable: false),
                    templatetype = table.Column<string>(type: "TEXT", nullable: false),
                    color = table.Column<string>(type: "TEXT", nullable: false),
                    size = table.Column<string>(type: "TEXT", nullable: false),
                    state = table.Column<string>(type: "TEXT", nullable: false),
                    thumbnailurl = table.Column<string>(type: "TEXT", nullable: false),
                    thumbnailpath = table.Column<string>(type: "TEXT", nullable: false),
                    tip = table.Column<string>(type: "TEXT", nullable: false),
                    temp = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itemdetails", x => x.url);
                });

            migrationBuilder.CreateTable(
                name: "Urls",
                columns: table => new
                {
                    url = table.Column<string>(type: "TEXT", nullable: false),
                    order = table.Column<string>(type: "TEXT", nullable: false),
                    title = table.Column<string>(type: "TEXT", nullable: false),
                    type = table.Column<string>(type: "TEXT", nullable: false),
                    price = table.Column<string>(type: "TEXT", nullable: false),
                    status = table.Column<string>(type: "TEXT", nullable: false),
                    updatetime = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urls", x => x.url);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Itemdetails");

            migrationBuilder.DropTable(
                name: "Urls");
        }
    }
}
