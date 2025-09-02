using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContasBancariasAspNet.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_account",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "varchar(20)", nullable: false),
                    Agency = table.Column<string>(type: "varchar(10)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    additional_limit = table.Column<decimal>(type: "decimal(13,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_card",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "varchar(16)", nullable: false),
                    available_limit = table.Column<decimal>(type: "decimal(13,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_card", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_user",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: true),
                    CardId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_user_tb_account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "tb_account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_user_tb_card_CardId",
                        column: x => x.CardId,
                        principalTable: "tb_card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_account_Number",
                table: "tb_account",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_card_Number",
                table: "tb_card",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_user_AccountId",
                table: "tb_user",
                column: "AccountId",
                unique: true,
                filter: "[AccountId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tb_user_CardId",
                table: "tb_user",
                column: "CardId",
                unique: true,
                filter: "[CardId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_user");

            migrationBuilder.DropTable(
                name: "tb_account");

            migrationBuilder.DropTable(
                name: "tb_card");
        }
    }
}
