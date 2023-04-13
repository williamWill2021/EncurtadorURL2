using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EncurtadorURL.API.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdShortUrl",
                table: "UrlEncurtadas",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdShortUrl",
                table: "UrlEncurtadas");
        }
    }
}
