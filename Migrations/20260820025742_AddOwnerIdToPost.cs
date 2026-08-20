using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blogmanager_nguyenngocvi_22t1020794.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerIdToPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Posts",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Posts");
        }
    }
}
