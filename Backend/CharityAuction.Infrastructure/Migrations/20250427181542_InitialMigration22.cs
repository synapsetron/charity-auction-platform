using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CharityAuction.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("22f6abea-b40c-48bd-9647-762232dc0692"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("27aeceb2-eef4-4a22-9337-2a189ee7c35e"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("28583864-22d4-4617-a9ab-c59856345a0d"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("4b602511-1b4b-4cdd-b63b-5cf9f5ec3328"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("50dba62c-df02-43b7-8a9a-0c61a460ebd1"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("5423bf64-12da-40ad-8fb6-fdb6d09e5620"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("6171fc96-97f0-44cd-bafa-183afccf0e34"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("62e5a135-158d-4f64-b127-1beb097db4e2"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("680f1b31-51f9-44d1-ae9c-441a7a52ec5d"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("6a9191ee-fb70-4b57-a543-b5e92c1104d0"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("9d9d0ca9-3c6e-4073-94ca-744b094437e4"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("d8d29709-1441-48b9-9fe2-01fc527d32d4"));

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a8da342-5a30-4c2a-a9f2-77bb6659c25f",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "ed2bfa29-a4da-4f29-b965-23bd796b4446", new DateTime(2024, 10, 27, 18, 15, 41, 584, DateTimeKind.Utc).AddTicks(4369), "AQAAAAIAAYagAAAAEBRrH+4Rk/gdZcel0NjcabKAjKncfdIrcdpjeJV2BsfnjeRB77Ktm3Yjhk0g+BUhDA==", "f41baeff-4b4a-46cb-b7ec-e7828f655faf", new DateTime(2025, 4, 27, 18, 15, 41, 584, DateTimeKind.Utc).AddTicks(4369) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62a7c1cd-e93a-4aab-a8a4-64e22210c77f",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "69d21322-b138-4774-a32c-a65f9912f3f3", new DateTime(2024, 10, 27, 18, 15, 41, 584, DateTimeKind.Utc).AddTicks(4369), "AQAAAAIAAYagAAAAECAOiT0MUWTcoe+9DQSGBw3yNgQHaNxqdnazw3oiMtcAUNVocXyH1AWOuQI9cTNzBA==", "b5c8346d-18a4-46a5-b965-5f8539e4b67f", new DateTime(2025, 4, 27, 18, 15, 41, 584, DateTimeKind.Utc).AddTicks(4369) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b26038d-1a43-4248-90e1-dc7f0381d7fa",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "038eba06-8ad9-43e4-952e-2ec6e4cea87c", new DateTime(2024, 10, 27, 18, 15, 41, 584, DateTimeKind.Utc).AddTicks(4369), "AQAAAAIAAYagAAAAEE7aCRMdEuFF4vfOaw50upjlx/zuPo51s46ZQejMRlJLWx7lNE73qkQ7A+g7sFiXsQ==", "1444e898-a620-4951-bf28-a5b8e7b0adc7", new DateTime(2025, 4, 27, 18, 15, 41, 584, DateTimeKind.Utc).AddTicks(4369) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "dad0736c-8fc5-46f6-9e9a-7af24e9af913", new DateTime(2024, 10, 27, 18, 15, 41, 584, DateTimeKind.Utc).AddTicks(4369), "AQAAAAIAAYagAAAAEKKzm+5Zx8Fb/K59W7F3uS9rIVf3ZU2crFVHjvuaNbQ1XBe3eWsiiw+qRXTvD3QcHw==", "7ce516e4-aafe-4093-b62c-83fd39361c88", new DateTime(2025, 4, 27, 18, 15, 41, 584, DateTimeKind.Utc).AddTicks(4369) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e034755d-65a9-4f2b-a661-556edac6a6b0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "937826d5-d82a-41b0-992b-f08968bcd69e", new DateTime(2024, 10, 27, 18, 15, 41, 584, DateTimeKind.Utc).AddTicks(4369), "AQAAAAIAAYagAAAAED/VZU5bGyrx9YU4Dbc2jfXeAr+IS6XK0yE6pdaINtKMhHetTWTGjBPiNBRYvjB1lQ==", "ceb5cca6-207a-49e0-b9f6-595cc6ff4579", new DateTime(2025, 4, 27, 18, 15, 41, 584, DateTimeKind.Utc).AddTicks(4369) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 27, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478), new DateTime(2025, 5, 4, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478), new DateTime(2025, 4, 27, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("6309e613-c9d2-4bbc-b0b6-546d82d27be5"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 27, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478), new DateTime(2025, 5, 4, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478), new DateTime(2025, 4, 27, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("6824a88e-6cc6-4708-99c7-77d96bc4828f"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 27, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478), new DateTime(2025, 5, 4, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478), new DateTime(2025, 4, 27, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("803387b4-afb3-4fcb-840a-9dd9f63fc841"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 27, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478), new DateTime(2025, 5, 4, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478), new DateTime(2025, 4, 27, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("9f74c4e9-8818-41cf-8520-b055ad40d3f1"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 27, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478), new DateTime(2025, 5, 4, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478), new DateTime(2025, 4, 27, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("bd9ebb2b-c661-4801-a15d-1603e988039a"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 27, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478), new DateTime(2025, 5, 4, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478), new DateTime(2025, 4, 27, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("cc5644bb-3762-4a4c-8842-a9fc93f5c73a"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 27, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478), new DateTime(2025, 5, 4, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478), new DateTime(2025, 4, 27, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("d09c2220-669a-401b-8f6a-4f4f114329df"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 27, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478), new DateTime(2025, 5, 4, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478), new DateTime(2025, 4, 27, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 27, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478), new DateTime(2025, 5, 4, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478), new DateTime(2025, 4, 27, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("fe807746-bf07-4157-aaf4-f157ff3c6f2a"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 27, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478), new DateTime(2025, 5, 4, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478), new DateTime(2025, 4, 27, 18, 15, 41, 817, DateTimeKind.Utc).AddTicks(3478) });

            migrationBuilder.InsertData(
                table: "Bids",
                columns: new[] { "Id", "Amount", "AuctionId", "CreatedAt", "IsDonated", "UserId" },
                values: new object[,]
                {
                    { new Guid("0d8e6730-dc0e-47dd-9142-5a7819766f91"), 7100m, new Guid("6309e613-c9d2-4bbc-b0b6-546d82d27be5"), new DateTime(2025, 4, 27, 9, 15, 41, 817, DateTimeKind.Utc).AddTicks(3561), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("852ea2a8-a5db-47e2-b982-36e50e92c1ca"), 1700m, new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"), new DateTime(2025, 4, 27, 10, 15, 41, 817, DateTimeKind.Utc).AddTicks(3561), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("8adce36b-fa54-47e2-9bd7-7770a92dd325"), 550m, new Guid("803387b4-afb3-4fcb-840a-9dd9f63fc841"), new DateTime(2025, 4, 27, 15, 15, 41, 817, DateTimeKind.Utc).AddTicks(3561), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("9afc98f5-9adc-4844-9f12-1174b91d155c"), 450m, new Guid("fe807746-bf07-4157-aaf4-f157ff3c6f2a"), new DateTime(2025, 4, 27, 14, 15, 41, 817, DateTimeKind.Utc).AddTicks(3561), false, "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { new Guid("a7a836a9-8888-4275-b511-5ee021440821"), 210m, new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"), new DateTime(2025, 4, 27, 11, 15, 41, 817, DateTimeKind.Utc).AddTicks(3561), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("b4e05f82-0dbb-4638-a058-156f0391fc3c"), 10500m, new Guid("bd9ebb2b-c661-4801-a15d-1603e988039a"), new DateTime(2025, 4, 27, 17, 45, 41, 817, DateTimeKind.Utc).AddTicks(3561), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("bc8b552d-fb3b-4b62-82e4-8ba9ea2dcb56"), 700m, new Guid("6824a88e-6cc6-4708-99c7-77d96bc4828f"), new DateTime(2025, 4, 27, 17, 15, 41, 817, DateTimeKind.Utc).AddTicks(3561), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("ce5ea704-50a5-487f-821f-0f55497b09b5"), 500m, new Guid("cc5644bb-3762-4a4c-8842-a9fc93f5c73a"), new DateTime(2025, 4, 27, 17, 25, 41, 817, DateTimeKind.Utc).AddTicks(3561), false, "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { new Guid("d8d95f89-7d5f-4161-9911-7313758d6796"), 850m, new Guid("d09c2220-669a-401b-8f6a-4f4f114329df"), new DateTime(2025, 4, 27, 12, 15, 41, 817, DateTimeKind.Utc).AddTicks(3561), false, "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { new Guid("ea8b0b61-6a1f-4f89-8b3e-ccaed2df9f9b"), 230m, new Guid("9f74c4e9-8818-41cf-8520-b055ad40d3f1"), new DateTime(2025, 4, 27, 16, 15, 41, 817, DateTimeKind.Utc).AddTicks(3561), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("ebf1feff-cba9-4cc8-8d91-435048a0fa9f"), 1600m, new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"), new DateTime(2025, 4, 27, 8, 15, 41, 817, DateTimeKind.Utc).AddTicks(3561), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("f17029c4-bf13-4eca-9f36-77f089c6b5b9"), 220m, new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"), new DateTime(2025, 4, 27, 13, 15, 41, 817, DateTimeKind.Utc).AddTicks(3561), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("0d8e6730-dc0e-47dd-9142-5a7819766f91"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("852ea2a8-a5db-47e2-b982-36e50e92c1ca"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("8adce36b-fa54-47e2-9bd7-7770a92dd325"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("9afc98f5-9adc-4844-9f12-1174b91d155c"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("a7a836a9-8888-4275-b511-5ee021440821"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("b4e05f82-0dbb-4638-a058-156f0391fc3c"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("bc8b552d-fb3b-4b62-82e4-8ba9ea2dcb56"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("ce5ea704-50a5-487f-821f-0f55497b09b5"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("d8d95f89-7d5f-4161-9911-7313758d6796"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("ea8b0b61-6a1f-4f89-8b3e-ccaed2df9f9b"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("ebf1feff-cba9-4cc8-8d91-435048a0fa9f"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("f17029c4-bf13-4eca-9f36-77f089c6b5b9"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a8da342-5a30-4c2a-a9f2-77bb6659c25f",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "19f71399-ee89-4e72-b2bb-0e570d214820", new DateTime(2024, 10, 27, 15, 20, 6, 169, DateTimeKind.Utc).AddTicks(6292), "AQAAAAIAAYagAAAAENOcpTF/GRVEy0hl9SgV+bUor7m/RDjVbF6vfOhX3x7mxMRNIIuBAoFk2qM0yTHuyg==", "19360aa0-566a-4c2f-930a-1bfe4abd24ce", new DateTime(2025, 4, 27, 15, 20, 6, 169, DateTimeKind.Utc).AddTicks(6292) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62a7c1cd-e93a-4aab-a8a4-64e22210c77f",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "fba89c8a-15ae-458e-bf50-0b6b815492ca", new DateTime(2024, 10, 27, 15, 20, 6, 169, DateTimeKind.Utc).AddTicks(6292), "AQAAAAIAAYagAAAAECVwj6ZH2Nmy69ExtnJCEZIDnggiYW8HiTCi5bZevtztF7IRcMMnd7cfyDYyldCp2w==", "fff2c829-05c7-4d81-b97b-db104db43810", new DateTime(2025, 4, 27, 15, 20, 6, 169, DateTimeKind.Utc).AddTicks(6292) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b26038d-1a43-4248-90e1-dc7f0381d7fa",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "6c6fd802-4e25-499c-8d1e-b3ffde424e66", new DateTime(2024, 10, 27, 15, 20, 6, 169, DateTimeKind.Utc).AddTicks(6292), "AQAAAAIAAYagAAAAEB6UdCDpLDdPc6othGYImPmiAQii2XpHiEA3UZdPvdLItymyhpg5zpcG1JOnQRcDQg==", "902d000b-87fa-418c-8e4b-41fa901219fd", new DateTime(2025, 4, 27, 15, 20, 6, 169, DateTimeKind.Utc).AddTicks(6292) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "e228ed23-74a9-460b-a16e-714a9b038e64", new DateTime(2024, 10, 27, 15, 20, 6, 169, DateTimeKind.Utc).AddTicks(6292), "AQAAAAIAAYagAAAAENV89nU8bt+Y211iG4h+36Rm7OvinSW+2cx+8eIMcjzQ3COK6MMsdL08wsj2FP29GA==", "746bfa21-44ad-4b5c-a883-3f6f564a6ec3", new DateTime(2025, 4, 27, 15, 20, 6, 169, DateTimeKind.Utc).AddTicks(6292) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e034755d-65a9-4f2b-a661-556edac6a6b0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "16cdc607-d7fe-45ff-b96f-ca98a5d48941", new DateTime(2024, 10, 27, 15, 20, 6, 169, DateTimeKind.Utc).AddTicks(6292), "AQAAAAIAAYagAAAAEH9RkDazc2Zyj3SOhSFTTSPbpdE8mzMKeGDzuINEm/1YfgGwbhO7XT6KmM0zIy3O0A==", "b88d795c-f492-4935-83d7-a6c4f61640b7", new DateTime(2025, 4, 27, 15, 20, 6, 169, DateTimeKind.Utc).AddTicks(6292) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 27, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 5, 4, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 4, 27, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("6309e613-c9d2-4bbc-b0b6-546d82d27be5"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 27, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 5, 4, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 4, 27, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("6824a88e-6cc6-4708-99c7-77d96bc4828f"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 27, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 5, 4, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 4, 27, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("803387b4-afb3-4fcb-840a-9dd9f63fc841"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 27, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 5, 4, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 4, 27, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("9f74c4e9-8818-41cf-8520-b055ad40d3f1"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 27, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 5, 4, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 4, 27, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("bd9ebb2b-c661-4801-a15d-1603e988039a"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 27, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 5, 4, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 4, 27, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("cc5644bb-3762-4a4c-8842-a9fc93f5c73a"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 27, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 5, 4, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 4, 27, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("d09c2220-669a-401b-8f6a-4f4f114329df"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 27, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 5, 4, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 4, 27, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 27, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 5, 4, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 4, 27, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("fe807746-bf07-4157-aaf4-f157ff3c6f2a"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 27, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 5, 4, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 4, 27, 15, 20, 6, 409, DateTimeKind.Utc).AddTicks(4200) });

            migrationBuilder.InsertData(
                table: "Bids",
                columns: new[] { "Id", "Amount", "AuctionId", "CreatedAt", "IsDonated", "UserId" },
                values: new object[,]
                {
                    { new Guid("22f6abea-b40c-48bd-9647-762232dc0692"), 1700m, new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"), new DateTime(2025, 4, 27, 7, 20, 6, 409, DateTimeKind.Utc).AddTicks(4306), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("27aeceb2-eef4-4a22-9337-2a189ee7c35e"), 220m, new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"), new DateTime(2025, 4, 27, 10, 20, 6, 409, DateTimeKind.Utc).AddTicks(4306), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("28583864-22d4-4617-a9ab-c59856345a0d"), 550m, new Guid("803387b4-afb3-4fcb-840a-9dd9f63fc841"), new DateTime(2025, 4, 27, 12, 20, 6, 409, DateTimeKind.Utc).AddTicks(4306), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("4b602511-1b4b-4cdd-b63b-5cf9f5ec3328"), 450m, new Guid("fe807746-bf07-4157-aaf4-f157ff3c6f2a"), new DateTime(2025, 4, 27, 11, 20, 6, 409, DateTimeKind.Utc).AddTicks(4306), false, "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { new Guid("50dba62c-df02-43b7-8a9a-0c61a460ebd1"), 850m, new Guid("d09c2220-669a-401b-8f6a-4f4f114329df"), new DateTime(2025, 4, 27, 9, 20, 6, 409, DateTimeKind.Utc).AddTicks(4306), false, "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { new Guid("5423bf64-12da-40ad-8fb6-fdb6d09e5620"), 210m, new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"), new DateTime(2025, 4, 27, 8, 20, 6, 409, DateTimeKind.Utc).AddTicks(4306), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("6171fc96-97f0-44cd-bafa-183afccf0e34"), 7100m, new Guid("6309e613-c9d2-4bbc-b0b6-546d82d27be5"), new DateTime(2025, 4, 27, 6, 20, 6, 409, DateTimeKind.Utc).AddTicks(4306), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("62e5a135-158d-4f64-b127-1beb097db4e2"), 230m, new Guid("9f74c4e9-8818-41cf-8520-b055ad40d3f1"), new DateTime(2025, 4, 27, 13, 20, 6, 409, DateTimeKind.Utc).AddTicks(4306), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("680f1b31-51f9-44d1-ae9c-441a7a52ec5d"), 1600m, new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"), new DateTime(2025, 4, 27, 5, 20, 6, 409, DateTimeKind.Utc).AddTicks(4306), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("6a9191ee-fb70-4b57-a543-b5e92c1104d0"), 700m, new Guid("6824a88e-6cc6-4708-99c7-77d96bc4828f"), new DateTime(2025, 4, 27, 14, 20, 6, 409, DateTimeKind.Utc).AddTicks(4306), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("9d9d0ca9-3c6e-4073-94ca-744b094437e4"), 500m, new Guid("cc5644bb-3762-4a4c-8842-a9fc93f5c73a"), new DateTime(2025, 4, 27, 14, 30, 6, 409, DateTimeKind.Utc).AddTicks(4306), false, "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { new Guid("d8d29709-1441-48b9-9fe2-01fc527d32d4"), 10500m, new Guid("bd9ebb2b-c661-4801-a15d-1603e988039a"), new DateTime(2025, 4, 27, 14, 50, 6, 409, DateTimeKind.Utc).AddTicks(4306), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" }
                });
        }
    }
}
