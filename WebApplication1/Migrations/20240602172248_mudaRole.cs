using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OngTDE.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class mudaRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Roles",
                table: "Usuarios",
                type: "character varying(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Roles",
                table: "Usuarios",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldUnicode: false,
                oldMaxLength: 100);
        }
    }
}
