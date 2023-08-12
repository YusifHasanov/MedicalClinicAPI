using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Doctors_DoctorId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_DoctorId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Works",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Patients",
                newName: "Gender");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Patients",
                type: "nvarchar(55)",
                maxLength: 55,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 55);

            migrationBuilder.AlterColumn<int>(
                name: "IsCame",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Diagnosis",
                table: "Patients",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Patients",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Patients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Bleeding",
                table: "Patients",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DelayedSurgeries",
                table: "Patients",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DrugAllergy",
                table: "Patients",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GeneralStateOfHealth",
                table: "Patients",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InjuryProblem",
                table: "Patients",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PregnancyStatus",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "ReactionToAnesthesia",
                table: "Patients",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkDone",
                table: "Patients",
                type: "nvarchar(2500)",
                maxLength: 2500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkToBeDone",
                table: "Patients",
                type: "nvarchar(2500)",
                maxLength: 2500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DoctorPatient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorPatient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorPatient_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorPatient_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneNumber_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorPatient_DoctorId",
                table: "DoctorPatient",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorPatient_PatientId",
                table: "DoctorPatient",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumber_PatientId",
                table: "PhoneNumber",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorPatient");

            migrationBuilder.DropTable(
                name: "PhoneNumber");

            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Bleeding",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DelayedSurgeries",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DrugAllergy",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "GeneralStateOfHealth",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "InjuryProblem",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PregnancyStatus",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "ReactionToAnesthesia",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "WorkDone",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "WorkToBeDone",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Patients",
                newName: "DoctorId");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Patients",
                type: "nvarchar(max)",
                maxLength: 55,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(55)",
                oldMaxLength: 55);

            migrationBuilder.AlterColumn<bool>(
                name: "IsCame",
                table: "Patients",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Diagnosis",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<string>(
                name: "Works",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DoctorId",
                table: "Patients",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Doctors_DoctorId",
                table: "Patients",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
