using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CharityAuction.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("0391b8f6-e8fa-4659-894f-ee1ac177cc6b"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("1f43dfee-a53e-4f77-935c-367d85aa5b13"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("36471d8c-800b-4603-8a8d-40629d119406"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("4628fc21-8d04-49de-83d4-87fadf20372d"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("59bad92d-8bce-4231-8345-637bafed40df"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("93b1e458-1668-4633-a95d-5d140b001e57"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("c52523b4-4031-4623-965c-1d323404e4a8"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("d37829b6-bdbb-4f1c-8693-bdcf050728da"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("da4c7c06-4978-4616-bf78-cce057727c6a"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("e56172a1-808e-4d11-918f-b074e8257d20"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("f7026819-8e05-4bcb-8676-e153d1b8e0bd"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("fc6fa5c1-798b-47c9-a271-c65270824a0a"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a8da342-5a30-4c2a-a9f2-77bb6659c25f",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "1fbb0172-699b-4cf0-a805-ada85ecbaadd", new DateTime(2024, 10, 29, 11, 17, 58, 27, DateTimeKind.Utc).AddTicks(1771), "AQAAAAIAAYagAAAAECMh+GNNgf021AOcuajA1qLqqbEL5G/OU25v9bXDyy3ewINxNW5BC2P+oCszXk2khA==", "aeff44bb-0214-4532-9725-6354e345467d", new DateTime(2025, 4, 29, 11, 17, 58, 27, DateTimeKind.Utc).AddTicks(1771) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62a7c1cd-e93a-4aab-a8a4-64e22210c77f",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "7806487c-c108-4615-9faa-1bac66527e42", new DateTime(2024, 10, 29, 11, 17, 58, 27, DateTimeKind.Utc).AddTicks(1771), "AQAAAAIAAYagAAAAEGOoiFmlEy6vW1+d90Gyk68af7C+ZJBGWCW2jICcMLpRp5PmnabFiDpWHwqd48CCGg==", "24805840-98d2-4f98-a293-89714354d1c7", new DateTime(2025, 4, 29, 11, 17, 58, 27, DateTimeKind.Utc).AddTicks(1771) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b26038d-1a43-4248-90e1-dc7f0381d7fa",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "f00efeed-aa59-423a-94b4-8c14cda604e9", new DateTime(2024, 10, 29, 11, 17, 58, 27, DateTimeKind.Utc).AddTicks(1771), "AQAAAAIAAYagAAAAEAZ+4TjXEV59bi8N5GlJomsPhxT8eBcNxR9yb3WLc7pYZMWsXIV/7U6OBtcZzyVffg==", "e4b0a243-767c-4595-83ac-9f7323b0a69b", new DateTime(2025, 4, 29, 11, 17, 58, 27, DateTimeKind.Utc).AddTicks(1771) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "dc0e897c-0e51-49a3-ab0d-764d6a967705", new DateTime(2024, 10, 29, 11, 17, 58, 27, DateTimeKind.Utc).AddTicks(1771), "AQAAAAIAAYagAAAAEEVdYOZ8QKkLcl/O89WcIooX0Ties81/ITGp3Cv94Mmcu/aRaffySPw6/D6nXO/UAQ==", "f4f9891d-ba98-45db-bd47-0b4a6033afbd", new DateTime(2025, 4, 29, 11, 17, 58, 27, DateTimeKind.Utc).AddTicks(1771) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e034755d-65a9-4f2b-a661-556edac6a6b0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "fd11840b-002e-4f13-a5a3-69eac66b1ccb", new DateTime(2024, 10, 29, 11, 17, 58, 27, DateTimeKind.Utc).AddTicks(1771), "AQAAAAIAAYagAAAAEIWVlnaGzFYBSpM5Yww90inxfy6Fry/1YuXH0OgSI/pGU01OMW9UIUHfnqlKmUZsVA==", "f3274cdc-ef5e-41cb-b569-23c61ca1ee40", new DateTime(2025, 4, 29, 11, 17, 58, 27, DateTimeKind.Utc).AddTicks(1771) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 29, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547), new DateTime(2025, 5, 6, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547), new DateTime(2025, 4, 29, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("6309e613-c9d2-4bbc-b0b6-546d82d27be5"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 29, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547), new DateTime(2025, 5, 6, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547), new DateTime(2025, 4, 29, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("6824a88e-6cc6-4708-99c7-77d96bc4828f"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 29, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547), new DateTime(2025, 5, 6, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547), new DateTime(2025, 4, 29, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("803387b4-afb3-4fcb-840a-9dd9f63fc841"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 29, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547), new DateTime(2025, 5, 6, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547), new DateTime(2025, 4, 29, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("9f74c4e9-8818-41cf-8520-b055ad40d3f1"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 29, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547), new DateTime(2025, 5, 6, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547), new DateTime(2025, 4, 29, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("bd9ebb2b-c661-4801-a15d-1603e988039a"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 29, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547), new DateTime(2025, 5, 6, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547), new DateTime(2025, 4, 29, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("cc5644bb-3762-4a4c-8842-a9fc93f5c73a"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 29, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547), new DateTime(2025, 5, 6, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547), new DateTime(2025, 4, 29, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("d09c2220-669a-401b-8f6a-4f4f114329df"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 29, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547), new DateTime(2025, 5, 6, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547), new DateTime(2025, 4, 29, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 29, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547), new DateTime(2025, 5, 6, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547), new DateTime(2025, 4, 29, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("fe807746-bf07-4157-aaf4-f157ff3c6f2a"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 29, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547), new DateTime(2025, 5, 6, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547), new DateTime(2025, 4, 29, 11, 17, 58, 267, DateTimeKind.Utc).AddTicks(4547) });

            migrationBuilder.InsertData(
                table: "Bids",
                columns: new[] { "Id", "Amount", "AuctionId", "CreatedAt", "IsDonated", "UserId" },
                values: new object[,]
                {
                    { new Guid("0391b8f6-e8fa-4659-894f-ee1ac177cc6b"), 10500m, new Guid("bd9ebb2b-c661-4801-a15d-1603e988039a"), new DateTime(2025, 4, 29, 10, 47, 58, 267, DateTimeKind.Utc).AddTicks(4738), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("1f43dfee-a53e-4f77-935c-367d85aa5b13"), 7100m, new Guid("6309e613-c9d2-4bbc-b0b6-546d82d27be5"), new DateTime(2025, 4, 29, 2, 17, 58, 267, DateTimeKind.Utc).AddTicks(4738), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("36471d8c-800b-4603-8a8d-40629d119406"), 550m, new Guid("803387b4-afb3-4fcb-840a-9dd9f63fc841"), new DateTime(2025, 4, 29, 8, 17, 58, 267, DateTimeKind.Utc).AddTicks(4738), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("4628fc21-8d04-49de-83d4-87fadf20372d"), 210m, new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"), new DateTime(2025, 4, 29, 4, 17, 58, 267, DateTimeKind.Utc).AddTicks(4738), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("59bad92d-8bce-4231-8345-637bafed40df"), 500m, new Guid("cc5644bb-3762-4a4c-8842-a9fc93f5c73a"), new DateTime(2025, 4, 29, 10, 27, 58, 267, DateTimeKind.Utc).AddTicks(4738), false, "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { new Guid("93b1e458-1668-4633-a95d-5d140b001e57"), 1600m, new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"), new DateTime(2025, 4, 29, 1, 17, 58, 267, DateTimeKind.Utc).AddTicks(4738), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("c52523b4-4031-4623-965c-1d323404e4a8"), 220m, new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"), new DateTime(2025, 4, 29, 6, 17, 58, 267, DateTimeKind.Utc).AddTicks(4738), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("d37829b6-bdbb-4f1c-8693-bdcf050728da"), 850m, new Guid("d09c2220-669a-401b-8f6a-4f4f114329df"), new DateTime(2025, 4, 29, 5, 17, 58, 267, DateTimeKind.Utc).AddTicks(4738), false, "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { new Guid("da4c7c06-4978-4616-bf78-cce057727c6a"), 450m, new Guid("fe807746-bf07-4157-aaf4-f157ff3c6f2a"), new DateTime(2025, 4, 29, 7, 17, 58, 267, DateTimeKind.Utc).AddTicks(4738), false, "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { new Guid("e56172a1-808e-4d11-918f-b074e8257d20"), 1700m, new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"), new DateTime(2025, 4, 29, 3, 17, 58, 267, DateTimeKind.Utc).AddTicks(4738), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("f7026819-8e05-4bcb-8676-e153d1b8e0bd"), 230m, new Guid("9f74c4e9-8818-41cf-8520-b055ad40d3f1"), new DateTime(2025, 4, 29, 9, 17, 58, 267, DateTimeKind.Utc).AddTicks(4738), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("fc6fa5c1-798b-47c9-a271-c65270824a0a"), 700m, new Guid("6824a88e-6cc6-4708-99c7-77d96bc4828f"), new DateTime(2025, 4, 29, 10, 17, 58, 267, DateTimeKind.Utc).AddTicks(4738), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" }
                });
        }
    }
}
