using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkDone",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "WorkToBeDone",
                table: "Patients");

            migrationBuilder.AddColumn<string>(
                name: "WorkDone",
                table: "Therapy",
                type: "nvarchar(2500)",
                maxLength: 2500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkToBeDone",
                table: "Therapy",
                type: "nvarchar(2500)",
                maxLength: 2500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkDone",
                table: "Therapy");

            migrationBuilder.DropColumn(
                name: "WorkToBeDone",
                table: "Therapy");

            migrationBuilder.AddColumn<string>(
                name: "WorkDone",
                table: "Patients",
                type: "nvarchar(2500)",
                maxLength: 2500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkToBeDone",
                table: "Patients",
                type: "nvarchar(2500)",
                maxLength: 2500,
                nullable: true);
        }
    }
}
