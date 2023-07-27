using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmFresh.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class other : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Capacity",
                table: "SoilAmendmentsFactory",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "MaxCapacity",
                table: "SoilAmendmentsFactory",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "Selling",
                table: "SoilAmendmentsFactory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<double>(
                name: "Capacity",
                table: "PestAndDiseaseFactory",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "MaxCapacity",
                table: "PestAndDiseaseFactory",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "Selling",
                table: "PestAndDiseaseFactory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<double>(
                name: "Capacity",
                table: "OrganicSeedsFactory",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "MaxCapacity",
                table: "OrganicSeedsFactory",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "Selling",
                table: "OrganicSeedsFactory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<double>(
                name: "Capacity",
                table: "OrganicFertilizerFactory",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "MaxCapacity",
                table: "OrganicFertilizerFactory",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "Selling",
                table: "OrganicFertilizerFactory",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxCapacity",
                table: "SoilAmendmentsFactory");

            migrationBuilder.DropColumn(
                name: "Selling",
                table: "SoilAmendmentsFactory");

            migrationBuilder.DropColumn(
                name: "MaxCapacity",
                table: "PestAndDiseaseFactory");

            migrationBuilder.DropColumn(
                name: "Selling",
                table: "PestAndDiseaseFactory");

            migrationBuilder.DropColumn(
                name: "MaxCapacity",
                table: "OrganicSeedsFactory");

            migrationBuilder.DropColumn(
                name: "Selling",
                table: "OrganicSeedsFactory");

            migrationBuilder.DropColumn(
                name: "MaxCapacity",
                table: "OrganicFertilizerFactory");

            migrationBuilder.DropColumn(
                name: "Selling",
                table: "OrganicFertilizerFactory");

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "SoilAmendmentsFactory",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "PestAndDiseaseFactory",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "OrganicSeedsFactory",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "OrganicFertilizerFactory",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
