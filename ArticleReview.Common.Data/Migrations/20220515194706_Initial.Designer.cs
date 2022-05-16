﻿// <auto-generated />
using System;
using ArticleReview.Common.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ArticleReview.Common.Data.Migrations
{
    [DbContext(typeof(ArticleReviewDbContext))]
    [Migration("20220515194706_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ArticleReview.Common.Data.Entities.ArticleEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ArticleContent")
                        .HasColumnType("text");

                    b.Property<string>("Author")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime?>("PublishDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("StarCount")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.HasKey("Id");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("ArticleReview.Common.Data.Entities.ReviewEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("ArticleId")
                        .HasColumnType("bigint");

                    b.Property<string>("ReviewContent")
                        .HasColumnType("text");

                    b.Property<string>("Reviewer")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("ArticleReview.Common.Data.Entities.ReviewEntity", b =>
                {
                    b.HasOne("ArticleReview.Common.Data.Entities.ArticleEntity", "Article")
                        .WithMany("Reviews")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");
                });

            modelBuilder.Entity("ArticleReview.Common.Data.Entities.ArticleEntity", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
