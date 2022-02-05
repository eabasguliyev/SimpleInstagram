using Microsoft.EntityFrameworkCore.Migrations;

namespace Instagram.Web.Migrations
{
    public partial class AddNewColumnsToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Body",
                table: "Posts",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "Caption",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caption",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Posts",
                newName: "Body");
        }
    }
}
