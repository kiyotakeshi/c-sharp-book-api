using Microsoft.EntityFrameworkCore.Migrations;

namespace BookApi.Migrations
{
    public partial class ModifyCountryTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Counter_CountryId",
                table: "Authors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Counter",
                table: "Counter");

            migrationBuilder.RenameTable(
                name: "Counter",
                newName: "Country");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Country_CountryId",
                table: "Authors",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Country_CountryId",
                table: "Authors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Counter");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Counter",
                table: "Counter",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Counter_CountryId",
                table: "Authors",
                column: "CountryId",
                principalTable: "Counter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
