using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageId);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "RailwayCompany",
                columns: table => new
                {
                    RailwayCompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RailwayCompany", x => x.RailwayCompanyId);
                    table.ForeignKey(
                        name: "FK_RailwayCompany_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    TagId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Interface = table.Column<int>(type: "int", nullable: true),
                    Sound = table.Column<bool>(type: "bit", nullable: true),
                    Control = table.Column<int>(type: "int", nullable: true),
                    LocoType = table.Column<int>(type: "int", nullable: true),
                    AutoCoupling = table.Column<bool>(type: "bit", nullable: true),
                    NumOfDrivenAxels = table.Column<int>(type: "int", nullable: true),
                    DigitalDecoderId = table.Column<int>(type: "int", nullable: true),
                    Scale = table.Column<int>(type: "int", nullable: true),
                    Epoch = table.Column<int>(type: "int", nullable: true),
                    Length = table.Column<float>(type: "real", nullable: true),
                    NumOfAxels = table.Column<int>(type: "int", nullable: true),
                    RailWayCompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Product_DigitalDecoderId",
                        column: x => x.DigitalDecoderId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_RailwayCompany_RailWayCompanyId",
                        column: x => x.RailWayCompanyId,
                        principalTable: "RailwayCompany",
                        principalColumn: "RailwayCompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImageProduct",
                columns: table => new
                {
                    ImagesImageId = table.Column<int>(type: "int", nullable: false),
                    ProductsProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageProduct", x => new { x.ImagesImageId, x.ProductsProductId });
                    table.ForeignKey(
                        name: "FK_ImageProduct_Image_ImagesImageId",
                        column: x => x.ImagesImageId,
                        principalTable: "Image",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImageProduct_Product_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockStatus",
                columns: table => new
                {
                    StockStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    NextStock = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockStatus", x => x.StockStatusId);
                    table.ForeignKey(
                        name: "FK_StockStatus_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, "Denmark" },
                    { 2, "Germany" },
                    { 3, "Switzerland" }
                });

            migrationBuilder.InsertData(
                table: "Image",
                columns: new[] { "ImageId", "Url" },
                values: new object[,]
                {
                    { 1, "https://www.roco.cc/doc/idimages/def2/1633611600/123106022013017006010001016009105021031014117.jpg" },
                    { 2, "https://www.roco.cc/doc/idimages/def2/1633611600/123109024010020014010001016009105021031014117.jpg" },
                    { 3, "https://www.roco.cc/doc/idimages/def2/1633611600/123109026011022009010001016009105021031014117.jpg" },
                    { 4, "https://www.roco.cc/doc/idimages/def2/1633611600/123109023008017012010001016009105021031014117.jpg" },
                    { 5, "https://www.roco.cc/doc/idimages/def2/1633615200/126105029012017019089002030013103027028015121010010.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Description", "Discriminator", "Interface", "Name", "Price", "Sound", "TagId" },
                values: new object[] { 1, "Suitable for Gauge H0. The decoder is equipped with the RailCom® function. Maximum motor current: 1.2 A.", "DigitalDecoder", 0, "PluX22 sound decoder (NEM 658)", 92.4m, true, null });

            migrationBuilder.InsertData(
                table: "Tag",
                column: "TagId",
                values: new object[]
                {
                    "Sale",
                    "New"
                });

            migrationBuilder.InsertData(
                table: "ImageProduct",
                columns: new[] { "ImagesImageId", "ProductsProductId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "RailwayCompany",
                columns: new[] { "RailwayCompanyId", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "DSB" },
                    { 2, 2, "DB" },
                    { 3, 2, "DR" },
                    { 4, 3, "SBB" }
                });

            migrationBuilder.InsertData(
                table: "StockStatus",
                columns: new[] { "StockStatusId", "Amount", "NextStock", "ProductId" },
                values: new object[] { 1, 23L, new DateTime(2021, 11, 7, 17, 0, 19, 449, DateTimeKind.Local).AddTicks(4564), 1 });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "AutoCoupling", "Control", "Description", "DigitalDecoderId", "Discriminator", "Epoch", "Length", "LocoType", "Name", "NumOfAxels", "NumOfDrivenAxels", "Price", "RailWayCompanyId", "Scale", "TagId" },
                values: new object[] { 2, false, 3, "The 023 series was a true all-round genius. The locomotive hauled commuter trains, fast and express trains. Sometimes they hauled even freight trains. The newly designed locomotive of the class 023 (which until 1968 was designated class 23) was being used even in the epoch IV. On Dec. 31 1971, 76 locomotives were a permanent part of the rolling stock of the DB and without exception they were stationed at the three railway depots Saarbrücken, Kaiserslautern and Crailsheim.", null, "Locomotive", 4, 24.5f, 1, "BR 023 040-9", 9, 4, 229.9m, 2, 1, "New" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "AutoCoupling", "Control", "Description", "DigitalDecoderId", "Discriminator", "Epoch", "Length", "LocoType", "Name", "NumOfAxels", "NumOfDrivenAxels", "Price", "RailWayCompanyId", "Scale", "TagId" },
                values: new object[] { 4, false, 3, "In the period between 1942 to 1950, over 7000 units of the class 52 war locomotive were built. These were constructed with as little effort as possible and savings were also made on the material wherever possible. With a weight of 84 tons, the loco achieved an output of 1,192 kW and a top speed of 80 km / h. The Deutsche Bundesbahn mainly got rid of the locomotives as early as 1953 - since it had sufficient machines of the series 50 and series 44 to haul the heavy goods trains. Only a few locomotives built in 1945 remained with the DB until 1962.", null, "Locomotive", 3, 26.5f, 1, "BR 52", 10, 4, 319.9m, 2, 1, "New" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "AutoCoupling", "Control", "Description", "DigitalDecoderId", "Discriminator", "Epoch", "Length", "LocoType", "Name", "NumOfAxels", "NumOfDrivenAxels", "Price", "RailWayCompanyId", "Scale", "TagId" },
                values: new object[] { 3, false, 4, "In 1992, the first locomotive Re 460 of the Swiss Federal Railways rolled out of the factory halls of the companies SLM and BBC in Oerlikon, Switzerland. The locomotive became known to the public as \"Lok 2000\". It stands for fast and modern passenger transport in Switzerland. An eye-catching and particularly aerodynamic design with a large front window, roof cladding and beads on the side wall make the class 460 visually an unbeatable rail vehicle.", 1, "Locomotive", 6, 21.2f, 3, "Re 460 068-0", 4, 4, 321.9m, 4, 1, null });

            migrationBuilder.InsertData(
                table: "ImageProduct",
                columns: new[] { "ImagesImageId", "ProductsProductId" },
                values: new object[,]
                {
                    { 3, 2 },
                    { 4, 4 },
                    { 5, 4 },
                    { 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "StockStatus",
                columns: new[] { "StockStatusId", "Amount", "NextStock", "ProductId" },
                values: new object[,]
                {
                    { 2, 5L, new DateTime(2021, 12, 7, 17, 0, 19, 453, DateTimeKind.Local).AddTicks(7554), 2 },
                    { 4, 1L, new DateTime(2022, 5, 7, 17, 0, 19, 453, DateTimeKind.Local).AddTicks(7727), 4 },
                    { 3, 2L, new DateTime(2021, 10, 23, 17, 0, 19, 453, DateTimeKind.Local).AddTicks(7687), 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageProduct_ProductsProductId",
                table: "ImageProduct",
                column: "ProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DigitalDecoderId",
                table: "Product",
                column: "DigitalDecoderId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_RailWayCompanyId",
                table: "Product",
                column: "RailWayCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_TagId",
                table: "Product",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_RailwayCompany_CountryId",
                table: "RailwayCompany",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_StockStatus_ProductId",
                table: "StockStatus",
                column: "ProductId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageProduct");

            migrationBuilder.DropTable(
                name: "StockStatus");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "RailwayCompany");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
