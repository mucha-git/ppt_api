﻿// <auto-generated />
using System;
using FirebirdSql.EntityFrameworkCore.Firebird.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Helpers;

#nullable disable

namespace WebApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 31);

            modelBuilder.Entity("WebApi.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AcceptTerms")
                        .HasColumnType("BOOLEAN");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FirstName")
                        .HasColumnType("varchar(80)");

                    b.Property<string>("LastName")
                        .HasColumnType("varchar(80)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("varchar(1000)");

                    b.Property<DateTime?>("PasswordReset")
                        .HasColumnType("TIMESTAMP");

                    b.Property<int?>("PilgrimageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Random")
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("ResetToken")
                        .HasColumnType("varchar(1000)");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("TIMESTAMP");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("varchar(80)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("varchar(1000)");

                    b.Property<DateTime?>("Verified")
                        .HasColumnType("TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("PilgrimageId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("WebApi.Entities.Coordinates", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Latitude")
                        .HasColumnType("DOUBLE PRECISION");

                    b.Property<double>("Longitude")
                        .HasColumnType("DOUBLE PRECISION");

                    b.HasKey("Id");

                    b.ToTable("Coordinates");
                });

            modelBuilder.Entity("WebApi.Entities.Elements", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Autoplay")
                        .HasColumnType("BOOLEAN");

                    b.Property<string>("Color")
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("DestinationViewId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Height")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImgSrc")
                        .HasColumnType("varchar(1000)");

                    b.Property<int?>("MapId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Margin")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Playlist")
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Text")
                        .HasColumnType("BLOB SUB_TYPE TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ViewId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YearId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("mapHeight")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MapId");

                    b.HasIndex("ViewId");

                    b.HasIndex("YearId");

                    b.ToTable("Elements");
                });

            modelBuilder.Entity("WebApi.Entities.MapPins", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Height")
                        .HasColumnType("INTEGER");

                    b.Property<string>("IconSrc")
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("PinSrc")
                        .HasColumnType("varchar(1000)");

                    b.Property<int>("Width")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YearId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("YearId");

                    b.ToTable("MapPins");
                });

            modelBuilder.Entity("WebApi.Entities.Maps", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("Delta")
                        .HasColumnType("DOUBLE PRECISION");

                    b.Property<double>("Latitude")
                        .HasColumnType("DOUBLE PRECISION");

                    b.Property<double>("Longitude")
                        .HasColumnType("DOUBLE PRECISION");

                    b.Property<string>("MapSrc")
                        .HasColumnType("BLOB SUB_TYPE TEXT");

                    b.Property<string>("MarkersString")
                        .HasColumnType("BLOB SUB_TYPE TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Polylines")
                        .HasColumnType("BLOB SUB_TYPE TEXT");

                    b.Property<string>("Provider")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("StrokeColor")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("StrokeWidth")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YearId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("YearId");

                    b.ToTable("Maps");
                });

            modelBuilder.Entity("WebApi.Entities.Markers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("FooterColor")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("FooterText")
                        .HasColumnType("varchar(100)");

                    b.Property<double>("Latitude")
                        .HasColumnType("DOUBLE PRECISION");

                    b.Property<double>("Longitude")
                        .HasColumnType("DOUBLE PRECISION");

                    b.Property<int?>("MapsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PinId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StrokeWidth")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("MapsId");

                    b.ToTable("Markers");
                });

            modelBuilder.Entity("WebApi.Entities.Pilgrimages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LogoSrc")
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(500)");

                    b.Property<string>("OneSignal")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("OneSignalApiKey")
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("isActive")
                        .HasColumnType("BOOLEAN");

                    b.HasKey("Id");

                    b.ToTable("Pilgrimages");
                });

            modelBuilder.Entity("WebApi.Entities.Views", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ExternalUrl")
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("HeaderText")
                        .HasColumnType("varchar(80)");

                    b.Property<string>("ImgSrc")
                        .HasColumnType("varchar(1000)");

                    b.Property<int?>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ScreenType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("varchar(80)");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ViewId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YearId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ViewId");

                    b.HasIndex("YearId");

                    b.ToTable("Views");
                });

            modelBuilder.Entity("WebApi.Entities.Years", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImgSrc")
                        .HasColumnType("varchar(1000)");

                    b.Property<int>("PilgrimageId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("Version")
                        .HasColumnType("CHAR(16) CHARACTER SET OCTETS");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.Property<string>("YearTopic")
                        .HasColumnType("varchar(500)");

                    b.Property<bool>("isActive")
                        .HasColumnType("BOOLEAN");

                    b.HasKey("Id");

                    b.HasIndex("PilgrimageId");

                    b.ToTable("Years");
                });

            modelBuilder.Entity("WebApi.Entities.Account", b =>
                {
                    b.HasOne("WebApi.Entities.Pilgrimages", "Pilgrimage")
                        .WithMany("Accounts")
                        .HasForeignKey("PilgrimageId");

                    b.OwnsMany("WebApi.Entities.RefreshToken", "RefreshTokens", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("INTEGER")
                                .HasAnnotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("AccountId")
                                .HasColumnType("INTEGER");

                            b1.Property<DateTime>("Created")
                                .HasColumnType("TIMESTAMP");

                            b1.Property<string>("CreatedByIp")
                                .HasColumnType("varchar(80)");

                            b1.Property<DateTime>("Expires")
                                .HasColumnType("TIMESTAMP");

                            b1.Property<string>("ReasonRevoked")
                                .HasColumnType("varchar(80)");

                            b1.Property<string>("ReplacedByToken")
                                .HasColumnType("varchar(1000)");

                            b1.Property<DateTime?>("Revoked")
                                .HasColumnType("TIMESTAMP");

                            b1.Property<string>("RevokedByIp")
                                .HasColumnType("varchar(80)");

                            b1.Property<string>("Token")
                                .HasColumnType("varchar(1000)");

                            b1.HasKey("Id");

                            b1.HasIndex("AccountId");

                            b1.ToTable("RefreshToken");

                            b1.WithOwner("Account")
                                .HasForeignKey("AccountId");

                            b1.Navigation("Account");
                        });

                    b.Navigation("Pilgrimage");

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("WebApi.Entities.Elements", b =>
                {
                    b.HasOne("WebApi.Entities.Maps", "Map")
                        .WithMany()
                        .HasForeignKey("MapId");

                    b.HasOne("WebApi.Entities.Views", "View")
                        .WithMany()
                        .HasForeignKey("ViewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Entities.Years", "Year")
                        .WithMany("Elements")
                        .HasForeignKey("YearId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Map");

                    b.Navigation("View");

                    b.Navigation("Year");
                });

            modelBuilder.Entity("WebApi.Entities.MapPins", b =>
                {
                    b.HasOne("WebApi.Entities.Years", "Year")
                        .WithMany("MapPins")
                        .HasForeignKey("YearId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Year");
                });

            modelBuilder.Entity("WebApi.Entities.Maps", b =>
                {
                    b.HasOne("WebApi.Entities.Years", "Year")
                        .WithMany("Maps")
                        .HasForeignKey("YearId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Year");
                });

            modelBuilder.Entity("WebApi.Entities.Markers", b =>
                {
                    b.HasOne("WebApi.Entities.Maps", null)
                        .WithMany("Markers")
                        .HasForeignKey("MapsId");
                });

            modelBuilder.Entity("WebApi.Entities.Views", b =>
                {
                    b.HasOne("WebApi.Entities.Views", "View")
                        .WithMany("ViewsList")
                        .HasForeignKey("ViewId");

                    b.HasOne("WebApi.Entities.Years", "Year")
                        .WithMany("Views")
                        .HasForeignKey("YearId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("View");

                    b.Navigation("Year");
                });

            modelBuilder.Entity("WebApi.Entities.Years", b =>
                {
                    b.HasOne("WebApi.Entities.Pilgrimages", "Pilgrimage")
                        .WithMany("Years")
                        .HasForeignKey("PilgrimageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pilgrimage");
                });

            modelBuilder.Entity("WebApi.Entities.Maps", b =>
                {
                    b.Navigation("Markers");
                });

            modelBuilder.Entity("WebApi.Entities.Pilgrimages", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("Years");
                });

            modelBuilder.Entity("WebApi.Entities.Views", b =>
                {
                    b.Navigation("ViewsList");
                });

            modelBuilder.Entity("WebApi.Entities.Years", b =>
                {
                    b.Navigation("Elements");

                    b.Navigation("MapPins");

                    b.Navigation("Maps");

                    b.Navigation("Views");
                });
#pragma warning restore 612, 618
        }
    }
}
