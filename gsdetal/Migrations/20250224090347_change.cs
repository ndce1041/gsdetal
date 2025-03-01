using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gsdetal.Migrations
{
    /// <inheritdoc />
    public partial class change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "templatetype",
                table: "Itemdetails");

            migrationBuilder.AddColumn<string>(
                name: "templatetype",
                table: "Urls",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "templatetype",
                table: "Urls");

            migrationBuilder.AddColumn<string>(
                name: "templatetype",
                table: "Itemdetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
