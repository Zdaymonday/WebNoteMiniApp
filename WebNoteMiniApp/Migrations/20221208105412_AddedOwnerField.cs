using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebNoteMiniApp.Migrations
{
    public partial class AddedOwnerField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Notes");
        }
    }
}
