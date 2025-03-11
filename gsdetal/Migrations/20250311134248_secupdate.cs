using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gsdetal.Migrations
{
    /// <inheritdoc />
    public partial class secupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Itemdetails",
                table: "Itemdetails");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "Urls",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "Urls",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "Urls",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "price",
                table: "Urls",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "order",
                table: "Urls",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Itemdetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Itemdetails",
                table: "Itemdetails",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Itemdetails_url_color_size",
                table: "Itemdetails",
                columns: new[] { "url", "color", "size" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Itemdetails",
                table: "Itemdetails");

            migrationBuilder.DropIndex(
                name: "IX_Itemdetails_url_color_size",
                table: "Itemdetails");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Itemdetails");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "Urls",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "Urls",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "Urls",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "price",
                table: "Urls",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "order",
                table: "Urls",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Itemdetails",
                table: "Itemdetails",
                column: "url");
        }
    }
}
