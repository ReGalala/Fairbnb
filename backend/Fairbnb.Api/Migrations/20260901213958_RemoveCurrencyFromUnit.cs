using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fairbnb.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCurrencyFromUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Units");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Units",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
