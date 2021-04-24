﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Sher.Infrastructure.Data;

namespace Sher.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210423141339_AddDirectoriesAndMoveUploadersToOwnTable")]
    partial class AddDirectoriesAndMoveUploadersToOwnTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Sher.Core.Access.Platform.PlatformInstance", b =>
                {
                    b.Property<int>("_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.HasKey("_id");

                    b.ToTable("PlatformInstance");
                });

            modelBuilder.Entity("Sher.Core.Access.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Sher.Core.Access.Users.UserClient", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClient");
                });

            modelBuilder.Entity("Sher.Core.Files.Directories.Directory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid?>("ParentDirectoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UploaderId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("ParentDirectoryId");

                    b.HasIndex("UploaderId");

                    b.ToTable("Directory");
                });

            modelBuilder.Entity("Sher.Core.Files.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DirectoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<long>("Length")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("UploaderId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FileName");

                    b.HasIndex("UploaderId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Sher.Core.Files.Uploaders.Uploader", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Uploaders");
                });

            modelBuilder.Entity("Sher.Core.Access.Platform.PlatformInstance", b =>
                {
                    b.OwnsOne("Sher.Core.Access.Platform.PlatformSettings", "Settings", b1 =>
                        {
                            b1.Property<int>("PlatformInstance_id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .UseIdentityByDefaultColumn();

                            b1.Property<string>("InvitationCode")
                                .HasColumnType("text");

                            b1.HasKey("PlatformInstance_id");

                            b1.ToTable("PlatformInstance");

                            b1.WithOwner()
                                .HasForeignKey("PlatformInstance_id");
                        });

                    b.Navigation("Settings");
                });

            modelBuilder.Entity("Sher.Core.Access.Users.User", b =>
                {
                    b.OwnsOne("Sher.Core.Access.Users.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Hash")
                                .HasColumnType("text");

                            b1.Property<string>("Salt")
                                .HasColumnType("text");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsMany("Sher.Core.Access.Users.UserRole", "Roles", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .UseIdentityByDefaultColumn();

                            b1.Property<string>("Name")
                                .HasColumnType("text");

                            b1.HasKey("UserId", "Id");

                            b1.ToTable("UserRole");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Password");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Sher.Core.Access.Users.UserClient", b =>
                {
                    b.HasOne("Sher.Core.Access.Users.User", null)
                        .WithMany("Clients")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Sher.Core.Files.Directories.Directory", b =>
                {
                    b.HasOne("Sher.Core.Files.Directories.Directory", null)
                        .WithMany()
                        .HasForeignKey("ParentDirectoryId");

                    b.HasOne("Sher.Core.Files.Uploaders.Uploader", null)
                        .WithMany()
                        .HasForeignKey("UploaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sher.Core.Files.File", b =>
                {
                    b.HasOne("Sher.Core.Files.Uploaders.Uploader", null)
                        .WithMany()
                        .HasForeignKey("UploaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sher.Core.Files.Uploaders.Uploader", b =>
                {
                    b.HasOne("Sher.Core.Access.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sher.Core.Access.Users.User", b =>
                {
                    b.Navigation("Clients");
                });
#pragma warning restore 612, 618
        }
    }
}
