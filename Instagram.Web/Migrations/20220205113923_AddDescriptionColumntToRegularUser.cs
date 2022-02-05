using Microsoft.EntityFrameworkCore.Migrations;

namespace Instagram.Web.Migrations
{
    public partial class AddDescriptionColumntToRegularUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RegularUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "RegularUsers");
        }
    }
}
