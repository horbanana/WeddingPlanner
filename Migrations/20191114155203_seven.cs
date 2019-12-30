using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingPlanner.Migrations
{
    public partial class seven : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Occasion_Plans_PlanId",
                table: "Occasion");

            migrationBuilder.DropForeignKey(
                name: "FK_Occasion_Users_UserId",
                table: "Occasion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Occasion",
                table: "Occasion");

            migrationBuilder.RenameTable(
                name: "Occasion",
                newName: "Occasions");

            migrationBuilder.RenameIndex(
                name: "IX_Occasion_UserId",
                table: "Occasions",
                newName: "IX_Occasions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Occasion_PlanId",
                table: "Occasions",
                newName: "IX_Occasions_PlanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Occasions",
                table: "Occasions",
                column: "OccasionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Occasions_Plans_PlanId",
                table: "Occasions",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "PlanId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Occasions_Users_UserId",
                table: "Occasions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Occasions_Plans_PlanId",
                table: "Occasions");

            migrationBuilder.DropForeignKey(
                name: "FK_Occasions_Users_UserId",
                table: "Occasions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Occasions",
                table: "Occasions");

            migrationBuilder.RenameTable(
                name: "Occasions",
                newName: "Occasion");

            migrationBuilder.RenameIndex(
                name: "IX_Occasions_UserId",
                table: "Occasion",
                newName: "IX_Occasion_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Occasions_PlanId",
                table: "Occasion",
                newName: "IX_Occasion_PlanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Occasion",
                table: "Occasion",
                column: "OccasionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Occasion_Plans_PlanId",
                table: "Occasion",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "PlanId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Occasion_Users_UserId",
                table: "Occasion",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
