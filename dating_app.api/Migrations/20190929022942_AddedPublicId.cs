using Microsoft.EntityFrameworkCore.Migrations;

namespace dating_app.api.Migrations
{
    public partial class AddedPublicId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.RenameColumn(
            //     name: "url",
            //     table: "Photos",
            //     newName: "Url");

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Photos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Photos",
                newName: "url");
        }
    }
}
