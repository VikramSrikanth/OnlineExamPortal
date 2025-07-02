using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassLib.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Usertables",
                table: "Usertables");

            migrationBuilder.RenameTable(
                name: "Usertables",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Usertables");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usertables",
                table: "Usertables",
                column: "UserId");
        }
    }
}
