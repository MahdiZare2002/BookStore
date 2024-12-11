using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_some_columns_to_books : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "inHomePage",
                table: "Books",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Books",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "inHomePage",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Books");
        }
    }
}
