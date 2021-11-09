using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingViews.Migrations
{
    public partial class addingtotalcount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalCount",
                table: "Positions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCount",
                table: "Positions");
        }
    }
}
