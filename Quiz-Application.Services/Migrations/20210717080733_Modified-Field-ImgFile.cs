using Microsoft.EntityFrameworkCore.Migrations;

namespace Quiz_Application.Services.Migrations
{
    public partial class ModifiedFieldImgFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImgPath",
                table: "Candidate",
                newName: "ImgFile");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImgFile",
                table: "Candidate",
                newName: "ImgPath");
        }
    }
}
