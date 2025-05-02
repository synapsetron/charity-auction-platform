using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CharityAuction.Domain.Entities;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharityAuction.Infrastructure.Persistence
{
    public static class DataSeeder
    {
        // Используем постоянные идентификаторы вместо случайных для предсказуемых результатов
        private static class Ids
        {
            // Роли
            public const string AdminRoleId = "1";
            public const string SellerRoleId = "2";
            public const string BuyerRoleId = "3";

            // Пользователи (используем стабильные GUID)
            public static readonly string AdminUserId = "7b26038d-1a43-4248-90e1-dc7f0381d7fa";
            public static readonly string SellerUserId = "c9db7b0d-5889-4a71-b1a9-cf59ef2fa4be";
            public static readonly string BuyerUserId1 = "62a7c1cd-e93a-4aab-a8a4-64e22210c77f";
            public static readonly string BuyerUserId2 = "e034755d-65a9-4f2b-a661-556edac6a6b0";
            public static readonly string BuyerUserId3 = "2a8da342-5a30-4c2a-a9f2-77bb6659c25f";

            // Аукционы
            public static readonly Guid Auction1Id = new Guid("db2b8c40-60c7-4ed1-a6cb-393be4444f77");
            public static readonly Guid Auction2Id = new Guid("29fe88ee-701c-42c3-9a4f-7d1926d1c23f");
            public static readonly Guid Auction3Id = new Guid("d09c2220-669a-401b-8f6a-4f4f114329df");
            public static readonly Guid Auction4Id = new Guid("6309e613-c9d2-4bbc-b0b6-546d82d27be5");
            public static readonly Guid Auction5Id = new Guid("803387b4-afb3-4fcb-840a-9dd9f63fc841");
            public static readonly Guid Auction6Id = new Guid("fe807746-bf07-4157-aaf4-f157ff3c6f2a");
            public static readonly Guid Auction7Id = new Guid("9f74c4e9-8818-41cf-8520-b055ad40d3f1");
            public static readonly Guid Auction8Id = new Guid("6824a88e-6cc6-4708-99c7-77d96bc4828f");
            public static readonly Guid Auction9Id = new Guid("cc5644bb-3762-4a4c-8842-a9fc93f5c73a");
            public static readonly Guid Auction10Id = new Guid("bd9ebb2b-c661-4801-a15d-1603e988039a");


        }

        public static void Seed(ModelBuilder modelBuilder)
        {
            SeedRoles(modelBuilder);
            SeedUsers(modelBuilder);
            SeedUserRoles(modelBuilder);
            SeedAuctions(modelBuilder);
            SeedBids(modelBuilder);
        }

        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = Ids.AdminRoleId, Name = UserRole.Admin, NormalizedName = UserRole.Admin.ToUpper() },
                new IdentityRole { Id = Ids.SellerRoleId, Name = UserRole.Seller, NormalizedName = UserRole.Seller.ToUpper() },
                new IdentityRole { Id = Ids.BuyerRoleId, Name = UserRole.Buyer, NormalizedName = UserRole.Buyer.ToUpper() }
            );
        }

        private static void SeedUsers(ModelBuilder modelBuilder)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var now = DateTime.UtcNow;
            var pastDate = now.AddMonths(-6);

            // Создаем список пользователей с предопределенными ID
            var users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = Ids.AdminUserId,
                    FirstName = "Admin",
                    LastName = "User",
                    UserName = "admin.user",
                    NormalizedUserName = "ADMIN.USER",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    PhotoUrl = "https://ipfs.io/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1.jpg",
                    Role = UserRole.Admin,
                    CommissionBalance = 500m,
                    Balance = 5000m,
                    EmailConfirmed = true,
                    CreatedAt = pastDate,
                    UpdatedAt = now,
                    PasswordHash = passwordHasher.HashPassword(null!, "Admin123!")
                },
                new ApplicationUser
                {
                    Id = Ids.SellerUserId,
                    FirstName = "Seller",
                    LastName = "User",
                    UserName = "seller.user",
                    NormalizedUserName = "SELLER.USER",
                    Email = "seller@example.com",
                    NormalizedEmail = "SELLER@EXAMPLE.COM",
                    PhotoUrl = "https://ipfs.io/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/2.jpg",
                    Role = UserRole.Seller,
                    CommissionBalance = 300m,
                    Balance = 3000m,
                    EmailConfirmed = true,
                    CreatedAt = pastDate,
                    UpdatedAt = now,
                    PasswordHash = passwordHasher.HashPassword(null!, "Seller123!")
                },
                new ApplicationUser
                {
                    Id = Ids.BuyerUserId1,
                    FirstName = "Buyer",
                    LastName = "One",
                    UserName = "buyer.one",
                    NormalizedUserName = "BUYER.ONE",
                    Email = "buyer1@example.com",
                    NormalizedEmail = "BUYER1@EXAMPLE.COM",
                    PhotoUrl = "https://ipfs.io/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/3.jpg",
                    Role = UserRole.Buyer,
                    CommissionBalance = 100m,
                    Balance = 4000m,
                    EmailConfirmed = true,
                    CreatedAt = pastDate,
                    UpdatedAt = now,
                    PasswordHash = passwordHasher.HashPassword(null!, "Buyer123!")
                },
                new ApplicationUser
                {
                    Id = Ids.BuyerUserId2,
                    FirstName = "Buyer",
                    LastName = "Two",
                    UserName = "buyer.two",
                    NormalizedUserName = "BUYER.TWO",
                    Email = "buyer2@example.com",
                    NormalizedEmail = "BUYER2@EXAMPLE.COM",
                    PhotoUrl = "https://ipfs.io/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/4.jpg",
                    Role = UserRole.Buyer,
                    CommissionBalance = 150m,
                    Balance = 2500m,
                    EmailConfirmed = true,
                    CreatedAt = pastDate,
                    UpdatedAt = now,
                    PasswordHash = passwordHasher.HashPassword(null!, "Buyer123!")
                },
                new ApplicationUser
                {
                    Id = Ids.BuyerUserId3,
                    FirstName = "Buyer",
                    LastName = "Three",
                    UserName = "buyer.three",
                    NormalizedUserName = "BUYER.THREE",
                    Email = "buyer3@example.com",
                    NormalizedEmail = "BUYER3@EXAMPLE.COM",
                    PhotoUrl = "https://ipfs.io/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/5.jpg",
                    Role = UserRole.Buyer,
                    CommissionBalance = 200m,
                    Balance = 3500m,
                    EmailConfirmed = true,
                    CreatedAt = pastDate,
                    UpdatedAt = now,
                    PasswordHash = passwordHasher.HashPassword(null!, "Buyer123!")
                }
            };

            modelBuilder.Entity<ApplicationUser>().HasData(users);
        }

        private static void SeedUserRoles(ModelBuilder modelBuilder)
        {
            var userRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string> { UserId = Ids.AdminUserId, RoleId = Ids.AdminRoleId },
                new IdentityUserRole<string> { UserId = Ids.SellerUserId, RoleId = Ids.SellerRoleId },
                new IdentityUserRole<string> { UserId = Ids.BuyerUserId1, RoleId = Ids.BuyerRoleId },
                new IdentityUserRole<string> { UserId = Ids.BuyerUserId2, RoleId = Ids.BuyerRoleId },
                new IdentityUserRole<string> { UserId = Ids.BuyerUserId3, RoleId = Ids.BuyerRoleId }
            };

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        }

        private static void SeedAuctions(ModelBuilder modelBuilder)
        {
            var now = DateTime.UtcNow;
            var auctions = new List<Auction>
            {
                new Auction
                {
                    Id = Ids.Auction1Id,
                    Title = "Базука",
                    Description = "Пускова установка в ідеальному стані для бойових завдань.",
                    StartingPrice = 5000m,
                    ImageUrl = "https://i.postimg.cc/9rFs09QD/bazuka.jpg",
                    OrganizerId = Ids.SellerUserId,
                    StartTime = now,
                    EndTime = now.AddDays(7),
                    IsActive = true,
                    CreatedAt = now
                },
                new Auction
                {
                    Id = Ids.Auction2Id,
                    Title = "Бінокль",
                    Description = "Оптичний бінокль для спостереження на великій відстані.",
                    StartingPrice = 800m,
                    ImageUrl = "https://i.postimg.cc/Tp7FDPKC/binokl.jpg",
                    OrganizerId = Ids.SellerUserId,
                    StartTime = now,
                    EndTime = now.AddDays(7),
                    IsActive = true,
                    CreatedAt = now
                },
                new Auction
                {
                    Id = Ids.Auction3Id,
                    Title = "Кобура Blackhawk",
                    Description = "Тактична кобура Blackhawk для зручного носіння зброї.",
                    StartingPrice = 1200m,
                    ImageUrl = "https://i.postimg.cc/d7dxpm4x/kobura-blackhawk.jpg",
                    OrganizerId = Ids.SellerUserId,
                    StartTime = now,
                    EndTime = now.AddDays(7),
                    IsActive = true,
                    CreatedAt = now
                },
                new Auction
                {
                    Id = Ids.Auction4Id,
                    Title = "РПГ-18 Муха",
                    Description = "Легендарний радянський гранатомет РПГ-18 \"Муха\".",
                    StartingPrice = 7000m,
                    ImageUrl = "https://i.postimg.cc/0zz3dqmx/RPG-18-Mukha.jpg",
                    OrganizerId = Ids.SellerUserId,
                    StartTime = now,
                    EndTime = now.AddDays(7),
                    IsActive = true,
                    CreatedAt = now
                },
                new Auction
                {
                    Id = Ids.Auction5Id,
                    Title = "Фуражка російської армії",
                    Description = "Оригінальна фуражка з комплекту екіпіровки окупантів.",
                    StartingPrice = 500m,
                    ImageUrl = "https://i.postimg.cc/87xYZnBv/russian-cap.jpg",
                    OrganizerId = Ids.SellerUserId,
                    StartTime = now,
                    EndTime = now.AddDays(7),
                    IsActive = true,
                    CreatedAt = now
                },
                new Auction
                {
                    Id = Ids.Auction6Id,
                    Title = "Орківська сумка",
                    Description = "Типова польова сумка орків, знайдена на передовій.",
                    StartingPrice = 400m,
                    ImageUrl = "https://i.postimg.cc/K3cpKXF6/russian-orkivska-sumka.jpg",
                    OrganizerId = Ids.SellerUserId,
                    StartTime = now,
                    EndTime = now.AddDays(7),
                    IsActive = true,
                    CreatedAt = now
                },
                new Auction
                {
                    Id = Ids.Auction7Id,
                    Title = "Російський сухпайок",
                    Description = "Армійський пайок російської армії (умовно їстівний).",
                    StartingPrice = 200m,
                    ImageUrl = "https://i.postimg.cc/5YVRVYrN/russian-payok.jpg",
                    OrganizerId = Ids.SellerUserId,
                    StartTime = now,
                    EndTime = now.AddDays(7),
                    IsActive = true,
                    CreatedAt = now
                },
                new Auction
                {
                    Id = Ids.Auction8Id,
                    Title = "Шеврон Україна 71",
                    Description = "Оригінальний шеврон українського підрозділу №71.",
                    StartingPrice = 600m,
                    ImageUrl = "https://i.postimg.cc/s13NHWST/shevron-Ukraine-71.jpg",
                    OrganizerId = Ids.SellerUserId,
                    StartTime = now,
                    EndTime = now.AddDays(7),
                    IsActive = true,
                    CreatedAt = now
                },
                new Auction
                {
                    Id = Ids.Auction9Id,
                    Title = "Шеврон Спецназу (Тріп)",
                    Description = "Шеврон спецпідрозділу з емблемою кажана.",
                    StartingPrice = 450m,
                    ImageUrl = "https://i.postimg.cc/xqgFwd3n/tripe.jpg",
                    OrganizerId = Ids.SellerUserId,
                    StartTime = now,
                    EndTime = now.AddDays(7),
                    IsActive = true,
                    CreatedAt = now
                },
                new Auction
                {
                    Id = Ids.Auction10Id,
                    Title = "Зенітка (комплект)",
                    Description = "Комплект зброї для зенітного підрозділу в камуфляжі.",
                    StartingPrice = 10000m,
                    ImageUrl = "https://i.postimg.cc/JGndMWpJ/zenitka.jpg",
                    OrganizerId = Ids.SellerUserId,
                    StartTime = now,
                    EndTime = now.AddDays(7),
                    IsActive = true,
                    CreatedAt = now
                }
            };

            modelBuilder.Entity<Auction>().HasData(auctions);
        }


        private static void SeedBids(ModelBuilder modelBuilder)
        {
            var now = DateTime.UtcNow;
            var bids = new List<Bid>();

            // Аукцион 1
            bids.Add(new Bid
            {
                Id = Guid.NewGuid(),
                Amount = 1600m,
                AuctionId = Ids.Auction1Id,
                UserId = Ids.BuyerUserId1,
                CreatedAt = now.AddHours(-10)
            });
            bids.Add(new Bid
            {
                Id = Guid.NewGuid(),
                Amount = 1700m,
                AuctionId = Ids.Auction1Id,
                UserId = Ids.BuyerUserId2,
                CreatedAt = now.AddHours(-8)
            });

            // Аукцион 2
            bids.Add(new Bid
            {
                Id = Guid.NewGuid(),
                Amount = 210m,
                AuctionId = Ids.Auction2Id,
                UserId = Ids.BuyerUserId2,
                CreatedAt = now.AddHours(-7)
            });
            bids.Add(new Bid
            {
                Id = Guid.NewGuid(),
                Amount = 220m,
                AuctionId = Ids.Auction2Id,
                UserId = Ids.BuyerUserId1,
                CreatedAt = now.AddHours(-5)
            });

            // Аукцион 3
            bids.Add(new Bid
            {
                Id = Guid.NewGuid(),
                Amount = 850m,
                AuctionId = Ids.Auction3Id,
                UserId = Ids.BuyerUserId3,
                CreatedAt = now.AddHours(-6)
            });

            // Аукцион 4
            bids.Add(new Bid
            {
                Id = Guid.NewGuid(),
                Amount = 7100m,
                AuctionId = Ids.Auction4Id,
                UserId = Ids.BuyerUserId1,
                CreatedAt = now.AddHours(-9)
            });

            // Аукцион 5
            bids.Add(new Bid
            {
                Id = Guid.NewGuid(),
                Amount = 550m,
                AuctionId = Ids.Auction5Id,
                UserId = Ids.BuyerUserId2,
                CreatedAt = now.AddHours(-3)
            });

            // Аукцион 6
            bids.Add(new Bid
            {
                Id = Guid.NewGuid(),
                Amount = 450m,
                AuctionId = Ids.Auction6Id,
                UserId = Ids.BuyerUserId3,
                CreatedAt = now.AddHours(-4)
            });

            // Аукцион 7
            bids.Add(new Bid
            {
                Id = Guid.NewGuid(),
                Amount = 230m,
                AuctionId = Ids.Auction7Id,
                UserId = Ids.BuyerUserId1,
                CreatedAt = now.AddHours(-2)
            });

            // Аукцион 8
            bids.Add(new Bid
            {
                Id = Guid.NewGuid(),
                Amount = 700m,
                AuctionId = Ids.Auction8Id,
                UserId = Ids.BuyerUserId2,
                CreatedAt = now.AddHours(-1)
            });

            // Аукцион 9
            bids.Add(new Bid
            {
                Id = Guid.NewGuid(),
                Amount = 500m,
                AuctionId = Ids.Auction9Id,
                UserId = Ids.BuyerUserId3,
                CreatedAt = now.AddMinutes(-50)
            });

            // Аукцион 10
            bids.Add(new Bid
            {
                Id = Guid.NewGuid(),
                Amount = 10500m,
                AuctionId = Ids.Auction10Id,
                UserId = Ids.BuyerUserId1,
                CreatedAt = now.AddMinutes(-30)
            });

            modelBuilder.Entity<Bid>().HasData(bids);
        }

    }
}