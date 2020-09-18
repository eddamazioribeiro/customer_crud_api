using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerApp.Repository.Migrations
{
    public partial class addedStateToAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Addresses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Addresses");
        }
    }
}
