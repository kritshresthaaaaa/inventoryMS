using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedOrderDetailModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderDetailId",
                table: "OrderDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderDetailId",
                table: "OrderDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
