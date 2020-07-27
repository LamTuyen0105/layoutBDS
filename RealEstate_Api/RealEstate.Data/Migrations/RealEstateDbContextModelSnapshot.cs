﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealEstate.Data.EF;

namespace RealEstate.Data.Migrations
{
    [DbContext(typeof(RealEstateDbContext))]
    partial class RealEstateDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("AppUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserTokens");
                });

            modelBuilder.Entity("RealEstate.Data.Entity.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppRoles");
                });

            modelBuilder.Entity("RealEstate.Data.Entity.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("IdentityNumber")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Provider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WardId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WardId");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("RealEstate.Data.Entity.Direction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DirectionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Directions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DirectionName = "Đông"
                        },
                        new
                        {
                            Id = 2,
                            DirectionName = "Tây"
                        },
                        new
                        {
                            Id = 3,
                            DirectionName = "Nam"
                        },
                        new
                        {
                            Id = 4,
                            DirectionName = "Bắc"
                        },
                        new
                        {
                            Id = 5,
                            DirectionName = "Đông Bắc"
                        },
                        new
                        {
                            Id = 6,
                            DirectionName = "Tây Bắc"
                        },
                        new
                        {
                            Id = 7,
                            DirectionName = "Đông Nam"
                        },
                        new
                        {
                            Id = 8,
                            DirectionName = "Tây Nam"
                        });
                });

            modelBuilder.Entity("RealEstate.Data.Entity.District", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Prefix")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<int>("ProvinceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("RealEstate.Data.Entity.EvaluationStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EvaluationStatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("EvaluationStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EvaluationStatusName = "Đã Thẩm Định"
                        },
                        new
                        {
                            Id = 2,
                            EvaluationStatusName = "Chưa Thẩm Định"
                        });
                });

            modelBuilder.Entity("RealEstate.Data.Entity.Favorite", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<bool>("Like")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.HasKey("UserId", "PropertyId");

                    b.HasIndex("PropertyId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("RealEstate.Data.Entity.LegalPaper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TypeOfLegalPapers")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("LegalPapers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TypeOfLegalPapers = "Sổ Hồng"
                        },
                        new
                        {
                            Id = 2,
                            TypeOfLegalPapers = "Sổ Đỏ"
                        },
                        new
                        {
                            Id = 3,
                            TypeOfLegalPapers = "Giấy Tay"
                        },
                        new
                        {
                            Id = 4,
                            TypeOfLegalPapers = "Giấy Tờ Hợp Lệ"
                        },
                        new
                        {
                            Id = 5,
                            TypeOfLegalPapers = "Đang Hợp Thức Hóa"
                        },
                        new
                        {
                            Id = 6,
                            TypeOfLegalPapers = "Chủ Quyền Tư Nhân"
                        },
                        new
                        {
                            Id = 7,
                            TypeOfLegalPapers = "Hợp Đồng"
                        });
                });

            modelBuilder.Entity("RealEstate.Data.Entity.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateSubmitted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2020, 7, 22, 18, 50, 44, 974, DateTimeKind.Local).AddTicks(2801));

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Share")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("View")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.ToTable("News");
                });

            modelBuilder.Entity("RealEstate.Data.Entity.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApartmentNumber")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<double?>("Area")
                        .HasColumnType("float");

                    b.Property<double?>("AreaFrom")
                        .HasColumnType("float");

                    b.Property<double?>("AreaTo")
                        .HasColumnType("float");

                    b.Property<string>("ContactName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("ContactPhone")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailContact")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("EndDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2020, 8, 21, 18, 50, 44, 989, DateTimeKind.Local).AddTicks(2756));

                    b.Property<int?>("EvaluationStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<double?>("Facade")
                        .HasColumnType("float");

                    b.Property<int?>("HouseDirectionId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDelete")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<double?>("Lat")
                        .HasColumnType("float");

                    b.Property<int>("LegalPapersId")
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<double?>("Length")
                        .HasColumnType("float");

                    b.Property<double?>("Lng")
                        .HasColumnType("float");

                    b.Property<int?>("NumberOfBedrooms")
                        .HasColumnType("int");

                    b.Property<string>("NumberOfStoreys")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("NumberOfWCs")
                        .HasColumnType("int");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("PriceFrom")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("PriceTo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2020, 7, 22, 18, 50, 44, 989, DateTimeKind.Local).AddTicks(2163));

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("StreetNames")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("TypeOfPropertyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("TypeOfTransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<DateTime>("UpdateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2020, 7, 22, 18, 50, 44, 989, DateTimeKind.Local).AddTicks(3467));

                    b.Property<Guid?>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("WardId")
                        .HasColumnType("int");

                    b.Property<double?>("Width")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("EvaluationStatusId");

                    b.HasIndex("HouseDirectionId");

                    b.HasIndex("LegalPapersId");

                    b.HasIndex("TypeOfPropertyId");

                    b.HasIndex("TypeOfTransactionId");

                    b.HasIndex("UserID");

                    b.HasIndex("WardId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("RealEstate.Data.Entity.PropertyImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Caption")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<long?>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<string>("LinkName")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<int?>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.ToTable("PropertyImages");
                });

            modelBuilder.Entity("RealEstate.Data.Entity.Province", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("RealEstate.Data.Entity.TypeOfProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TypeOfPropertyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("TypeOfPrperties");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TypeOfPropertyName = "Chung Cư"
                        },
                        new
                        {
                            Id = 2,
                            TypeOfPropertyName = "Căn Hộ"
                        },
                        new
                        {
                            Id = 3,
                            TypeOfPropertyName = "Nhà Riêng"
                        },
                        new
                        {
                            Id = 4,
                            TypeOfPropertyName = "Đất Nền"
                        });
                });

            modelBuilder.Entity("RealEstate.Data.Entity.TypeOfTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TypeOfTransactionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("TypeOfTransactions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TypeOfTransactionName = "Cần Bán"
                        },
                        new
                        {
                            Id = 2,
                            TypeOfTransactionName = "Cho Thuê"
                        },
                        new
                        {
                            Id = 3,
                            TypeOfTransactionName = "Cần Mua"
                        },
                        new
                        {
                            Id = 4,
                            TypeOfTransactionName = "Cần Thuê"
                        });
                });

            modelBuilder.Entity("RealEstate.Data.Entity.Ward", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Prefix")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Wards");
                });

            modelBuilder.Entity("RealEstate.Data.Entity.AppUser", b =>
                {
                    b.HasOne("RealEstate.Data.Entity.Ward", "Ward")
                        .WithMany("AppUsers")
                        .HasForeignKey("WardId");
                });

            modelBuilder.Entity("RealEstate.Data.Entity.District", b =>
                {
                    b.HasOne("RealEstate.Data.Entity.Province", "Province")
                        .WithMany("Districts")
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RealEstate.Data.Entity.Favorite", b =>
                {
                    b.HasOne("RealEstate.Data.Entity.Property", "Property")
                        .WithMany("Favorites")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstate.Data.Entity.AppUser", "AppUser")
                        .WithMany("Favorites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RealEstate.Data.Entity.Property", b =>
                {
                    b.HasOne("RealEstate.Data.Entity.EvaluationStatus", "EvaluationStatus")
                        .WithMany("Properties")
                        .HasForeignKey("EvaluationStatusId");

                    b.HasOne("RealEstate.Data.Entity.Direction", "Direction")
                        .WithMany("Properties")
                        .HasForeignKey("HouseDirectionId");

                    b.HasOne("RealEstate.Data.Entity.LegalPaper", "LegalPaper")
                        .WithMany("Properties")
                        .HasForeignKey("LegalPapersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstate.Data.Entity.TypeOfProperty", "TypeOfProperty")
                        .WithMany("Properties")
                        .HasForeignKey("TypeOfPropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstate.Data.Entity.TypeOfTransaction", "TypeOfTransaction")
                        .WithMany("Properties")
                        .HasForeignKey("TypeOfTransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstate.Data.Entity.AppUser", "AppUser")
                        .WithMany("Properties")
                        .HasForeignKey("UserID");

                    b.HasOne("RealEstate.Data.Entity.Ward", "Ward")
                        .WithMany("Properties")
                        .HasForeignKey("WardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RealEstate.Data.Entity.PropertyImage", b =>
                {
                    b.HasOne("RealEstate.Data.Entity.Property", "Property")
                        .WithMany("PropertyImages")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RealEstate.Data.Entity.Ward", b =>
                {
                    b.HasOne("RealEstate.Data.Entity.District", "District")
                        .WithMany("Wards")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
