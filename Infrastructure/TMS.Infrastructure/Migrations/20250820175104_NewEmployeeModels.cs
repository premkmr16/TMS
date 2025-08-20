using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewEmployeeModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Employees",
                type: "boolean",
                nullable: false,
                defaultValueSql: "true",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValueSql: "true");

            migrationBuilder.AddColumn<string>(
                name: "ManagerId",
                table: "Employees",
                type: "varchar(26)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "Employees",
                type: "varchar(26)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EmployeeCertifications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "varchar(26)", nullable: false),
                    CertificationName = table.Column<string>(type: "varchar(100)", nullable: false),
                    IssuingOrganization = table.Column<string>(type: "varchar(100)", nullable: false),
                    IssuedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CertificationId = table.Column<string>(type: "varchar(50)", nullable: false),
                    CertificationUrl = table.Column<string>(type: "varchar(200)", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true"),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCertifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeCertifications_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEducations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "varchar(26)", nullable: false),
                    EducationalLevel = table.Column<string>(type: "varchar(20)", nullable: false),
                    Qualification = table.Column<string>(type: "text", nullable: true),
                    Institution = table.Column<string>(type: "varchar(100)", nullable: false),
                    BoardOrUniversity = table.Column<string>(type: "varchar(20)", nullable: false),
                    YearOfPassing = table.Column<string>(type: "varchar(4)", nullable: false),
                    PercentageOrCgpa = table.Column<decimal>(type: "numeric", nullable: false),
                    Specialization = table.Column<string>(type: "varchar(20)", nullable: true),
                    City = table.Column<string>(type: "varchar(50)", nullable: false),
                    State = table.Column<string>(type: "varchar(50)", nullable: false),
                    Country = table.Column<string>(type: "varchar(50)", nullable: false),
                    PostalCode = table.Column<string>(type: "varchar(10)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEducations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeEducations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRelations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(26)", nullable: false),
                    MentorId = table.Column<string>(type: "varchar(26)", nullable: false),
                    MenteeId = table.Column<string>(type: "varchar(26)", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    StartDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeRelations_Employees_MenteeId",
                        column: x => x.MenteeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeRelations_Employees_MentorId",
                        column: x => x.MentorId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(26)", nullable: false),
                    Name = table.Column<string>(type: "varchar(30)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeesIdentity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "varchar(26)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    EmergencyContactName = table.Column<string>(type: "varchar(50)", nullable: false),
                    EmergencyContactNumber = table.Column<string>(type: "varchar(10)", nullable: false),
                    AadharNumber = table.Column<string>(type: "varchar(12)", nullable: false),
                    PanNumber = table.Column<string>(type: "varchar(10)", nullable: false),
                    PassportNumber = table.Column<string>(type: "varchar(12)", nullable: true),
                    VoterId = table.Column<string>(type: "varchar(18)", nullable: false),
                    BloodGroup = table.Column<string>(type: "varchar(5)", nullable: false),
                    Gender = table.Column<string>(type: "varchar(10)", nullable: false),
                    CurrentAddress = table.Column<string>(type: "varchar(255)", nullable: false),
                    PermanentAddress = table.Column<string>(type: "varchar(255)", nullable: true),
                    City = table.Column<string>(type: "varchar(50)", nullable: false),
                    State = table.Column<string>(type: "varchar(50)", nullable: false),
                    Country = table.Column<string>(type: "varchar(50)", nullable: false),
                    PostalCode = table.Column<string>(type: "varchar(10)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesIdentity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeesIdentity_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCertifications_EmployeeId",
                table: "EmployeeCertifications",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEducations_EmployeeId",
                table: "EmployeeEducations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRelations_MenteeId",
                table: "EmployeeRelations",
                column: "MenteeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRelations_MentorId",
                table: "EmployeeRelations",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesIdentity_EmployeeId",
                table: "EmployeesIdentity",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeRoles_RoleId",
                table: "Employees",
                column: "RoleId",
                principalTable: "EmployeeRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeRoles_RoleId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_ManagerId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeCertifications");

            migrationBuilder.DropTable(
                name: "EmployeeEducations");

            migrationBuilder.DropTable(
                name: "EmployeeRelations");

            migrationBuilder.DropTable(
                name: "EmployeeRoles");

            migrationBuilder.DropTable(
                name: "EmployeesIdentity");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_RoleId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Employees");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Employees",
                type: "boolean",
                nullable: true,
                defaultValueSql: "true",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValueSql: "true");
        }
    }
}
