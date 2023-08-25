using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedFlow.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userdetails_usertypes_user_type",
                table: "userdetails");

            migrationBuilder.AlterColumn<int>(
                name: "user_type",
                table: "userdetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "prescription_id",
                table: "prescriptionq",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_userdetails_usertypes_user_type",
                table: "userdetails",
                column: "user_type",
                principalTable: "usertypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userdetails_usertypes_user_type",
                table: "userdetails");

            migrationBuilder.DropColumn(
                name: "prescription_id",
                table: "prescriptionq");

            migrationBuilder.AlterColumn<int>(
                name: "user_type",
                table: "userdetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_userdetails_usertypes_user_type",
                table: "userdetails",
                column: "user_type",
                principalTable: "usertypes",
                principalColumn: "Id");
        }
    }
}
