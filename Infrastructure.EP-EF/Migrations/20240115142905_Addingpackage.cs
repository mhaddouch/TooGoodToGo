using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.EP_EF.Migrations
{
    /// <inheritdoc />
    public partial class Addingpackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Canteens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<int>(type: "int", nullable: false),
                    OfferHotMeals = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canteens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContainsAlcohol = table.Column<bool>(type: "bit", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentNumber = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "Date", nullable: false),
                    StudyCity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Voorbeelds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voorbeelds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeNumber = table.Column<int>(type: "int", nullable: false),
                    CanteenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Canteens_CanteenId",
                        column: x => x.CanteenId,
                        principalTable: "Canteens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<int>(type: "int", nullable: false),
                    Meal = table.Column<int>(type: "int", nullable: false),
                    CanteenId = table.Column<int>(type: "int", nullable: true),
                    RetrieveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeadLineRetriveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReserverdByStudentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Packages_Canteens_CanteenId",
                        column: x => x.CanteenId,
                        principalTable: "Canteens",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Packages_Students_ReserverdByStudentId",
                        column: x => x.ReserverdByStudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PackageProduct",
                columns: table => new
                {
                    PackagesId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageProduct", x => new { x.PackagesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_PackageProduct_Packages_PackagesId",
                        column: x => x.PackagesId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Canteens",
                columns: new[] { "Id", "City", "LocationName", "OfferHotMeals" },
                values: new object[,]
                {
                    { 1, 0, "LA", true },
                    { 2, 1, "LD", true },
                    { 3, 0, "HA", true },
                    { 4, 0, "LA", true }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ContainsAlcohol", "Name", "PhotoPath" },
                values: new object[,]
                {
                    { 1, false, "bread", "https://i0.wp.com/www.vickyvandijk.nl/wp-content/uploads/2020/04/Vicky-van-Dijk-Knapperig-wit-brood-03.jpg?fit=1500%2C2100&ssl=1" },
                    { 2, false, "apple", "https://images.nrc.nl/iV2oqfYUkk7SP_itBSSOkEk6-TE=/1280x/filters:no_upscale()/s3/static.nrc.nl/images/gn4/stripped/data93925993-1e8a11.jpg" },
                    { 3, false, "milk", "https://images.unsplash.com/photo-1588710929895-6ee7a0a4d155?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MjR8fG1pbGt8ZW58MHx8MHx8fDA%3D" }
                });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "CanteenId", "City", "DeadLineRetriveDate", "Meal", "Name", "Price", "ReserverdByStudentId", "RetrieveDate" },
                values: new object[,]
                {
                    { 1, 1, 0, new DateTime(2024, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Healthy meal", 8, null, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, 0, new DateTime(2024, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Lots of bread", 8, null, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CanteenId",
                table: "Employees",
                column: "CanteenId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageProduct_ProductsId",
                table: "PackageProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_CanteenId",
                table: "Packages",
                column: "CanteenId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_ReserverdByStudentId",
                table: "Packages",
                column: "ReserverdByStudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "PackageProduct");

            migrationBuilder.DropTable(
                name: "Voorbeelds");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Canteens");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
