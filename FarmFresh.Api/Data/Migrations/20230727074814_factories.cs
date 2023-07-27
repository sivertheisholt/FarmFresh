using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmFresh.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class factories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OrganicFertilizerFactoryFactoryId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganicSeedsFactoryFactoryId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PestAndDiseaseFactoryFactoryId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoilAmendmentsFactoryFactoryId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CoalPowerPlant",
                columns: table => new
                {
                    PlantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Production = table.Column<double>(type: "float", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoalPowerPlant", x => x.PlantId);
                    table.ForeignKey(
                        name: "FK_CoalPowerPlant_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "OrganicFertilizerFactory",
                columns: table => new
                {
                    FactoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Production = table.Column<int>(type: "int", nullable: false),
                    PowerUsage = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganicFertilizerFactory", x => x.FactoryId);
                });

            migrationBuilder.CreateTable(
                name: "OrganicSeedsFactory",
                columns: table => new
                {
                    FactoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Production = table.Column<int>(type: "int", nullable: false),
                    PowerUsage = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganicSeedsFactory", x => x.FactoryId);
                });

            migrationBuilder.CreateTable(
                name: "PestAndDiseaseFactory",
                columns: table => new
                {
                    FactoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Production = table.Column<int>(type: "int", nullable: false),
                    PowerUsage = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PestAndDiseaseFactory", x => x.FactoryId);
                });

            migrationBuilder.CreateTable(
                name: "SoilAmendmentsFactory",
                columns: table => new
                {
                    FactoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Production = table.Column<int>(type: "int", nullable: false),
                    PowerUsage = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilAmendmentsFactory", x => x.FactoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_OrganicFertilizerFactoryFactoryId",
                table: "User",
                column: "OrganicFertilizerFactoryFactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_User_OrganicSeedsFactoryFactoryId",
                table: "User",
                column: "OrganicSeedsFactoryFactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PestAndDiseaseFactoryFactoryId",
                table: "User",
                column: "PestAndDiseaseFactoryFactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_User_SoilAmendmentsFactoryFactoryId",
                table: "User",
                column: "SoilAmendmentsFactoryFactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CoalPowerPlant_UserId",
                table: "CoalPowerPlant",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_OrganicFertilizerFactory_OrganicFertilizerFactoryFactoryId",
                table: "User",
                column: "OrganicFertilizerFactoryFactoryId",
                principalTable: "OrganicFertilizerFactory",
                principalColumn: "FactoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_OrganicSeedsFactory_OrganicSeedsFactoryFactoryId",
                table: "User",
                column: "OrganicSeedsFactoryFactoryId",
                principalTable: "OrganicSeedsFactory",
                principalColumn: "FactoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_PestAndDiseaseFactory_PestAndDiseaseFactoryFactoryId",
                table: "User",
                column: "PestAndDiseaseFactoryFactoryId",
                principalTable: "PestAndDiseaseFactory",
                principalColumn: "FactoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_SoilAmendmentsFactory_SoilAmendmentsFactoryFactoryId",
                table: "User",
                column: "SoilAmendmentsFactoryFactoryId",
                principalTable: "SoilAmendmentsFactory",
                principalColumn: "FactoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_OrganicFertilizerFactory_OrganicFertilizerFactoryFactoryId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_OrganicSeedsFactory_OrganicSeedsFactoryFactoryId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_PestAndDiseaseFactory_PestAndDiseaseFactoryFactoryId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_SoilAmendmentsFactory_SoilAmendmentsFactoryFactoryId",
                table: "User");

            migrationBuilder.DropTable(
                name: "CoalPowerPlant");

            migrationBuilder.DropTable(
                name: "OrganicFertilizerFactory");

            migrationBuilder.DropTable(
                name: "OrganicSeedsFactory");

            migrationBuilder.DropTable(
                name: "PestAndDiseaseFactory");

            migrationBuilder.DropTable(
                name: "SoilAmendmentsFactory");

            migrationBuilder.DropIndex(
                name: "IX_User_OrganicFertilizerFactoryFactoryId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_OrganicSeedsFactoryFactoryId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_PestAndDiseaseFactoryFactoryId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_SoilAmendmentsFactoryFactoryId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "OrganicFertilizerFactoryFactoryId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "OrganicSeedsFactoryFactoryId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PestAndDiseaseFactoryFactoryId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SoilAmendmentsFactoryFactoryId",
                table: "User");
        }
    }
}
