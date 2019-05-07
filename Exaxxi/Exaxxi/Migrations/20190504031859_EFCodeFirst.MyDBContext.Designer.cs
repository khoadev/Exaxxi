﻿// <auto-generated />
using System;
using Exaxxi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Exaxxi.Migrations
{
    [DbContext(typeof(ExaxxiDbContext))]
    [Migration("20190504031859_EFCodeFirst.MyDBContext")]
    partial class EFCodeFirstMyDBContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Exaxxi.Models.Admins", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<DateTime>("date_create");

                    b.Property<string>("email")
                        .IsRequired();

                    b.Property<int>("level");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Exaxxi.Models.Brands", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<int>("id_department");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("order");

                    b.HasKey("id");

                    b.HasIndex("id_department");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Exaxxi.Models.Categories", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<string>("en_name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("id_brand");

                    b.Property<int>("order");

                    b.Property<string>("vi_name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("id");

                    b.HasIndex("id_brand");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Exaxxi.Models.Departments", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<string>("en_name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("order");

                    b.Property<string>("vi_name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Exaxxi.Models.ds_Size", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Centimet");

                    b.Property<float>("Inch");

                    b.Property<float>("UK");

                    b.Property<float>("US");

                    b.Property<int>("VN");

                    b.HasKey("id");

                    b.ToTable("ds_Size");
                });

            modelBuilder.Entity("Exaxxi.Models.Followings", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("id_size");

                    b.Property<int>("id_user");

                    b.HasKey("id");

                    b.HasIndex("id_size");

                    b.HasIndex("id_user");

                    b.ToTable("Followings");
                });

            modelBuilder.Entity("Exaxxi.Models.Items", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<string>("en_info");

                    b.Property<int>("id_admin");

                    b.Property<int>("id_category");

                    b.Property<string>("img")
                        .IsRequired();

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<double>("trade_max");

                    b.Property<double>("trade_min");

                    b.Property<string>("vi_info");

                    b.Property<float>("volatility");

                    b.HasKey("id");

                    b.HasIndex("id_admin");

                    b.HasIndex("id_category");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Exaxxi.Models.News", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<DateTime>("date_create");

                    b.Property<string>("en_content")
                        .IsRequired();

                    b.Property<string>("en_title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("id_admin");

                    b.Property<int>("id_department");

                    b.Property<string>("vi_content")
                        .IsRequired();

                    b.Property<string>("vi_title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("id");

                    b.HasIndex("id_admin");

                    b.HasIndex("id_department");

                    b.ToTable("News");
                });

            modelBuilder.Entity("Exaxxi.Models.Posts", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("date_end");

                    b.Property<DateTime>("date_start");

                    b.Property<int>("id_size");

                    b.Property<int>("id_user");

                    b.Property<int>("kind");

                    b.Property<double>("price");

                    b.HasKey("id");

                    b.HasIndex("id_size");

                    b.HasIndex("id_user");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Exaxxi.Models.Sizes", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Adminsid");

                    b.Property<double>("highest_bid");

                    b.Property<int>("id_ds_size");

                    b.Property<int>("id_item");

                    b.Property<double>("last_sale");

                    b.Property<double>("lowest_ask");

                    b.HasKey("id");

                    b.HasIndex("Adminsid");

                    b.HasIndex("id_ds_size");

                    b.HasIndex("id_item");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("Exaxxi.Models.Users", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<string>("confirm_password")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<DateTime>("date_registion");

                    b.Property<string>("email")
                        .IsRequired();

                    b.Property<int>("level_seller");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int>("score_buyer");

                    b.Property<int?>("shoe_sizeid");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("id");

                    b.HasIndex("shoe_sizeid");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Exaxxi.Models.Brands", b =>
                {
                    b.HasOne("Exaxxi.Models.Departments", "department")
                        .WithMany("brands")
                        .HasForeignKey("id_department")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Exaxxi.Models.Categories", b =>
                {
                    b.HasOne("Exaxxi.Models.Brands", "brand")
                        .WithMany("categories")
                        .HasForeignKey("id_brand")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Exaxxi.Models.Followings", b =>
                {
                    b.HasOne("Exaxxi.Models.Sizes", "wish")
                        .WithMany("followings")
                        .HasForeignKey("id_size")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Exaxxi.Models.Users", "user")
                        .WithMany("followings")
                        .HasForeignKey("id_user")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Exaxxi.Models.Items", b =>
                {
                    b.HasOne("Exaxxi.Models.Admins", "admin")
                        .WithMany("items")
                        .HasForeignKey("id_admin")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Exaxxi.Models.Categories", "category")
                        .WithMany("items")
                        .HasForeignKey("id_category")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Exaxxi.Models.News", b =>
                {
                    b.HasOne("Exaxxi.Models.Admins", "admin")
                        .WithMany("news")
                        .HasForeignKey("id_admin")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Exaxxi.Models.Departments", "department")
                        .WithMany("news")
                        .HasForeignKey("id_department")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Exaxxi.Models.Posts", b =>
                {
                    b.HasOne("Exaxxi.Models.Sizes", "size")
                        .WithMany("posts")
                        .HasForeignKey("id_size")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Exaxxi.Models.Users", "user")
                        .WithMany("posts")
                        .HasForeignKey("id_user")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Exaxxi.Models.Sizes", b =>
                {
                    b.HasOne("Exaxxi.Models.Admins")
                        .WithMany("sizes")
                        .HasForeignKey("Adminsid");

                    b.HasOne("Exaxxi.Models.ds_Size", "size")
                        .WithMany()
                        .HasForeignKey("id_ds_size")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Exaxxi.Models.Items", "item")
                        .WithMany("sizes")
                        .HasForeignKey("id_item")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Exaxxi.Models.Users", b =>
                {
                    b.HasOne("Exaxxi.Models.ds_Size", "shoe_size")
                        .WithMany()
                        .HasForeignKey("shoe_sizeid");
                });
#pragma warning restore 612, 618
        }
    }
}
