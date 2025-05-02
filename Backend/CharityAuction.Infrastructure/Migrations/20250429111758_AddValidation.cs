using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CharityAuction.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddValidation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("0c21944d-93d7-4dde-b36d-4a87b85d1457"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("256cf0e6-82d0-48f2-8dfb-6351fee1d157"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("295d15eb-05ae-4596-ace2-81257763af5f"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("330f26eb-a0c9-455b-af9a-d48463168f2c"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("8461da56-1127-44e5-8c45-24bbeea25984"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("a3716aba-e99e-4be2-b634-87b81b6ef347"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("a4a7911e-a359-44e5-acf6-81a6419b532d"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("c4257975-ef0c-4bca-975b-d4bac08f73f7"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("c50b333a-82e1-42a8-b383-06282636ade8"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("ef7cf4d3-adc9-497c-ac0d-27d6b9952eef"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("f4de2f39-9758-4644-9fd0-0563cd06a1c3"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("f6113f49-f5e8-4bd5-9715-9d8e9de77454"));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Auctions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Auctions",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "Name",
                value: "Admin");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "Name",
                value: "Seller");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "Name",
                value: "Buyer");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a8da342-5a30-4c2a-a9f2-77bb6659c25f",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "Role", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "1fbb0172-699b-4cf0-a805-ada85ecbaadd", new DateTime(2024, 10, 29, 11, 17, 58, 27, DateTimeKind.Utc).AddTicks(1771), "AQAAAAIAAYagAAAAECMh+GNNgf021AOcuajA1qLqqbEL5G/OU25v9bXDyy3ewINxNW5BC2P+oCszXk2khA==", "Buyer", "aeff44bb-0214-4532-9725-6354e345467d", new DateTime(2025, 4, 29, 11, 17, 58, 27, DateTimeKind.Utc).AddTicks(1771) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62a7c1cd-e93a-4aab-a8a4-64e22210c77f",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "Role", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "7806487c-c108-4615-9faa-1bac66527e42", new DateTime(2024, 10, 29, 11, 17, 58, 27, DateTimeKind.Utc).AddTicks(1771), "AQAAAAIAAYagAAAAEGOoiFmlEy6vW1+d90Gyk68af7C+ZJBGWCW2jICcMLpRp5PmnabFiDpWHwqd48CCGg==", "Buyer", "24805840-98d2-4f98-a293-89714354d1c7", new DateTime(2025, 4, 29, 11, 17, 58, 27, DateTimeKind.Utc).AddTicks(1771) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b26038d-1a43-4248-90e1-dc7f0381d7fa",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "Role", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "f00efeed-aa59-423a-94b4-8c14cda604e9", new DateTime(2024, 10, 29, 11, 17, 58, 27, DateTimeKind.Utc).AddTicks(1771), "AQAAAAIAAYagAAAAEAZ+4TjXEV59bi8N5GlJomsPhxT8eBcNxR9yb3WLc7pYZMWsXIV/7U6OBtcZzyVffg==", "Admin", "e4b0a243-767c-4595-83ac-9f7323b0a69b", new DateTime(2025, 4, 29, 11, 17, 58, 27, DateTimeKind.Utc).AddTicks(1771) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "Role", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "dc0e897c-0e51-49a3-ab0d-764d6a967705", new DateTime(2024, 10, 29, 11, 17, 58, 27, DateTimeKind.Utc).AddTicks(1771), "AQAAAAIAAYagAAAAEEVdYOZ8QKkLcl/O89WcIooX0Ties81/ITGp3Cv94Mmcu/aRaffySPw6/D6nXO/UAQ==", "Seller", "f4f9891d-ba98-45db-bd47-0b4a6033afbd", new DateTime(2025, 4, 29, 11, 17, 58, 27, DateTimeKind.Utc).AddTicks(1771) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e034755d-65a9-4f2b-a661-556edac6a6b0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "Role", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "fd11840b-002e-4f13-a5a3-69eac66b1ccb", new DateTime(2024, 10, 29, 11, 17, 58, 27, DateTimeKind.Utc).AddTicks(1771), "AQAAAAIAAYagAAAAEIWVlnaGzFYBSpM5Yww90inxfy6Fry/1YuXH0OgSI/pGU01OMW9UIUHfnqlKmUZsVA==", "Buyer", "f3274cdc-ef5e-41cb-b569-23c61ca1ee40", new DateTime(2025, 4, 29, 11, 17, 58, 27, DateTimeKind.Utc).AddTicks(1771) });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Auctions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Auctions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "Name",
                value: "admin");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "Name",
                value: "seller");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "Name",
                value: "buyer");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a8da342-5a30-4c2a-a9f2-77bb6659c25f",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "Role", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "753fbee7-3068-4213-a634-d3fa34740247", new DateTime(2024, 10, 28, 7, 56, 10, 132, DateTimeKind.Utc).AddTicks(9540), "AQAAAAIAAYagAAAAEHlUj7rBeC066v7F/oeQasFz7TaJCGg6RQ/fOTIRvqIgJSgArVR0AJei+KCGIibzEQ==", "buyer", "ba9917e8-64a2-43bb-ba7c-3dba791b12cc", new DateTime(2025, 4, 28, 7, 56, 10, 132, DateTimeKind.Utc).AddTicks(9540) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62a7c1cd-e93a-4aab-a8a4-64e22210c77f",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "Role", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "97834df6-0ea2-43aa-a0cc-b151749a2454", new DateTime(2024, 10, 28, 7, 56, 10, 132, DateTimeKind.Utc).AddTicks(9540), "AQAAAAIAAYagAAAAEM2GMbDCeaIZJD6rHw7NUFs42xnrHDUnZEP5srJKUAptxm4ym7FuhwCoNhHds5oofw==", "buyer", "c078afae-9243-4d50-9e79-08189e952e3c", new DateTime(2025, 4, 28, 7, 56, 10, 132, DateTimeKind.Utc).AddTicks(9540) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b26038d-1a43-4248-90e1-dc7f0381d7fa",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "Role", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "aac267e7-24a9-4448-a34e-ee66fceb06d5", new DateTime(2024, 10, 28, 7, 56, 10, 132, DateTimeKind.Utc).AddTicks(9540), "AQAAAAIAAYagAAAAEN2VQlyiVSbTnPe807uUAPy/am9hdVepwkLxFCtghXdddEiS9KhSScN6iad/iZs91g==", "admin", "e3ab49cd-8133-46cc-9740-774209fac9d7", new DateTime(2025, 4, 28, 7, 56, 10, 132, DateTimeKind.Utc).AddTicks(9540) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "Role", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "fec7b9f1-82a7-4ae0-8259-4226efdf9207", new DateTime(2024, 10, 28, 7, 56, 10, 132, DateTimeKind.Utc).AddTicks(9540), "AQAAAAIAAYagAAAAEPQHawYqyeu37k2m5G2AczGcbxXpSJCpVeZF4bLAz1kncuUb8rZvcBpzkZrP9xuT1g==", "seller", "802e1ae1-e9fd-44b4-a1fb-39a4bb669d29", new DateTime(2025, 4, 28, 7, 56, 10, 132, DateTimeKind.Utc).AddTicks(9540) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e034755d-65a9-4f2b-a661-556edac6a6b0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "Role", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "f0d30ce3-20cc-49be-8611-e1b6693dac1d", new DateTime(2024, 10, 28, 7, 56, 10, 132, DateTimeKind.Utc).AddTicks(9540), "AQAAAAIAAYagAAAAEI2Yycbvg4RXLOQ0y0VZRlP8NudV8ZvKHDOG0qMghDj7jtTpc2ZC4yraX9YuOkKFlg==", "buyer", "6698d276-17af-4401-9b30-217ef10f79d4", new DateTime(2025, 4, 28, 7, 56, 10, 132, DateTimeKind.Utc).AddTicks(9540) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 5, 5, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("6309e613-c9d2-4bbc-b0b6-546d82d27be5"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 5, 5, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("6824a88e-6cc6-4708-99c7-77d96bc4828f"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 5, 5, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("803387b4-afb3-4fcb-840a-9dd9f63fc841"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 5, 5, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("9f74c4e9-8818-41cf-8520-b055ad40d3f1"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 5, 5, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("bd9ebb2b-c661-4801-a15d-1603e988039a"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 5, 5, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("cc5644bb-3762-4a4c-8842-a9fc93f5c73a"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 5, 5, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("d09c2220-669a-401b-8f6a-4f4f114329df"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 5, 5, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 5, 5, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("fe807746-bf07-4157-aaf4-f157ff3c6f2a"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 5, 5, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.InsertData(
                table: "Bids",
                columns: new[] { "Id", "Amount", "AuctionId", "CreatedAt", "IsDonated", "UserId" },
                values: new object[,]
                {
                    { new Guid("0c21944d-93d7-4dde-b36d-4a87b85d1457"), 220m, new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"), new DateTime(2025, 4, 28, 2, 56, 10, 368, DateTimeKind.Utc).AddTicks(8537), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("256cf0e6-82d0-48f2-8dfb-6351fee1d157"), 450m, new Guid("fe807746-bf07-4157-aaf4-f157ff3c6f2a"), new DateTime(2025, 4, 28, 3, 56, 10, 368, DateTimeKind.Utc).AddTicks(8537), false, "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { new Guid("295d15eb-05ae-4596-ace2-81257763af5f"), 850m, new Guid("d09c2220-669a-401b-8f6a-4f4f114329df"), new DateTime(2025, 4, 28, 1, 56, 10, 368, DateTimeKind.Utc).AddTicks(8537), false, "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { new Guid("330f26eb-a0c9-455b-af9a-d48463168f2c"), 230m, new Guid("9f74c4e9-8818-41cf-8520-b055ad40d3f1"), new DateTime(2025, 4, 28, 5, 56, 10, 368, DateTimeKind.Utc).AddTicks(8537), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("8461da56-1127-44e5-8c45-24bbeea25984"), 550m, new Guid("803387b4-afb3-4fcb-840a-9dd9f63fc841"), new DateTime(2025, 4, 28, 4, 56, 10, 368, DateTimeKind.Utc).AddTicks(8537), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("a3716aba-e99e-4be2-b634-87b81b6ef347"), 500m, new Guid("cc5644bb-3762-4a4c-8842-a9fc93f5c73a"), new DateTime(2025, 4, 28, 7, 6, 10, 368, DateTimeKind.Utc).AddTicks(8537), false, "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { new Guid("a4a7911e-a359-44e5-acf6-81a6419b532d"), 210m, new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"), new DateTime(2025, 4, 28, 0, 56, 10, 368, DateTimeKind.Utc).AddTicks(8537), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("c4257975-ef0c-4bca-975b-d4bac08f73f7"), 7100m, new Guid("6309e613-c9d2-4bbc-b0b6-546d82d27be5"), new DateTime(2025, 4, 27, 22, 56, 10, 368, DateTimeKind.Utc).AddTicks(8537), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("c50b333a-82e1-42a8-b383-06282636ade8"), 10500m, new Guid("bd9ebb2b-c661-4801-a15d-1603e988039a"), new DateTime(2025, 4, 28, 7, 26, 10, 368, DateTimeKind.Utc).AddTicks(8537), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("ef7cf4d3-adc9-497c-ac0d-27d6b9952eef"), 700m, new Guid("6824a88e-6cc6-4708-99c7-77d96bc4828f"), new DateTime(2025, 4, 28, 6, 56, 10, 368, DateTimeKind.Utc).AddTicks(8537), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("f4de2f39-9758-4644-9fd0-0563cd06a1c3"), 1600m, new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"), new DateTime(2025, 4, 27, 21, 56, 10, 368, DateTimeKind.Utc).AddTicks(8537), false, "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("f6113f49-f5e8-4bd5-9715-9d8e9de77454"), 1700m, new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"), new DateTime(2025, 4, 27, 23, 56, 10, 368, DateTimeKind.Utc).AddTicks(8537), false, "e034755d-65a9-4f2b-a661-556edac6a6b0" }
                });
        }
    }
}
