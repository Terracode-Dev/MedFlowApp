using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedFlow.Migrations
{
    /// <inheritdoc />
    public partial class addedToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "token",
                table: "prescriptions",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "token",
                table: "prescriptions");
        }
    }
}
