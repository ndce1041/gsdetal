using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gsdetal.Migrations
{
    /// <inheritdoc />
    public partial class _4change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Thumbnails",
                columns: table => new
                {
                    thumbnailurl = table.Column<string>(type: "TEXT", nullable: false),
                    thumbnailpath = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thumbnails", x => x.thumbnailurl);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Thumbnails");
        }
    }
}
