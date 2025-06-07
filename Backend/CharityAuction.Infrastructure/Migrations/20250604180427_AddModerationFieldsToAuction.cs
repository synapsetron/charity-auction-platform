using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CharityAuction.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddModerationFieldsToAuction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("10817540-6e0a-4fc0-827f-ef833cc00191"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("1497f24f-4ff6-489d-9af1-5dadf12da820"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("19f1c20f-e4f5-4fee-9699-55c22031ac7d"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("252aade8-8a7a-4806-aeb1-8cef6c5d2491"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("327b5805-2ca7-4772-8119-eb30916b2904"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("55f414ee-3faa-40fd-896b-71991cfa40aa"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("5a095e47-6403-4dc6-8e0b-efcafa43a72e"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("9e5a5295-611d-4e1e-aa1b-258775b90444"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("a52ed81c-ce45-448b-a573-97c4b9d5021b"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("ab32ac39-0cd5-4941-b72e-f13987514583"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("c716a094-d304-418d-a09c-a1a23e5adfe3"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("f1b4fea8-1509-49f0-be3f-169fc6f02a95"));

            migrationBuilder.AddColumn<string>(
                name: "FlaggedReason",
                table: "Auctions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFlagged",
                table: "Auctions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a8da342-5a30-4c2a-a9f2-77bb6659c25f",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "273c88da-fedf-4d6a-9132-691abd2aaa42", new DateTime(2024, 12, 4, 18, 4, 26, 700, DateTimeKind.Utc).AddTicks(3752), "AQAAAAIAAYagAAAAEApQob7zdnpLQf4EBIToC8p7g+9DE7STNlw6HgEHrKrFastSZ8qe/aMvI9cl2PrU6Q==", "68eb74f6-f267-48b1-8964-d57ff9434cc8", new DateTime(2025, 6, 4, 18, 4, 26, 700, DateTimeKind.Utc).AddTicks(3752) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62a7c1cd-e93a-4aab-a8a4-64e22210c77f",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "ad093f4d-72cc-4d33-b4b8-3b364a0b8d24", new DateTime(2024, 12, 4, 18, 4, 26, 700, DateTimeKind.Utc).AddTicks(3752), "AQAAAAIAAYagAAAAEBqa1Wvw7wIVBlmvDTxr9HFrimbUigEkNAfFqddCm8BM2/8OYzCsQ03jy73EW2OHxg==", "a3ecd296-b9cd-44f6-9472-34f8ec80a0f9", new DateTime(2025, 6, 4, 18, 4, 26, 700, DateTimeKind.Utc).AddTicks(3752) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b26038d-1a43-4248-90e1-dc7f0381d7fa",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "e44de91e-ab24-49a0-8281-9e97e788b958", new DateTime(2024, 12, 4, 18, 4, 26, 700, DateTimeKind.Utc).AddTicks(3752), "AQAAAAIAAYagAAAAEEBSw+UalSX0VYuDAl/Xypu2vMjd1zMHyH9usuO7ImGwYNIpg363+jE1nMXyYaT2cg==", "5449cc63-73e0-4f30-8357-03cab2541f9e", new DateTime(2025, 6, 4, 18, 4, 26, 700, DateTimeKind.Utc).AddTicks(3752) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "e194a17b-fc0d-493d-9c0f-8fdcfe8f6481", new DateTime(2024, 12, 4, 18, 4, 26, 700, DateTimeKind.Utc).AddTicks(3752), "AQAAAAIAAYagAAAAEPj+OQ9VubUFphSQjvyabUeuAhiT1/GbEI8aP25DwtpFl2GM6ZOvIAwOGwxYMCfJLQ==", "5e391bd5-3942-4910-ab3e-2d789f3969e0", new DateTime(2025, 6, 4, 18, 4, 26, 700, DateTimeKind.Utc).AddTicks(3752) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e034755d-65a9-4f2b-a661-556edac6a6b0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "b2238d9b-9fe5-44ed-89ed-5ecee9d6406c", new DateTime(2024, 12, 4, 18, 4, 26, 700, DateTimeKind.Utc).AddTicks(3752), "AQAAAAIAAYagAAAAEEByCA8kx4bPCVlCNN53jaAa89zJhPNfvB2Upqx+6vn7v3yAJVzQ/nTID8oEz2dbig==", "d366195e-f880-497a-8981-94aeb2d3c19d", new DateTime(2025, 6, 4, 18, 4, 26, 700, DateTimeKind.Utc).AddTicks(3752) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"),
                columns: new[] { "CreatedAt", "EndTime", "FlaggedReason", "IsFlagged", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939), new DateTime(2025, 6, 11, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939), null, false, new DateTime(2025, 6, 4, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("6309e613-c9d2-4bbc-b0b6-546d82d27be5"),
                columns: new[] { "CreatedAt", "EndTime", "FlaggedReason", "IsFlagged", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939), new DateTime(2025, 6, 11, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939), null, false, new DateTime(2025, 6, 4, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("6824a88e-6cc6-4708-99c7-77d96bc4828f"),
                columns: new[] { "CreatedAt", "EndTime", "FlaggedReason", "IsFlagged", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939), new DateTime(2025, 6, 11, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939), null, false, new DateTime(2025, 6, 4, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("803387b4-afb3-4fcb-840a-9dd9f63fc841"),
                columns: new[] { "CreatedAt", "EndTime", "FlaggedReason", "IsFlagged", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939), new DateTime(2025, 6, 11, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939), null, false, new DateTime(2025, 6, 4, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("9f74c4e9-8818-41cf-8520-b055ad40d3f1"),
                columns: new[] { "CreatedAt", "EndTime", "FlaggedReason", "IsFlagged", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939), new DateTime(2025, 6, 11, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939), null, false, new DateTime(2025, 6, 4, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("bd9ebb2b-c661-4801-a15d-1603e988039a"),
                columns: new[] { "CreatedAt", "EndTime", "FlaggedReason", "IsFlagged", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939), new DateTime(2025, 6, 11, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939), null, false, new DateTime(2025, 6, 4, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("cc5644bb-3762-4a4c-8842-a9fc93f5c73a"),
                columns: new[] { "CreatedAt", "EndTime", "FlaggedReason", "IsFlagged", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939), new DateTime(2025, 6, 11, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939), null, false, new DateTime(2025, 6, 4, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("d09c2220-669a-401b-8f6a-4f4f114329df"),
                columns: new[] { "CreatedAt", "EndTime", "FlaggedReason", "IsFlagged", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939), new DateTime(2025, 6, 11, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939), null, false, new DateTime(2025, 6, 4, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"),
                columns: new[] { "CreatedAt", "EndTime", "FlaggedReason", "IsFlagged", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939), new DateTime(2025, 6, 11, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939), null, false, new DateTime(2025, 6, 4, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("fe807746-bf07-4157-aaf4-f157ff3c6f2a"),
                columns: new[] { "CreatedAt", "EndTime", "FlaggedReason", "IsFlagged", "StartTime" },
                values: new object[] { new DateTime(2025, 6, 4, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939), new DateTime(2025, 6, 11, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939), null, false, new DateTime(2025, 6, 4, 18, 4, 26, 929, DateTimeKind.Utc).AddTicks(5939) });

            migrationBuilder.InsertData(
                table: "Bids",
                columns: new[] { "Id", "Amount", "AuctionId", "CreatedAt", "IsDonated", "UserId" },
                values: new object[,]
                {
                    { new Guid("0a714ba8-d85a-41a2-b35e-3125b7666e46"), 7100m, new Guid("6309e613-c9d2-4bbc-b0b6-546d82d27be5"), new DateTime(2025, 6, 4, 9, 4, 26, 929, DateTimeKind.Utc).AddTicks(6033), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("0a9c95cb-5c4a-4205-96b3-4c3433338039"), 500m, new Guid("cc5644bb-3762-4a4c-8842-a9fc93f5c73a"), new DateTime(2025, 6, 4, 17, 14, 26, 929, DateTimeKind.Utc).AddTicks(6033), false, "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { new Guid("0ce49bc6-ee31-401d-a76a-aa5dcad1f3a8"), 850m, new Guid("d09c2220-669a-401b-8f6a-4f4f114329df"), new DateTime(2025, 6, 4, 12, 4, 26, 929, DateTimeKind.Utc).AddTicks(6033), false, "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { new Guid("1c4fe240-b610-4325-9616-3e207c1e5244"), 10500m, new Guid("bd9ebb2b-c661-4801-a15d-1603e988039a"), new DateTime(2025, 6, 4, 17, 34, 26, 929, DateTimeKind.Utc).AddTicks(6033), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("2a6b9f72-bffb-493a-8847-f98e906ab2ea"), 230m, new Guid("9f74c4e9-8818-41cf-8520-b055ad40d3f1"), new DateTime(2025, 6, 4, 16, 4, 26, 929, DateTimeKind.Utc).AddTicks(6033), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("56622d4a-3b74-4c69-9e2c-492718854beb"), 1700m, new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"), new DateTime(2025, 6, 4, 10, 4, 26, 929, DateTimeKind.Utc).AddTicks(6033), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("6624c357-ddde-40b7-ae3f-81ce04991aca"), 450m, new Guid("fe807746-bf07-4157-aaf4-f157ff3c6f2a"), new DateTime(2025, 6, 4, 14, 4, 26, 929, DateTimeKind.Utc).AddTicks(6033), false, "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { new Guid("96012c54-a3ca-45a7-a14c-15f61c6f41fe"), 210m, new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"), new DateTime(2025, 6, 4, 11, 4, 26, 929, DateTimeKind.Utc).AddTicks(6033), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("9ec34e56-05f0-4aa9-8643-de1ab7033f2b"), 700m, new Guid("6824a88e-6cc6-4708-99c7-77d96bc4828f"), new DateTime(2025, 6, 4, 17, 4, 26, 929, DateTimeKind.Utc).AddTicks(6033), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("a910dfe3-c1b6-41ee-97b6-cd179e2cade6"), 220m, new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"), new DateTime(2025, 6, 4, 13, 4, 26, 929, DateTimeKind.Utc).AddTicks(6033), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("d902649b-5e54-41d2-a68c-296af155a48c"), 1600m, new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"), new DateTime(2025, 6, 4, 8, 4, 26, 929, DateTimeKind.Utc).AddTicks(6033), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("f68d69ff-6b16-4025-9601-d0486bfc4ee1"), 550m, new Guid("803387b4-afb3-4fcb-840a-9dd9f63fc841"), new DateTime(2025, 6, 4, 15, 4, 26, 929, DateTimeKind.Utc).AddTicks(6033), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("0a714ba8-d85a-41a2-b35e-3125b7666e46"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("0a9c95cb-5c4a-4205-96b3-4c3433338039"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("0ce49bc6-ee31-401d-a76a-aa5dcad1f3a8"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("1c4fe240-b610-4325-9616-3e207c1e5244"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("2a6b9f72-bffb-493a-8847-f98e906ab2ea"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("56622d4a-3b74-4c69-9e2c-492718854beb"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("6624c357-ddde-40b7-ae3f-81ce04991aca"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("96012c54-a3ca-45a7-a14c-15f61c6f41fe"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("9ec34e56-05f0-4aa9-8643-de1ab7033f2b"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("a910dfe3-c1b6-41ee-97b6-cd179e2cade6"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("d902649b-5e54-41d2-a68c-296af155a48c"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("f68d69ff-6b16-4025-9601-d0486bfc4ee1"));

            migrationBuilder.DropColumn(
                name: "FlaggedReason",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "IsFlagged",
                table: "Auctions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a8da342-5a30-4c2a-a9f2-77bb6659c25f",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "ad136792-fb71-48fd-9799-7cd4470ad161", new DateTime(2024, 11, 1, 10, 1, 56, 67, DateTimeKind.Utc).AddTicks(9892), "AQAAAAIAAYagAAAAEGd8RKyX+lSrL2J7VrqfFyXc3jOGc09XwOYpcP2yE5rsRulbqfZe5D71AnqVWiMS6g==", "40f99b42-f3d2-44c9-a604-c718747ee308", new DateTime(2025, 5, 1, 10, 1, 56, 67, DateTimeKind.Utc).AddTicks(9892) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62a7c1cd-e93a-4aab-a8a4-64e22210c77f",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "6a21a751-e8fc-42a7-bf0d-8af1d0efefb1", new DateTime(2024, 11, 1, 10, 1, 56, 67, DateTimeKind.Utc).AddTicks(9892), "AQAAAAIAAYagAAAAEA3qLDy/Kv+6Oy3ta2zbcvI2KOCW/HcAUeJ4sCNlglyfHU+tmBmv9f4Idf/Gm/dNEQ==", "84473a13-770a-4e8c-917d-766470496dba", new DateTime(2025, 5, 1, 10, 1, 56, 67, DateTimeKind.Utc).AddTicks(9892) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b26038d-1a43-4248-90e1-dc7f0381d7fa",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "d369ca88-ef67-4cdb-b2fc-03f3758972f5", new DateTime(2024, 11, 1, 10, 1, 56, 67, DateTimeKind.Utc).AddTicks(9892), "AQAAAAIAAYagAAAAED2jh5rQQfEthIOxPC3WsVLLvFeyXYA0FkQWEWIztP9C0Gf62yhlK7eCjJnuHoz5Rg==", "c5507851-0c91-494a-b0f5-266ce94a171b", new DateTime(2025, 5, 1, 10, 1, 56, 67, DateTimeKind.Utc).AddTicks(9892) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "892035b7-bf95-4a72-9c37-c5b09b12372b", new DateTime(2024, 11, 1, 10, 1, 56, 67, DateTimeKind.Utc).AddTicks(9892), "AQAAAAIAAYagAAAAEOwZiafdzg4PsMW06EkScyBE+iaNVYiwyHydnpjFbspv4w/+9Nttnz1srvVfI/LvAg==", "18f599fe-15f3-474b-81bc-49e5845154a7", new DateTime(2025, 5, 1, 10, 1, 56, 67, DateTimeKind.Utc).AddTicks(9892) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e034755d-65a9-4f2b-a661-556edac6a6b0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "a1ca8dce-1151-4d02-b2c1-aab356e1d3e2", new DateTime(2024, 11, 1, 10, 1, 56, 67, DateTimeKind.Utc).AddTicks(9892), "AQAAAAIAAYagAAAAEGq6X0hB7UbphfjJ9aswxWxM17C7KH1ZfCjPUOfmCpWu3u8u9iYLZQn+B4QpgwIRvg==", "d48c2863-a2f4-4007-ae98-b70a12356d66", new DateTime(2025, 5, 1, 10, 1, 56, 67, DateTimeKind.Utc).AddTicks(9892) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 1, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 5, 8, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 5, 1, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("6309e613-c9d2-4bbc-b0b6-546d82d27be5"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 1, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 5, 8, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 5, 1, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("6824a88e-6cc6-4708-99c7-77d96bc4828f"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 1, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 5, 8, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 5, 1, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("803387b4-afb3-4fcb-840a-9dd9f63fc841"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 1, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 5, 8, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 5, 1, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("9f74c4e9-8818-41cf-8520-b055ad40d3f1"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 1, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 5, 8, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 5, 1, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("bd9ebb2b-c661-4801-a15d-1603e988039a"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 1, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 5, 8, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 5, 1, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("cc5644bb-3762-4a4c-8842-a9fc93f5c73a"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 1, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 5, 8, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 5, 1, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("d09c2220-669a-401b-8f6a-4f4f114329df"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 1, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 5, 8, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 5, 1, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 1, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 5, 8, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 5, 1, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("fe807746-bf07-4157-aaf4-f157ff3c6f2a"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 1, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 5, 8, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 5, 1, 10, 1, 56, 305, DateTimeKind.Utc).AddTicks(167) });

            migrationBuilder.InsertData(
                table: "Bids",
                columns: new[] { "Id", "Amount", "AuctionId", "CreatedAt", "IsDonated", "UserId" },
                values: new object[,]
                {
                    { new Guid("10817540-6e0a-4fc0-827f-ef833cc00191"), 500m, new Guid("cc5644bb-3762-4a4c-8842-a9fc93f5c73a"), new DateTime(2025, 5, 1, 9, 11, 56, 305, DateTimeKind.Utc).AddTicks(322), false, "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { new Guid("1497f24f-4ff6-489d-9af1-5dadf12da820"), 1700m, new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"), new DateTime(2025, 5, 1, 2, 1, 56, 305, DateTimeKind.Utc).AddTicks(322), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("19f1c20f-e4f5-4fee-9699-55c22031ac7d"), 550m, new Guid("803387b4-afb3-4fcb-840a-9dd9f63fc841"), new DateTime(2025, 5, 1, 7, 1, 56, 305, DateTimeKind.Utc).AddTicks(322), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("252aade8-8a7a-4806-aeb1-8cef6c5d2491"), 850m, new Guid("d09c2220-669a-401b-8f6a-4f4f114329df"), new DateTime(2025, 5, 1, 4, 1, 56, 305, DateTimeKind.Utc).AddTicks(322), false, "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { new Guid("327b5805-2ca7-4772-8119-eb30916b2904"), 700m, new Guid("6824a88e-6cc6-4708-99c7-77d96bc4828f"), new DateTime(2025, 5, 1, 9, 1, 56, 305, DateTimeKind.Utc).AddTicks(322), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("55f414ee-3faa-40fd-896b-71991cfa40aa"), 230m, new Guid("9f74c4e9-8818-41cf-8520-b055ad40d3f1"), new DateTime(2025, 5, 1, 8, 1, 56, 305, DateTimeKind.Utc).AddTicks(322), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("5a095e47-6403-4dc6-8e0b-efcafa43a72e"), 10500m, new Guid("bd9ebb2b-c661-4801-a15d-1603e988039a"), new DateTime(2025, 5, 1, 9, 31, 56, 305, DateTimeKind.Utc).AddTicks(322), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("9e5a5295-611d-4e1e-aa1b-258775b90444"), 7100m, new Guid("6309e613-c9d2-4bbc-b0b6-546d82d27be5"), new DateTime(2025, 5, 1, 1, 1, 56, 305, DateTimeKind.Utc).AddTicks(322), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("a52ed81c-ce45-448b-a573-97c4b9d5021b"), 220m, new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"), new DateTime(2025, 5, 1, 5, 1, 56, 305, DateTimeKind.Utc).AddTicks(322), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("ab32ac39-0cd5-4941-b72e-f13987514583"), 210m, new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"), new DateTime(2025, 5, 1, 3, 1, 56, 305, DateTimeKind.Utc).AddTicks(322), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("c716a094-d304-418d-a09c-a1a23e5adfe3"), 450m, new Guid("fe807746-bf07-4157-aaf4-f157ff3c6f2a"), new DateTime(2025, 5, 1, 6, 1, 56, 305, DateTimeKind.Utc).AddTicks(322), false, "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { new Guid("f1b4fea8-1509-49f0-be3f-169fc6f02a95"), 1600m, new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"), new DateTime(2025, 5, 1, 0, 1, 56, 305, DateTimeKind.Utc).AddTicks(322), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" }
                });
        }
    }
}
