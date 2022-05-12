using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientMasterApp.Migrations
{
    public partial class patientsys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbPatientInfo",
                columns: table => new
                {
                    PatientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(nullable: true),
                    age = table.Column<int>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    vaccinestatus = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbPatientInfo", x => x.PatientId);
                });

            migrationBuilder.CreateTable(
                name: "DbVaccinestatus",
                columns: table => new
                {
                    VaccineStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(nullable: false),
                    Dose = table.Column<string>(nullable: true),
                    createdby = table.Column<DateTime>(nullable: false),
                    updatedby = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbVaccinestatus", x => x.VaccineStatusId);
                    table.ForeignKey(
                        name: "FK_DbVaccinestatus_DbPatientInfo_PatientId",
                        column: x => x.PatientId,
                        principalTable: "DbPatientInfo",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbVaccinestatus_PatientId",
                table: "DbVaccinestatus",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbVaccinestatus");

            migrationBuilder.DropTable(
                name: "DbPatientInfo");
        }
    }
}
