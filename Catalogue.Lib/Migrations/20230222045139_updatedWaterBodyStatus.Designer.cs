﻿// <auto-generated />
using System;
using Catalogue.Lib.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Catalogue.Lib.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230222045139_updatedWaterBodyStatus")]
    partial class updatedWaterBodyStatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Catalogue.Lib.Models.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Catalogue.Lib.Models.Entities.DataSync", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("SyncedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TotalCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DataSyncs");
                });

            modelBuilder.Entity("Catalogue.Lib.Models.Entities.FileUpload", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("filePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FileUploads");
                });

            modelBuilder.Entity("Catalogue.Lib.Models.Entities.WaterBodyDetectionData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("HasBeenVisited")
                        .HasColumnType("bit");

                    b.Property<bool>("IsWaterBodyPresent")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastTimeUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LastUpdatedBy")
                        .HasColumnType("int");

                    b.Property<string>("crs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("featureGometry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("featureProperties")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("featureType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("fileId")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WaterBodyDetectionDatas");
                });

            modelBuilder.Entity("Catalogue.Lib.Models.Entities.WaterBodyPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("AREA_SQM")
                        .HasColumnType("float");

                    b.Property<string>("CONFIDENCE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Depression")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasBeenVisited")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAbateKnownPoint")
                        .HasColumnType("bit");

                    b.Property<bool>("IsWaterBodyPresent")
                        .HasColumnType("bit");

                    b.Property<double>("LATITUDE")
                        .HasColumnType("float");

                    b.Property<double>("LONGITUDE")
                        .HasColumnType("float");

                    b.Property<DateTime?>("LastTimeVisisted")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LastUpdatedBy")
                        .HasColumnType("int");

                    b.Property<string>("LastUpdatedByName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastVisistedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OBJECTID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PHASE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("SHAPE_Area")
                        .HasColumnType("float");

                    b.Property<double>("SHAPE_Leng")
                        .HasColumnType("float");

                    b.Property<string>("UNIQUE_ID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WaterBodyStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WaterBodyPoints");
                });
#pragma warning restore 612, 618
        }
    }
}
