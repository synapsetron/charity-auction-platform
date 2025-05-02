using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CharityAuction.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsApproved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Auctions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a8da342-5a30-4c2a-a9f2-77bb6659c25f",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "753fbee7-3068-4213-a634-d3fa34740247", new DateTime(2024, 10, 28, 7, 56, 10, 132, DateTimeKind.Utc).AddTicks(9540), "AQAAAAIAAYagAAAAEHlUj7rBeC066v7F/oeQasFz7TaJCGg6RQ/fOTIRvqIgJSgArVR0AJei+KCGIibzEQ==", "ba9917e8-64a2-43bb-ba7c-3dba791b12cc", new DateTime(2025, 4, 28, 7, 56, 10, 132, DateTimeKind.Utc).AddTicks(9540) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62a7c1cd-e93a-4aab-a8a4-64e22210c77f",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "97834df6-0ea2-43aa-a0cc-b151749a2454", new DateTime(2024, 10, 28, 7, 56, 10, 132, DateTimeKind.Utc).AddTicks(9540), "AQAAAAIAAYagAAAAEM2GMbDCeaIZJD6rHw7NUFs42xnrHDUnZEP5srJKUAptxm4ym7FuhwCoNhHds5oofw==", "c078afae-9243-4d50-9e79-08189e952e3c", new DateTime(2025, 4, 28, 7, 56, 10, 132, DateTimeKind.Utc).AddTicks(9540) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b26038d-1a43-4248-90e1-dc7f0381d7fa",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "aac267e7-24a9-4448-a34e-ee66fceb06d5", new DateTime(2024, 10, 28, 7, 56, 10, 132, DateTimeKind.Utc).AddTicks(9540), "AQAAAAIAAYagAAAAEN2VQlyiVSbTnPe807uUAPy/am9hdVepwkLxFCtghXdddEiS9KhSScN6iad/iZs91g==", "e3ab49cd-8133-46cc-9740-774209fac9d7", new DateTime(2025, 4, 28, 7, 56, 10, 132, DateTimeKind.Utc).AddTicks(9540) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "fec7b9f1-82a7-4ae0-8259-4226efdf9207", new DateTime(2024, 10, 28, 7, 56, 10, 132, DateTimeKind.Utc).AddTicks(9540), "AQAAAAIAAYagAAAAEPQHawYqyeu37k2m5G2AczGcbxXpSJCpVeZF4bLAz1kncuUb8rZvcBpzkZrP9xuT1g==", "802e1ae1-e9fd-44b4-a1fb-39a4bb669d29", new DateTime(2025, 4, 28, 7, 56, 10, 132, DateTimeKind.Utc).AddTicks(9540) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e034755d-65a9-4f2b-a661-556edac6a6b0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "f0d30ce3-20cc-49be-8611-e1b6693dac1d", new DateTime(2024, 10, 28, 7, 56, 10, 132, DateTimeKind.Utc).AddTicks(9540), "AQAAAAIAAYagAAAAEI2Yycbvg4RXLOQ0y0VZRlP8NudV8ZvKHDOG0qMghDj7jtTpc2ZC4yraX9YuOkKFlg==", "6698d276-17af-4401-9b30-217ef10f79d4", new DateTime(2025, 4, 28, 7, 56, 10, 132, DateTimeKind.Utc).AddTicks(9540) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"),
                columns: new[] { "CreatedAt", "EndTime", "IsApproved", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 5, 5, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), false, new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("6309e613-c9d2-4bbc-b0b6-546d82d27be5"),
                columns: new[] { "CreatedAt", "EndTime", "IsApproved", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 5, 5, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), false, new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("6824a88e-6cc6-4708-99c7-77d96bc4828f"),
                columns: new[] { "CreatedAt", "EndTime", "IsApproved", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 5, 5, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), false, new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("803387b4-afb3-4fcb-840a-9dd9f63fc841"),
                columns: new[] { "CreatedAt", "EndTime", "IsApproved", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 5, 5, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), false, new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("9f74c4e9-8818-41cf-8520-b055ad40d3f1"),
                columns: new[] { "CreatedAt", "EndTime", "IsApproved", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 5, 5, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), false, new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("bd9ebb2b-c661-4801-a15d-1603e988039a"),
                columns: new[] { "CreatedAt", "EndTime", "IsApproved", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 5, 5, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), false, new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("cc5644bb-3762-4a4c-8842-a9fc93f5c73a"),
                columns: new[] { "CreatedAt", "EndTime", "IsApproved", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 5, 5, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), false, new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("d09c2220-669a-401b-8f6a-4f4f114329df"),
                columns: new[] { "CreatedAt", "EndTime", "IsApproved", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 5, 5, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), false, new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"),
                columns: new[] { "CreatedAt", "EndTime", "IsApproved", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 5, 5, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), false, new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("fe807746-bf07-4157-aaf4-f157ff3c6f2a"),
                columns: new[] { "CreatedAt", "EndTime", "IsApproved", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 5, 5, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410), false, new DateTime(2025, 4, 28, 7, 56, 10, 368, DateTimeKind.Utc).AddTicks(8410) });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Auctions");

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
        }
    }
}
