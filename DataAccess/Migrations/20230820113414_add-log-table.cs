using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addlogtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Therapy_TherapyId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumber_Patients_PatientId",
                table: "PhoneNumber");

            migrationBuilder.DropForeignKey(
                name: "FK_Therapy_Doctors_DoctorId",
                table: "Therapy");

            migrationBuilder.DropForeignKey(
                name: "FK_Therapy_Patients_PatientId",
                table: "Therapy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Therapy",
                table: "Therapy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneNumber",
                table: "PhoneNumber");

            migrationBuilder.RenameTable(
                name: "Therapy",
                newName: "Therapies");

            migrationBuilder.RenameTable(
                name: "PhoneNumber",
                newName: "PhoneNumbers");

            migrationBuilder.RenameIndex(
                name: "IX_Therapy_PatientId",
                table: "Therapies",
                newName: "IX_Therapies_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Therapy_DoctorId",
                table: "Therapies",
                newName: "IX_Therapies_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumber_PatientId",
                table: "PhoneNumbers",
                newName: "IX_PhoneNumbers_PatientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Therapies",
                table: "Therapies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneNumbers",
                table: "PhoneNumbers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExceptionMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Therapies_TherapyId",
                table: "Payments",
                column: "TherapyId",
                principalTable: "Therapies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_Patients_PatientId",
                table: "PhoneNumbers",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Therapies_Doctors_DoctorId",
                table: "Therapies",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Therapies_Patients_PatientId",
                table: "Therapies",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Therapies_TherapyId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_Patients_PatientId",
                table: "PhoneNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_Therapies_Doctors_DoctorId",
                table: "Therapies");

            migrationBuilder.DropForeignKey(
                name: "FK_Therapies_Patients_PatientId",
                table: "Therapies");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Therapies",
                table: "Therapies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneNumbers",
                table: "PhoneNumbers");

            migrationBuilder.RenameTable(
                name: "Therapies",
                newName: "Therapy");

            migrationBuilder.RenameTable(
                name: "PhoneNumbers",
                newName: "PhoneNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Therapies_PatientId",
                table: "Therapy",
                newName: "IX_Therapy_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Therapies_DoctorId",
                table: "Therapy",
                newName: "IX_Therapy_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumbers_PatientId",
                table: "PhoneNumber",
                newName: "IX_PhoneNumber_PatientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Therapy",
                table: "Therapy",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneNumber",
                table: "PhoneNumber",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Therapy_TherapyId",
                table: "Payments",
                column: "TherapyId",
                principalTable: "Therapy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumber_Patients_PatientId",
                table: "PhoneNumber",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Therapy_Doctors_DoctorId",
                table: "Therapy",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Therapy_Patients_PatientId",
                table: "Therapy",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
