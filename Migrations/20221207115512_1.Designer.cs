// <auto-generated />
using System;
using FirebirdSql.EntityFrameworkCore.Firebird.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Helpers;

#nullable disable

namespace WebApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221207115512_1")]
    partial class _1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("WebApi.Entities.Account", b =>
                {
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

                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
