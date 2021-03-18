using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class addlistofusers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IssueId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_IssueId",
                table: "Users",
                column: "IssueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Issues_IssueId",
                table: "Users",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Issues_IssueId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_IssueId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IssueId",
                table: "Users");
        }
    }
}
