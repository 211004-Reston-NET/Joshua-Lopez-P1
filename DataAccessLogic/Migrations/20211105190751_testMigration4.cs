using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLogic.Migrations
{
    public partial class testMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CurrentCurrency = table.Column<decimal>(type: "money", nullable: false, defaultValueSql: "((0.00))"),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    UserName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(16,2)", nullable: false, defaultValueSql: "((0.00))"),
                    Description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Category = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "StoreFront",
                columns: table => new
                {
                    StoreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Location = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StoreFro__3B82F0E1A4CC44F6", x => x.StoreID);
                });

            migrationBuilder.CreateTable(
                name: "OrdersRecords",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<decimal>(type: "money", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    StoreID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrdersRe__C3905BCF362EFFB7", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK__OrdersRec__Custo__160F4887",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__OrdersRec__Store__17036CC0",
                        column: x => x.StoreID,
                        principalTable: "StoreFront",
                        principalColumn: "StoreID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    StoreID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    ProductEstablishId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Stock__F0C23C8FFE8CD921", x => new { x.StoreID, x.ProductID });
                    table.ForeignKey(
                        name: "FK__Stock__ProductID__114A936A",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Stock__StoreID__10566F31",
                        column: x => x.StoreID,
                        principalTable: "StoreFront",
                        principalColumn: "StoreID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stock_Products_ProductEstablishId",
                        column: x => x.ProductEstablishId,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderHistory",
                columns: table => new
                {
                    ReferenceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    StoreID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    line_quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderHis__E1A99A79D614F7ED", x => x.ReferenceID);
                    table.ForeignKey(
                        name: "FK__OrderHist__Custo__1AD3FDA4",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__OrderHist__Order__19DFD96B",
                        column: x => x.OrderId,
                        principalTable: "OrdersRecords",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__OrderHist__Produ__1CBC4616",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__OrderHist__Store__1BC821DD",
                        column: x => x.StoreID,
                        principalTable: "StoreFront",
                        principalColumn: "StoreID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "Customer_UN",
                table: "Customer",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_CustomerID",
                table: "OrderHistory",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_OrderId",
                table: "OrderHistory",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_ProductID",
                table: "OrderHistory",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_StoreID",
                table: "OrderHistory",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersRecords_CustomerID",
                table: "OrdersRecords",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersRecords_StoreID",
                table: "OrdersRecords",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ProductEstablishId",
                table: "Stock",
                column: "ProductEstablishId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ProductID",
                table: "Stock",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderHistory");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "OrdersRecords");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "StoreFront");
        }
    }
}
