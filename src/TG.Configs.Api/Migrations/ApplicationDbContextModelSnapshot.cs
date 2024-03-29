﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TG.Configs.Api.Db;

#nullable disable

namespace TG.Configs.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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

                    b.Property<string>("TgApp")
                        .HasColumnType("text")
                        .HasColumnName("tg_app");

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
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<int>("Format")
                        .HasColumnType("integer")
                        .HasColumnName("format");

                    b.Property<bool>("HasSecrets")
                        .HasColumnType("boolean")
                        .HasColumnName("has_secrets");

                    b.Property<string>("Secret")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("secret");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.HasKey("Id")
                        .HasName("pk_configs");

                    b.ToTable("configs");
                });

            modelBuilder.Entity("TG.Configs.Api.Entities.ConfigVariable", b =>
                {
                    b.Property<string>("ConfigId")
                        .HasColumnType("text")
                        .HasColumnName("config_id");

                    b.Property<string>("Key")
                        .HasColumnType("text")
                        .HasColumnName("key");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("ConfigId", "Key")
                        .HasName("pk_config_variables");

                    b.ToTable("config_variables");
                });

            modelBuilder.Entity("TG.Configs.Api.Entities.Callback", b =>
                {
                    b.HasOne("TG.Configs.Api.Entities.Config", null)
                        .WithMany("Callbacks")
                        .HasForeignKey("ConfigId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_callbacks_configs_config_id");
                });

            modelBuilder.Entity("TG.Configs.Api.Entities.ConfigVariable", b =>
                {
                    b.HasOne("TG.Configs.Api.Entities.Config", "Config")
                        .WithMany("Variables")
                        .HasForeignKey("ConfigId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_config_variables_configs_config_id");

                    b.Navigation("Config");
                });

            modelBuilder.Entity("TG.Configs.Api.Entities.Config", b =>
                {
                    b.Navigation("Callbacks");

                    b.Navigation("Variables");
                });
#pragma warning restore 612, 618
        }
    }
}
