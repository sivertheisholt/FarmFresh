using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmFresh.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class capacity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "SoilAmendmentsFactory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "PestAndDiseaseFactory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "OrganicSeedsFactory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "OrganicFertilizerFactory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "SoilAmendmentsFactory");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "PestAndDiseaseFactory");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "OrganicSeedsFactory");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "OrganicFertilizerFactory");
        }
    }
}
