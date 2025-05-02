using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CharityAuction.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("017099fa-23bd-40e3-a73a-511668a3e9d9"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("0b1f0546-b3bf-4ce9-bc19-cd70c6123309"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("1b507f33-bf08-4fd7-9b7b-37be4396d7a1"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("42ddc98f-fea2-4341-b680-c2ffa81265b4"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("4e2361ad-05eb-44c1-8119-bbfd261f2b63"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("568ec6b7-403b-4f9b-98b1-ed53f300b23c"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("92ef61c5-0ffc-4e12-9c7a-c2dd9c05aff8"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("a2b88f60-9394-4e7c-91fb-db8173f2dd17"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("a6fc542b-747b-4996-93a1-7255b32a6228"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("c09bb673-4389-408d-8788-e5d78a8f9d62"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("e653f443-a56b-4ecf-ad1f-e221230d1f50"));

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: new Guid("ef18a1a6-2f3e-45b4-9c9e-c9d5c6d1253d"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDonated",
                table: "Bids",
                type: "boolean",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsDonated",
                table: "Bids");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a8da342-5a30-4c2a-a9f2-77bb6659c25f",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "26f8a894-9be6-4340-9006-5c1cc8031033", new DateTime(2024, 10, 22, 10, 16, 20, 177, DateTimeKind.Utc).AddTicks(14), "AQAAAAIAAYagAAAAEFIcu/1fEjVd2VdcklcGo5GV8puoR8aqdrsj+IuH9LpADnbbKh/NQbgvU801f48Clg==", "d09042a2-34b9-42f0-b1e0-6d131a0bad07", new DateTime(2025, 4, 22, 10, 16, 20, 177, DateTimeKind.Utc).AddTicks(14) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62a7c1cd-e93a-4aab-a8a4-64e22210c77f",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "6780a098-662a-456d-a229-bcda59627019", new DateTime(2024, 10, 22, 10, 16, 20, 177, DateTimeKind.Utc).AddTicks(14), "AQAAAAIAAYagAAAAEH7o84cq1LntbsEj5rRxYLHpjJJJTlyPf/BWx4xA4GspyjeBDQ8hcrZjWbL/3JGYoA==", "103adb65-7b32-4902-959a-941fb779c37e", new DateTime(2025, 4, 22, 10, 16, 20, 177, DateTimeKind.Utc).AddTicks(14) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b26038d-1a43-4248-90e1-dc7f0381d7fa",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "d4643afe-af77-417a-979c-eb147dc93063", new DateTime(2024, 10, 22, 10, 16, 20, 177, DateTimeKind.Utc).AddTicks(14), "AQAAAAIAAYagAAAAECkvdcjiI2Xl14xTxv7a7HVU7J0/RXrtCVRFfgjnRRaHH0+mns5BfNXljOSdMfMK1Q==", "0ba344ca-0937-41b4-a6f9-30a447a08edd", new DateTime(2025, 4, 22, 10, 16, 20, 177, DateTimeKind.Utc).AddTicks(14) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "31f7f6e5-85a0-456a-b7e4-05d0478ae3c2", new DateTime(2024, 10, 22, 10, 16, 20, 177, DateTimeKind.Utc).AddTicks(14), "AQAAAAIAAYagAAAAEGJgoLcSGPNti3WNUMxnUmkeUZKvapQ5IeyEvmyIHqdd/svRdt3DZIw1ye4ndmgVAw==", "575d3302-4e9b-4b0e-a789-940fec50c674", new DateTime(2025, 4, 22, 10, 16, 20, 177, DateTimeKind.Utc).AddTicks(14) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e034755d-65a9-4f2b-a661-556edac6a6b0",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "d9eefeec-1c92-4eab-9190-bf3832999a5e", new DateTime(2024, 10, 22, 10, 16, 20, 177, DateTimeKind.Utc).AddTicks(14), "AQAAAAIAAYagAAAAEA8GWa1LeHBky29wcnjDovvAvH4Q3rLicbm+98whPwfNGf6j5XFvGMfA9opcwV2Jgw==", "65a3cb0c-5484-4458-936e-26e7ee10e587", new DateTime(2025, 4, 22, 10, 16, 20, 177, DateTimeKind.Utc).AddTicks(14) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 4, 29, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("6309e613-c9d2-4bbc-b0b6-546d82d27be5"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 4, 29, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("6824a88e-6cc6-4708-99c7-77d96bc4828f"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 4, 29, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("803387b4-afb3-4fcb-840a-9dd9f63fc841"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 4, 29, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("9f74c4e9-8818-41cf-8520-b055ad40d3f1"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 4, 29, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("bd9ebb2b-c661-4801-a15d-1603e988039a"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 4, 29, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("cc5644bb-3762-4a4c-8842-a9fc93f5c73a"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 4, 29, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("d09c2220-669a-401b-8f6a-4f4f114329df"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 4, 29, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 4, 29, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: new Guid("fe807746-bf07-4157-aaf4-f157ff3c6f2a"),
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 4, 29, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.InsertData(
                table: "Bids",
                columns: new[] { "Id", "Amount", "AuctionId", "CreatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("017099fa-23bd-40e3-a73a-511668a3e9d9"), 1700m, new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"), new DateTime(2025, 4, 22, 2, 16, 20, 412, DateTimeKind.Utc).AddTicks(8063), "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("0b1f0546-b3bf-4ce9-bc19-cd70c6123309"), 700m, new Guid("6824a88e-6cc6-4708-99c7-77d96bc4828f"), new DateTime(2025, 4, 22, 9, 16, 20, 412, DateTimeKind.Utc).AddTicks(8063), "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("1b507f33-bf08-4fd7-9b7b-37be4396d7a1"), 220m, new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"), new DateTime(2025, 4, 22, 5, 16, 20, 412, DateTimeKind.Utc).AddTicks(8063), "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("42ddc98f-fea2-4341-b680-c2ffa81265b4"), 550m, new Guid("803387b4-afb3-4fcb-840a-9dd9f63fc841"), new DateTime(2025, 4, 22, 7, 16, 20, 412, DateTimeKind.Utc).AddTicks(8063), "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("4e2361ad-05eb-44c1-8119-bbfd261f2b63"), 500m, new Guid("cc5644bb-3762-4a4c-8842-a9fc93f5c73a"), new DateTime(2025, 4, 22, 9, 26, 20, 412, DateTimeKind.Utc).AddTicks(8063), "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { new Guid("568ec6b7-403b-4f9b-98b1-ed53f300b23c"), 230m, new Guid("9f74c4e9-8818-41cf-8520-b055ad40d3f1"), new DateTime(2025, 4, 22, 8, 16, 20, 412, DateTimeKind.Utc).AddTicks(8063), "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("92ef61c5-0ffc-4e12-9c7a-c2dd9c05aff8"), 210m, new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"), new DateTime(2025, 4, 22, 3, 16, 20, 412, DateTimeKind.Utc).AddTicks(8063), "e034755d-65a9-4f2b-a661-556edac6a6b0" },
                    { new Guid("a2b88f60-9394-4e7c-91fb-db8173f2dd17"), 450m, new Guid("fe807746-bf07-4157-aaf4-f157ff3c6f2a"), new DateTime(2025, 4, 22, 6, 16, 20, 412, DateTimeKind.Utc).AddTicks(8063), "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { new Guid("a6fc542b-747b-4996-93a1-7255b32a6228"), 10500m, new Guid("bd9ebb2b-c661-4801-a15d-1603e988039a"), new DateTime(2025, 4, 22, 9, 46, 20, 412, DateTimeKind.Utc).AddTicks(8063), "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("c09bb673-4389-408d-8788-e5d78a8f9d62"), 850m, new Guid("d09c2220-669a-401b-8f6a-4f4f114329df"), new DateTime(2025, 4, 22, 4, 16, 20, 412, DateTimeKind.Utc).AddTicks(8063), "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { new Guid("e653f443-a56b-4ecf-ad1f-e221230d1f50"), 1600m, new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"), new DateTime(2025, 4, 22, 0, 16, 20, 412, DateTimeKind.Utc).AddTicks(8063), "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { new Guid("ef18a1a6-2f3e-45b4-9c9e-c9d5c6d1253d"), 7100m, new Guid("6309e613-c9d2-4bbc-b0b6-546d82d27be5"), new DateTime(2025, 4, 22, 1, 16, 20, 412, DateTimeKind.Utc).AddTicks(8063), "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" }
                });
        }
    }
}
