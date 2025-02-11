using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerceClean.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDefaultTypeAndBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM ProductBrands;");
            migrationBuilder.Sql("DELETE FROM ProductTypes;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
