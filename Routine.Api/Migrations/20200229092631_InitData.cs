using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Routine.Api.Migrations
{
    public partial class InitData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Introduction = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    companyId = table.Column<Guid>(nullable: false),
                    employeeNo = table.Column<string>(maxLength: 10, nullable: false),
                    firstName = table.Column<string>(maxLength: 50, nullable: false),
                    lastName = table.Column<string>(maxLength: 50, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    dateOfBirth = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_companyId",
                        column: x => x.companyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("da7ee895-bb98-4b80-b0f1-d9d86fef8c95"), "Great Company", "Microsoft" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("1544a292-1c80-4595-bc4a-2a4952fc5c1f"), "Don't be evil", "Google" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("d030761a-7ab4-4235-9232-def88be14d4a"), "Fubao Company", "Alipapa" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Gender", "companyId", "dateOfBirth", "employeeNo", "firstName", "lastName" },
                values: new object[] { new Guid("9665c8ef-03d4-4af1-87bc-d84cdaded5f5"), 1, new Guid("da7ee895-bb98-4b80-b0f1-d9d86fef8c95"), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1wckk9NbrGGUSZU4", "Bruce", "Forster" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Gender", "companyId", "dateOfBirth", "employeeNo", "firstName", "lastName" },
                values: new object[] { new Guid("12bcfa41-5b5c-45e9-aa34-5a7a52183584"), 2, new Guid("da7ee895-bb98-4b80-b0f1-d9d86fef8c95"), new DateTime(1988, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VU7yqFzVUXqnVJyU", "Donald", "Washington" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Gender", "companyId", "dateOfBirth", "employeeNo", "firstName", "lastName" },
                values: new object[] { new Guid("d51d5aa1-2329-4e40-8573-4a8ddb520c18"), 1, new Guid("da7ee895-bb98-4b80-b0f1-d9d86fef8c95"), new DateTime(1958, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1RewsGnQdvozB0cb", "Vito", "Evans" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Gender", "companyId", "dateOfBirth", "employeeNo", "firstName", "lastName" },
                values: new object[] { new Guid("bc7d1bbd-bd48-413d-95e7-c8f800e32629"), 2, new Guid("1544a292-1c80-4595-bc4a-2a4952fc5c1f"), new DateTime(1994, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "huHM1OcqoLLpyL0X", "Polly", "Salome" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Gender", "companyId", "dateOfBirth", "employeeNo", "firstName", "lastName" },
                values: new object[] { new Guid("053988cc-05b4-4bf5-8c24-ea826d5ee90d"), 2, new Guid("1544a292-1c80-4595-bc4a-2a4952fc5c1f"), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zpsy9IdWEc6kdrhQ", "Adair", "Kent" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Gender", "companyId", "dateOfBirth", "employeeNo", "firstName", "lastName" },
                values: new object[] { new Guid("a035c368-1171-4633-baee-fcfa31bf9b3b"), 2, new Guid("1544a292-1c80-4595-bc4a-2a4952fc5c1f"), new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NO9drmGvuBM9hBom", "Edison", "Thackeray" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Gender", "companyId", "dateOfBirth", "employeeNo", "firstName", "lastName" },
                values: new object[] { new Guid("e9d3f23d-c71d-4c12-bcc2-a9554ba50396"), 1, new Guid("d030761a-7ab4-4235-9232-def88be14d4a"), new DateTime(1976, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "g7BSz9HJZoJui5wr", "Len", "Partridge" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Gender", "companyId", "dateOfBirth", "employeeNo", "firstName", "lastName" },
                values: new object[] { new Guid("3c3fe8ec-c441-4855-83cd-ea9e3cf60fc9"), 1, new Guid("d030761a-7ab4-4235-9232-def88be14d4a"), new DateTime(1983, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "I4nNzk1ezsuSEaVC", "Bernice", "Harold" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_companyId",
                table: "Employees",
                column: "companyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
