﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TG.Configs.Api.Db;

namespace TG.Configs.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211018113921_Format")]
    partial class Format
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("TG.Configs.Api.Entities.Callback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("ConfigId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("config_id");

                    b.Property<string>("Url")
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.HasKey("Id")
                        .HasName("pk_callbacks");

                    b.HasIndex("ConfigId")
                        .HasDatabaseName("ix_callbacks_config_id");

                    b.ToTable("callbacks");
                });

            modelBuilder.Entity("TG.Configs.Api.Entities.Config", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<int>("Format")
                        .HasColumnType("integer")
                        .HasColumnName("format");

                    b.Property<string>("Secret")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("secret");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.HasKey("Id")
                        .HasName("pk_configs");

                    b.ToTable("configs");
                });

            modelBuilder.Entity("TG.Configs.Api.Entities.Callback", b =>
                {
                    b.HasOne("TG.Configs.Api.Entities.Config", null)
                        .WithMany("Callbacks")
                        .HasForeignKey("ConfigId")
                        .HasConstraintName("fk_callbacks_configs_config_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TG.Configs.Api.Entities.Config", b =>
                {
                    b.Navigation("Callbacks");
                });
#pragma warning restore 612, 618
        }
    }
}
