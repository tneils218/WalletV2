using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace WalletV2.DB.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_account_type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    account_type_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_account_type", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_action",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    action_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_action", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    full_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    dob = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    account_type_id = table.Column<int>(type: "int", nullable: false),
                    activated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_account_tbl_account_type_account_type_id",
                        column: x => x.account_type_id,
                        principalTable: "tbl_account_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_action_fee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    action_fee_account_type = table.Column<int>(type: "int", nullable: false),
                    action_fee_action_type = table.Column<int>(type: "int", nullable: false),
                    fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_action_fee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_action_fee_tbl_account_type_action_fee_account_type",
                        column: x => x.action_fee_account_type,
                        principalTable: "tbl_account_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_action_fee_tbl_action_action_fee_action_type",
                        column: x => x.action_fee_action_type,
                        principalTable: "tbl_action",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_wallet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    wallet_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    wallet_updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    account_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_wallet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_wallet_tbl_account_account_id",
                        column: x => x.account_id,
                        principalTable: "tbl_account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_wallet_history",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    wallet_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    wallet_account_type = table.Column<int>(type: "int", nullable: false),
                    wallet_action_type = table.Column<int>(type: "int", nullable: false),
                    source_wallet_id = table.Column<int>(type: "int", nullable: true),
                    destination_wallet_id = table.Column<int>(type: "int", nullable: true),
                    wallet_fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    wallet_created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_wallet_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_wallet_history_tbl_account_type_wallet_account_type",
                        column: x => x.wallet_account_type,
                        principalTable: "tbl_account_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_wallet_history_tbl_action_wallet_action_type",
                        column: x => x.wallet_action_type,
                        principalTable: "tbl_action",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_wallet_history_tbl_wallet_WalletId",
                        column: x => x.WalletId,
                        principalTable: "tbl_wallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "tbl_account_type",
                columns: new[] { "Id", "account_type_name" },
                values: new object[,]
                {
                    { 1, "normal" },
                    { 2, "premium" },
                    { 3, "vip" }
                });

            migrationBuilder.InsertData(
                table: "tbl_action",
                columns: new[] { "Id", "action_name" },
                values: new object[,]
                {
                    { 1, "Add money" },
                    { 2, "Transfer money" },
                    { 3, "Withdraw money" },
                    { 4, "Receive money" }
                });

            migrationBuilder.InsertData(
                table: "tbl_action_fee",
                columns: new[] { "Id", "action_fee_account_type", "action_fee_action_type", "fee" },
                values: new object[,]
                {
                    { 1, 1, 1, 0m },
                    { 2, 2, 1, 0m },
                    { 3, 3, 1, 0m },
                    { 4, 1, 2, 30m },
                    { 5, 2, 2, 20m },
                    { 6, 3, 2, 1m },
                    { 7, 1, 3, 25m },
                    { 8, 2, 3, 15m },
                    { 9, 3, 3, 5m },
                    { 10, 1, 4, 0m },
                    { 11, 2, 4, 0m },
                    { 12, 3, 4, 0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_account_account_type_id",
                table: "tbl_account",
                column: "account_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_account_email",
                table: "tbl_account",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_account_user_name",
                table: "tbl_account",
                column: "user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_action_fee_action_fee_account_type",
                table: "tbl_action_fee",
                column: "action_fee_account_type");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_action_fee_action_fee_action_type",
                table: "tbl_action_fee",
                column: "action_fee_action_type");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_wallet_account_id",
                table: "tbl_wallet",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_wallet_history_wallet_account_type",
                table: "tbl_wallet_history",
                column: "wallet_account_type");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_wallet_history_wallet_action_type",
                table: "tbl_wallet_history",
                column: "wallet_action_type");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_wallet_history_WalletId",
                table: "tbl_wallet_history",
                column: "WalletId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_action_fee");

            migrationBuilder.DropTable(
                name: "tbl_wallet_history");

            migrationBuilder.DropTable(
                name: "tbl_action");

            migrationBuilder.DropTable(
                name: "tbl_wallet");

            migrationBuilder.DropTable(
                name: "tbl_account");

            migrationBuilder.DropTable(
                name: "tbl_account_type");
        }
    }
}
