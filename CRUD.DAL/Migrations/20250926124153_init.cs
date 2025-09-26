using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CRUD.DAL.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FooFoos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FooFoos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Foos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    FooFooId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foos_FooFoos_FooFooId",
                        column: x => x.FooFooId,
                        principalTable: "FooFoos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FooFoos",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 2L, 2L }
                });

            migrationBuilder.InsertData(
                table: "Foos",
                columns: new[] { "Id", "FooFooId", "IsDeleted", "LastModifiedDate", "Title" },
                values: new object[,]
                {
                    { -20L, 2L, false, new DateTimeOffset(new DateTime(2025, 9, 26, 12, 41, 53, 158, DateTimeKind.Unspecified).AddTicks(7008), new TimeSpan(0, 0, 0, 0, 0)), "Foo 20" },
                    { -19L, 1L, false, new DateTimeOffset(new DateTime(2025, 9, 26, 12, 41, 53, 158, DateTimeKind.Unspecified).AddTicks(7007), new TimeSpan(0, 0, 0, 0, 0)), "Foo 19" },
                    { -18L, 1L, false, new DateTimeOffset(new DateTime(2025, 9, 26, 12, 41, 53, 158, DateTimeKind.Unspecified).AddTicks(7006), new TimeSpan(0, 0, 0, 0, 0)), "Foo 18" },
                    { -17L, 1L, false, new DateTimeOffset(new DateTime(2025, 9, 26, 12, 41, 53, 158, DateTimeKind.Unspecified).AddTicks(7005), new TimeSpan(0, 0, 0, 0, 0)), "Foo 17" },
                    { -16L, 1L, false, new DateTimeOffset(new DateTime(2025, 9, 26, 12, 41, 53, 158, DateTimeKind.Unspecified).AddTicks(7004), new TimeSpan(0, 0, 0, 0, 0)), "Foo 16" },
                    { -15L, 1L, false, new DateTimeOffset(new DateTime(2025, 9, 26, 12, 41, 53, 158, DateTimeKind.Unspecified).AddTicks(7003), new TimeSpan(0, 0, 0, 0, 0)), "Foo 15" },
                    { -14L, 1L, false, new DateTimeOffset(new DateTime(2025, 9, 26, 12, 41, 53, 158, DateTimeKind.Unspecified).AddTicks(7002), new TimeSpan(0, 0, 0, 0, 0)), "Foo 14" },
                    { -13L, 1L, false, new DateTimeOffset(new DateTime(2025, 9, 26, 12, 41, 53, 158, DateTimeKind.Unspecified).AddTicks(7001), new TimeSpan(0, 0, 0, 0, 0)), "Foo 13" },
                    { -12L, 1L, false, new DateTimeOffset(new DateTime(2025, 9, 26, 12, 41, 53, 158, DateTimeKind.Unspecified).AddTicks(7000), new TimeSpan(0, 0, 0, 0, 0)), "Foo 12" },
                    { -11L, 1L, false, new DateTimeOffset(new DateTime(2025, 9, 26, 12, 41, 53, 158, DateTimeKind.Unspecified).AddTicks(6999), new TimeSpan(0, 0, 0, 0, 0)), "Foo 11" },
                    { -10L, 1L, false, new DateTimeOffset(new DateTime(2025, 9, 26, 12, 41, 53, 158, DateTimeKind.Unspecified).AddTicks(6996), new TimeSpan(0, 0, 0, 0, 0)), "Foo 10" },
                    { -9L, 1L, false, new DateTimeOffset(new DateTime(2025, 9, 26, 12, 41, 53, 158, DateTimeKind.Unspecified).AddTicks(6995), new TimeSpan(0, 0, 0, 0, 0)), "Foo 9" },
                    { -8L, 1L, false, new DateTimeOffset(new DateTime(2025, 9, 26, 12, 41, 53, 158, DateTimeKind.Unspecified).AddTicks(6994), new TimeSpan(0, 0, 0, 0, 0)), "Foo 8" },
                    { -7L, 1L, false, new DateTimeOffset(new DateTime(2025, 9, 26, 12, 41, 53, 158, DateTimeKind.Unspecified).AddTicks(6992), new TimeSpan(0, 0, 0, 0, 0)), "Foo 7" },
                    { -6L, 1L, false, new DateTimeOffset(new DateTime(2025, 9, 26, 12, 41, 53, 158, DateTimeKind.Unspecified).AddTicks(6991), new TimeSpan(0, 0, 0, 0, 0)), "Foo 6" },
                    { -5L, 1L, false, new DateTimeOffset(new DateTime(2025, 9, 26, 12, 41, 53, 158, DateTimeKind.Unspecified).AddTicks(6990), new TimeSpan(0, 0, 0, 0, 0)), "Foo 5" },
                    { -4L, 1L, false, new DateTimeOffset(new DateTime(2025, 9, 26, 12, 41, 53, 158, DateTimeKind.Unspecified).AddTicks(6948), new TimeSpan(0, 0, 0, 0, 0)), "Foo 4" },
                    { -3L, 1L, false, new DateTimeOffset(new DateTime(2025, 9, 26, 12, 41, 53, 158, DateTimeKind.Unspecified).AddTicks(6947), new TimeSpan(0, 0, 0, 0, 0)), "Foo 3" },
                    { -2L, 1L, false, new DateTimeOffset(new DateTime(2025, 9, 26, 12, 41, 53, 158, DateTimeKind.Unspecified).AddTicks(6940), new TimeSpan(0, 0, 0, 0, 0)), "Foo 2" },
                    { -1L, 1L, false, new DateTimeOffset(new DateTime(2025, 9, 26, 12, 41, 53, 158, DateTimeKind.Unspecified).AddTicks(6140), new TimeSpan(0, 0, 0, 0, 0)), "Foo 1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Foos_FooFooId",
                table: "Foos",
                column: "FooFooId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Foos");

            migrationBuilder.DropTable(
                name: "FooFoos");
        }
    }
}
