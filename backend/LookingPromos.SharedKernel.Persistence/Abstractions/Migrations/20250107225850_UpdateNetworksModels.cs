using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LookingPromos.SharedKernel.Persistence.Abstractions.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNetworksModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProviderCategoryId",
                table: "Categories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProviderCategoryId",
                table: "Categories");
        }
    }
}
