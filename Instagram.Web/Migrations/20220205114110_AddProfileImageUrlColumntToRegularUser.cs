using Microsoft.EntityFrameworkCore.Migrations;

namespace Instagram.Web.Migrations
{
    public partial class AddProfileImageUrlColumntToRegularUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "RegularUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "RegularUsers");
        }
    }
}
