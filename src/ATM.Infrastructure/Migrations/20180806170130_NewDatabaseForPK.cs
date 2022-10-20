using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ATM.Infrastructure.Migrations
{
    public partial class NewDatabaseForPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Down(migrationBuilder);

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    AccountNumber = table.Column<string>(nullable: false),
                    AccountName = table.Column<string>(nullable: true),
                    Balance = table.Column<decimal>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.AccountNumber);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<string>(nullable: true),
                    Recipient = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Transactions_BankAccounts_AccountNumber",
                        column: x => x.AccountNumber,
                        principalTable: "BankAccounts",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountNumber",
                table: "Transactions",
                column: "AccountNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "BankAccounts");
        }
    }
}