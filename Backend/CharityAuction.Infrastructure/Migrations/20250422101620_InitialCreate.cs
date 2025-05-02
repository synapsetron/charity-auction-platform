using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CharityAuction.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PhotoUrl = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    CommissionBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auctions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    StartingPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    FinalPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    IsSold = table.Column<bool>(type: "boolean", nullable: false),
                    OrganizerId = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auctions_AspNetUsers_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    AuctionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bids_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bids_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "admin", "ADMIN" },
                    { "2", null, "seller", "SELLER" },
                    { "3", null, "buyer", "BUYER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Balance", "CommissionBalance", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoUrl", "RefreshToken", "RefreshTokenExpiryTime", "Role", "SecurityStamp", "TwoFactorEnabled", "UpdatedAt", "UserName" },
                values: new object[,]
                {
                    { "2a8da342-5a30-4c2a-a9f2-77bb6659c25f", 0, 3500m, 200m, "26f8a894-9be6-4340-9006-5c1cc8031033", new DateTime(2024, 10, 22, 10, 16, 20, 177, DateTimeKind.Utc).AddTicks(14), "buyer3@example.com", true, "Buyer", "Three", false, null, "BUYER3@EXAMPLE.COM", "BUYER.THREE", "AQAAAAIAAYagAAAAEFIcu/1fEjVd2VdcklcGo5GV8puoR8aqdrsj+IuH9LpADnbbKh/NQbgvU801f48Clg==", null, false, "https://ipfs.io/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/5.jpg", null, null, "buyer", "d09042a2-34b9-42f0-b1e0-6d131a0bad07", false, new DateTime(2025, 4, 22, 10, 16, 20, 177, DateTimeKind.Utc).AddTicks(14), "buyer.three" },
                    { "62a7c1cd-e93a-4aab-a8a4-64e22210c77f", 0, 4000m, 100m, "6780a098-662a-456d-a229-bcda59627019", new DateTime(2024, 10, 22, 10, 16, 20, 177, DateTimeKind.Utc).AddTicks(14), "buyer1@example.com", true, "Buyer", "One", false, null, "BUYER1@EXAMPLE.COM", "BUYER.ONE", "AQAAAAIAAYagAAAAEH7o84cq1LntbsEj5rRxYLHpjJJJTlyPf/BWx4xA4GspyjeBDQ8hcrZjWbL/3JGYoA==", null, false, "https://ipfs.io/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/3.jpg", null, null, "buyer", "103adb65-7b32-4902-959a-941fb779c37e", false, new DateTime(2025, 4, 22, 10, 16, 20, 177, DateTimeKind.Utc).AddTicks(14), "buyer.one" },
                    { "7b26038d-1a43-4248-90e1-dc7f0381d7fa", 0, 5000m, 500m, "d4643afe-af77-417a-979c-eb147dc93063", new DateTime(2024, 10, 22, 10, 16, 20, 177, DateTimeKind.Utc).AddTicks(14), "admin@example.com", true, "Admin", "User", false, null, "ADMIN@EXAMPLE.COM", "ADMIN.USER", "AQAAAAIAAYagAAAAECkvdcjiI2Xl14xTxv7a7HVU7J0/RXrtCVRFfgjnRRaHH0+mns5BfNXljOSdMfMK1Q==", null, false, "https://ipfs.io/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1.jpg", null, null, "admin", "0ba344ca-0937-41b4-a6f9-30a447a08edd", false, new DateTime(2025, 4, 22, 10, 16, 20, 177, DateTimeKind.Utc).AddTicks(14), "admin.user" },
                    { "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be", 0, 3000m, 300m, "31f7f6e5-85a0-456a-b7e4-05d0478ae3c2", new DateTime(2024, 10, 22, 10, 16, 20, 177, DateTimeKind.Utc).AddTicks(14), "seller@example.com", true, "Seller", "User", false, null, "SELLER@EXAMPLE.COM", "SELLER.USER", "AQAAAAIAAYagAAAAEGJgoLcSGPNti3WNUMxnUmkeUZKvapQ5IeyEvmyIHqdd/svRdt3DZIw1ye4ndmgVAw==", null, false, "https://ipfs.io/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/2.jpg", null, null, "seller", "575d3302-4e9b-4b0e-a789-940fec50c674", false, new DateTime(2025, 4, 22, 10, 16, 20, 177, DateTimeKind.Utc).AddTicks(14), "seller.user" },
                    { "e034755d-65a9-4f2b-a661-556edac6a6b0", 0, 2500m, 150m, "d9eefeec-1c92-4eab-9190-bf3832999a5e", new DateTime(2024, 10, 22, 10, 16, 20, 177, DateTimeKind.Utc).AddTicks(14), "buyer2@example.com", true, "Buyer", "Two", false, null, "BUYER2@EXAMPLE.COM", "BUYER.TWO", "AQAAAAIAAYagAAAAEA8GWa1LeHBky29wcnjDovvAvH4Q3rLicbm+98whPwfNGf6j5XFvGMfA9opcwV2Jgw==", null, false, "https://ipfs.io/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/4.jpg", null, null, "buyer", "65a3cb0c-5484-4458-936e-26e7ee10e587", false, new DateTime(2025, 4, 22, 10, 16, 20, 177, DateTimeKind.Utc).AddTicks(14), "buyer.two" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "3", "2a8da342-5a30-4c2a-a9f2-77bb6659c25f" },
                    { "3", "62a7c1cd-e93a-4aab-a8a4-64e22210c77f" },
                    { "1", "7b26038d-1a43-4248-90e1-dc7f0381d7fa" },
                    { "2", "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be" },
                    { "3", "e034755d-65a9-4f2b-a661-556edac6a6b0" }
                });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "CreatedAt", "Description", "EndTime", "FinalPrice", "ImageUrl", "IsActive", "IsSold", "OrganizerId", "StartTime", "StartingPrice", "Title" },
                values: new object[,]
                {
                    { new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f"), new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), "Оптичний бінокль для спостереження на великій відстані.", new DateTime(2025, 4, 29, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), null, "https://i.postimg.cc/Tp7FDPKC/binokl.jpg", true, false, "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be", new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), 800m, "Бінокль" },
                    { new Guid("6309e613-c9d2-4bbc-b0b6-546d82d27be5"), new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), "Легендарний радянський гранатомет РПГ-18 \"Муха\".", new DateTime(2025, 4, 29, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), null, "https://i.postimg.cc/0zz3dqmx/RPG-18-Mukha.jpg", true, false, "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be", new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), 7000m, "РПГ-18 Муха" },
                    { new Guid("6824a88e-6cc6-4708-99c7-77d96bc4828f"), new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), "Оригінальний шеврон українського підрозділу №71.", new DateTime(2025, 4, 29, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), null, "https://i.postimg.cc/s13NHWST/shevron-Ukraine-71.jpg", true, false, "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be", new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), 600m, "Шеврон Україна 71" },
                    { new Guid("803387b4-afb3-4fcb-840a-9dd9f63fc841"), new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), "Оригінальна фуражка з комплекту екіпіровки окупантів.", new DateTime(2025, 4, 29, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), null, "https://i.postimg.cc/87xYZnBv/russian-cap.jpg", true, false, "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be", new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), 500m, "Фуражка російської армії" },
                    { new Guid("9f74c4e9-8818-41cf-8520-b055ad40d3f1"), new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), "Армійський пайок російської армії (умовно їстівний).", new DateTime(2025, 4, 29, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), null, "https://i.postimg.cc/5YVRVYrN/russian-payok.jpg", true, false, "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be", new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), 200m, "Російський сухпайок" },
                    { new Guid("bd9ebb2b-c661-4801-a15d-1603e988039a"), new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), "Комплект зброї для зенітного підрозділу в камуфляжі.", new DateTime(2025, 4, 29, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), null, "https://i.postimg.cc/JGndMWpJ/zenitka.jpg", true, false, "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be", new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), 10000m, "Зенітка (комплект)" },
                    { new Guid("cc5644bb-3762-4a4c-8842-a9fc93f5c73a"), new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), "Шеврон спецпідрозділу з емблемою кажана.", new DateTime(2025, 4, 29, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), null, "https://i.postimg.cc/xqgFwd3n/tripe.jpg", true, false, "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be", new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), 450m, "Шеврон Спецназу (Тріп)" },
                    { new Guid("d09c2220-669a-401b-8f6a-4f4f114329df"), new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), "Тактична кобура Blackhawk для зручного носіння зброї.", new DateTime(2025, 4, 29, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), null, "https://i.postimg.cc/d7dxpm4x/kobura-blackhawk.jpg", true, false, "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be", new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), 1200m, "Кобура Blackhawk" },
                    { new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77"), new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), "Пускова установка в ідеальному стані для бойових завдань.", new DateTime(2025, 4, 29, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), null, "https://i.postimg.cc/9rFs09QD/bazuka.jpg", true, false, "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be", new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), 5000m, "Базука" },
                    { new Guid("fe807746-bf07-4157-aaf4-f157ff3c6f2a"), new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), "Типова польова сумка орків, знайдена на передовій.", new DateTime(2025, 4, 29, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), null, "https://i.postimg.cc/K3cpKXF6/russian-orkivska-sumka.jpg", true, false, "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be", new DateTime(2025, 4, 22, 10, 16, 20, 412, DateTimeKind.Utc).AddTicks(7620), 400m, "Орківська сумка" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_OrganizerId",
                table: "Auctions",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_AuctionId",
                table: "Bids",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_UserId",
                table: "Bids",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Auctions");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
