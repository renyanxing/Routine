using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Routine.Api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Country = table.Column<string>(maxLength: 100, nullable: false),
                    Industry = table.Column<string>(maxLength: 50, nullable: false),
                    Product = table.Column<string>(maxLength: 50, nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("da7ee895-bb98-4b80-b0f1-d9d86fef8c95"), "USA", "Software", "Great Company", "Microsoft", "Software" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("1544a292-1c80-4595-bc4a-2a4952fc5c1f"), "USA", "Internet", "Don't be evil", "Google", "Software" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("d030761a-7ab4-4235-9232-def88be14d4a"), "China", "Internet", "Fubao Company", "Alipapa", "Software" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Gender", "companyId", "dateOfBirth", "employeeNo", "firstName", "lastName" },
                values: new object[] { new Guid("9665c8ef-03d4-4af1-87bc-d84cdaded5f5"), 1, new Guid("da7ee895-bb98-4b80-b0f1-d9d86fef8c95"), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1wckk9NbrGGUSZU4", "Bruce", "Forster" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Gender", "companyId", "dateOfBirth", "employeeNo", "firstName", "lastName" },
                values: new object[] { new Guid("bc4873aa-623f-4069-96a4-76c323f39e82"), 2, new Guid("da7ee895-bb98-4b80-b0f1-d9d86fef8c95"), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1wckk9NbrGGUSZU4", "Bruce", "Forster" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Gender", "companyId", "dateOfBirth", "employeeNo", "firstName", "lastName" },
                values: new object[] { new Guid("d2f3d4bd-984a-49f9-9dfc-394cd204158b"), 1, new Guid("da7ee895-bb98-4b80-b0f1-d9d86fef8c95"), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1wckk9NbrGGUSZU4", "Bruce", "Forster" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Gender", "companyId", "dateOfBirth", "employeeNo", "firstName", "lastName" },
                values: new object[] { new Guid("43f76027-229b-435f-95f6-d47fa4f8e7d5"), 2, new Guid("da7ee895-bb98-4b80-b0f1-d9d86fef8c95"), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1wckk9NbrGGUSZU4", "Bruce", "Forster" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Gender", "companyId", "dateOfBirth", "employeeNo", "firstName", "lastName" },
                values: new object[] { new Guid("5ff6be02-52cd-4914-af1f-b6b1f73dffdd"), 1, new Guid("da7ee895-bb98-4b80-b0f1-d9d86fef8c95"), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1wckk9NbrGGUSZU4", "Bruce", "Forster" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Gender", "companyId", "dateOfBirth", "employeeNo", "firstName", "lastName" },
                values: new object[] { new Guid("14427c90-cc70-421a-a25d-11f9e3a1e5a7"), 2, new Guid("da7ee895-bb98-4b80-b0f1-d9d86fef8c95"), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1wckk9NbrGGUSZU4", "Bruce", "Forster" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Gender", "companyId", "dateOfBirth", "employeeNo", "firstName", "lastName" },
                values: new object[] { new Guid("c5f415f7-edf8-4894-99f3-08c35e80f871"), 1, new Guid("da7ee895-bb98-4b80-b0f1-d9d86fef8c95"), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1wckk9NbrGGUSZU4", "Bruce", "Forster" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Gender", "companyId", "dateOfBirth", "employeeNo", "firstName", "lastName" },
                values: new object[] { new Guid("75d0c702-cf15-4064-a7c9-f58685be0d94"), 2, new Guid("da7ee895-bb98-4b80-b0f1-d9d86fef8c95"), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1wckk9NbrGGUSZU4", "Bruce", "Forster" });

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
