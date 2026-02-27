using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthWellness.Migrations
{
    /// <inheritdoc />
    public partial class AddNameToHealthForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "HealthForms",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "HealthForms");
        }
    }
}
