using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthWellness.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailtoHeathForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "HealthForms",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "HealthForms");
        }
    }
}
