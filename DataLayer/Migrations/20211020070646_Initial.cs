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
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RailwayCompany",
                columns: table => new
                {
                    RailwayCompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RailwayCompany", x => x.RailwayCompanyId);
                    table.ForeignKey(
                        name: "FK_RailwayCompany_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiteUsers",
                columns: table => new
                {
                    SiteUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteUsers", x => x.SiteUserId);
                    table.ForeignKey(
                        name: "FK_SiteUsers_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "AddressId",
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
                    AmountInStock = table.Column<long>(type: "bigint", nullable: false),
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
                    RailwayCompanyId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_Product_RailwayCompany_RailwayCompanyId",
                        column: x => x.RailwayCompanyId,
                        principalTable: "RailwayCompany",
                        principalColumn: "RailwayCompanyId",
                        onDelete: ReferentialAction.Restrict);
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
                columns: new[] { "ImageId", "Path" },
                values: new object[,]
                {
                    { 1, "" },
                    { 2, "" },
                    { 3, "" },
                    { 4, "" },
                    { 5, "" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "AmountInStock", "Description", "Discriminator", "Interface", "Name", "Price", "Sound", "TagId" },
                values: new object[] { 1, 23L, "Suitable for Gauge H0. The decoder is equipped with the RailCom® function. Maximum motor current: 1.2 A.", "DigitalDecoder", 0, "PluX22 sound decoder (NEM 658)", 92.4m, true, null });

            migrationBuilder.InsertData(
                table: "SiteUsers",
                columns: new[] { "SiteUserId", "AddressId", "Email", "FirstName", "IsAdmin", "LastName", "Password", "UserName" },
                values: new object[] { 1, null, null, null, true, null, "admin", "admin" });

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
                table: "Product",
                columns: new[] { "ProductId", "AmountInStock", "AutoCoupling", "Control", "Description", "DigitalDecoderId", "Discriminator", "Epoch", "Length", "LocoType", "Name", "NumOfAxels", "NumOfDrivenAxels", "Price", "RailwayCompanyId", "Scale", "TagId" },
                values: new object[] { 2, 5L, false, 2, "The 023 series was a true all-round genius. The locomotive hauled commuter trains, fast and express trains. Sometimes they hauled even freight trains. The newly designed locomotive of the class 023 (which until 1968 was designated class 23) was being used even in the epoch IV. On Dec. 31 1971, 76 locomotives were a permanent part of the rolling stock of the DB and without exception they were stationed at the three railway depots Saarbrücken, Kaiserslautern and Crailsheim.", null, "Locomotive", 3, 24.5f, 0, "BR 023 040-9", 9, 4, 229.9m, 2, 0, "New" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "AmountInStock", "AutoCoupling", "Control", "Description", "DigitalDecoderId", "Discriminator", "Epoch", "Length", "LocoType", "Name", "NumOfAxels", "NumOfDrivenAxels", "Price", "RailwayCompanyId", "Scale", "TagId" },
                values: new object[] { 4, 1L, false, 2, "In the period between 1942 to 1950, over 7000 units of the class 52 war locomotive were built. These were constructed with as little effort as possible and savings were also made on the material wherever possible. With a weight of 84 tons, the loco achieved an output of 1,192 kW and a top speed of 80 km / h. The Deutsche Bundesbahn mainly got rid of the locomotives as early as 1953 - since it had sufficient machines of the series 50 and series 44 to haul the heavy goods trains. Only a few locomotives built in 1945 remained with the DB until 1962.", null, "Locomotive", 2, 26.5f, 0, "BR 52", 10, 4, 319.9m, 2, 0, "New" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "AmountInStock", "AutoCoupling", "Control", "Description", "DigitalDecoderId", "Discriminator", "Epoch", "Length", "LocoType", "Name", "NumOfAxels", "NumOfDrivenAxels", "Price", "RailwayCompanyId", "Scale", "TagId" },
                values: new object[] { 3, 2L, false, 3, "In 1992, the first locomotive Re 460 of the Swiss Federal Railways rolled out of the factory halls of the companies SLM and BBC in Oerlikon, Switzerland. The locomotive became known to the public as \"Lok 2000\". It stands for fast and modern passenger transport in Switzerland. An eye-catching and particularly aerodynamic design with a large front window, roof cladding and beads on the side wall make the class 460 visually an unbeatable rail vehicle.", 1, "Locomotive", 5, 21.2f, 2, "Re 460 068-0", 4, 4, 321.9m, 4, 0, null });

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

            migrationBuilder.CreateIndex(
                name: "IX_Address_CountryId",
                table: "Address",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageProduct_ProductsProductId",
                table: "ImageProduct",
                column: "ProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DigitalDecoderId",
                table: "Product",
                column: "DigitalDecoderId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_RailwayCompanyId",
                table: "Product",
                column: "RailwayCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_TagId",
                table: "Product",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_RailwayCompany_CountryId",
                table: "RailwayCompany",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteUsers_AddressId",
                table: "SiteUsers",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageProduct");

            migrationBuilder.DropTable(
                name: "SiteUsers");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "RailwayCompany");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
