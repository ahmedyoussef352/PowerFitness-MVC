using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectGym.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "Ck_Session_Start_End",
                table: "Sessions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Sessions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Sessions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddCheckConstraint(
                name: "Ck_Session_Start_End",
                table: "Sessions",
                sql: "[EndTime] > [StartTime]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "Ck_Session_Start_End",
                table: "Sessions");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartTime",
                table: "Sessions",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndTime",
                table: "Sessions",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddCheckConstraint(
                name: "Ck_Session_Start_End",
                table: "Sessions",
                sql: "[EndTime] > [StartTime]");
        }
    }
}