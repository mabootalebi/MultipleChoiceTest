using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _140302212 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnsweredTests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    TotalScore = table.Column<int>(type: "int", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnsweredTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnsweredTests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AnsweredTests_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AnsweredTestDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnsweredTestId = table.Column<long>(type: "bigint", nullable: false),
                    ChoiceId = table.Column<long>(type: "bigint", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnsweredTestDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnsweredTestDetail_AnsweredTests_AnsweredTestId",
                        column: x => x.AnsweredTestId,
                        principalTable: "AnsweredTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AnsweredTestDetail_Choices_ChoiceId",
                        column: x => x.ChoiceId,
                        principalTable: "Choices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnsweredTestDetail_AnsweredTestId",
                table: "AnsweredTestDetail",
                column: "AnsweredTestId");

            migrationBuilder.CreateIndex(
                name: "IX_AnsweredTestDetail_ChoiceId",
                table: "AnsweredTestDetail",
                column: "ChoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AnsweredTests_TestId",
                table: "AnsweredTests",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_AnsweredTests_UserId",
                table: "AnsweredTests",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnsweredTestDetail");

            migrationBuilder.DropTable(
                name: "AnsweredTests");
        }
    }
}
