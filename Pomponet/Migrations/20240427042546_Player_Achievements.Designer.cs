﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pomponet.Context;

#nullable disable

namespace Pomponet.Migrations
{
    [DbContext(typeof(CropsDbContext))]
    [Migration("20240427042546_Player_Achievements")]
    partial class Player_Achievements
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Pomponet.Model.Achievements", b =>
                {
                    b.Property<int>("Id_Achievement")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Achievement"));

                    b.Property<string>("Achievement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.HasKey("Id_Achievement");

                    b.ToTable("Achievements");
                });

            modelBuilder.Entity("Pomponet.Model.AplicationTools", b =>
                {
                    b.Property<int>("Id_AplicationTool")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_AplicationTool"));

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Quantity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tool")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_AplicationTool");

                    b.ToTable("AplicationTools");
                });

            modelBuilder.Entity("Pomponet.Model.Crops", b =>
                {
                    b.Property<int>("Id_Crop")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Crop"));

                    b.Property<int>("Crop_Number")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int>("Id_Player")
                        .HasColumnType("int");

                    b.Property<int?>("PlayersId_Player")
                        .HasColumnType("int");

                    b.HasKey("Id_Crop");

                    b.HasIndex("PlayersId_Player");

                    b.ToTable("Crop");
                });

            modelBuilder.Entity("Pomponet.Model.Epps", b =>
                {
                    b.Property<int>("Id_Epp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Epp"));

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name_Epp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Epp");

                    b.ToTable("Epps");
                });

            modelBuilder.Entity("Pomponet.Model.Fungicide_X_Pompon_Part", b =>
                {
                    b.Property<int>("Id_Fungicide_X_Pompon_Part")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Fungicide_X_Pompon_Part"));

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int?>("FungicidesId_Fungicide")
                        .HasColumnType("int");

                    b.Property<int>("Id_Fungicide")
                        .HasColumnType("int");

                    b.Property<int>("Id_Pompon_Part")
                        .HasColumnType("int");

                    b.Property<int?>("Pompon_PartsId_Pompon_Part")
                        .HasColumnType("int");

                    b.HasKey("Id_Fungicide_X_Pompon_Part");

                    b.HasIndex("FungicidesId_Fungicide");

                    b.HasIndex("Pompon_PartsId_Pompon_Part");

                    b.ToTable("Fungicide_X_Pompon_Part");
                });

            modelBuilder.Entity("Pomponet.Model.Fungicides", b =>
                {
                    b.Property<int>("Id_Fungicide")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Fungicide"));

                    b.Property<int?>("CropsId_Crop")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id_crop")
                        .HasColumnType("int");

                    b.Property<string>("Name_Fungicide")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id_Fungicide");

                    b.HasIndex("CropsId_Crop");

                    b.ToTable("Fungicides");
                });

            modelBuilder.Entity("Pomponet.Model.Inventories", b =>
                {
                    b.Property<int>("Id_Inventory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Inventory"));

                    b.Property<int?>("Aplication_ToolsId_AplicationTool")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int?>("EppsId_Epp")
                        .HasColumnType("int");

                    b.Property<int>("Id_Epp")
                        .HasColumnType("int");

                    b.Property<int>("Id_Person")
                        .HasColumnType("int");

                    b.Property<int>("Id_Tool")
                        .HasColumnType("int");

                    b.Property<int>("Number_Inventorie")
                        .HasColumnType("int");

                    b.Property<int?>("PeopleId_Person")
                        .HasColumnType("int");

                    b.HasKey("Id_Inventory");

                    b.HasIndex("Aplication_ToolsId_AplicationTool");

                    b.HasIndex("EppsId_Epp");

                    b.HasIndex("PeopleId_Person");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("Pomponet.Model.Money", b =>
                {
                    b.Property<int>("Id_Money")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Money"));

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int>("Id_Player")
                        .HasColumnType("int");

                    b.Property<int?>("PlayersId_Player")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id_Money");

                    b.HasIndex("PlayersId_Player");

                    b.ToTable("Money");
                });

            modelBuilder.Entity("Pomponet.Model.People", b =>
                {
                    b.Property<int>("Id_Person")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Person"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Names")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Person");

                    b.ToTable("People");
                });

            modelBuilder.Entity("Pomponet.Model.Pest_X_Fungicide", b =>
                {
                    b.Property<int>("Id_Pest_X_Fungicide")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Pest_X_Fungicide"));

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int?>("FungicidesId_Fungicide")
                        .HasColumnType("int");

                    b.Property<int>("Id_Fungicide")
                        .HasColumnType("int");

                    b.Property<int>("Id_Pest")
                        .HasColumnType("int");

                    b.Property<int?>("PestsId_Pest")
                        .HasColumnType("int");

                    b.HasKey("Id_Pest_X_Fungicide");

                    b.HasIndex("FungicidesId_Fungicide");

                    b.HasIndex("PestsId_Pest");

                    b.ToTable("Pest_X_Fungicide");
                });

            modelBuilder.Entity("Pomponet.Model.Pests", b =>
                {
                    b.Property<int>("Id_Pest")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Pest"));

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Pest")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Pest");

                    b.ToTable("Pests");
                });

            modelBuilder.Entity("Pomponet.Model.Player_Achievements", b =>
                {
                    b.Property<int>("Id_Player_Achievement")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Player_Achievement"));

                    b.Property<int?>("AchievementsId_Achievement")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int>("Id_Achievement")
                        .HasColumnType("int");

                    b.Property<int>("Id_Player")
                        .HasColumnType("int");

                    b.Property<int>("Logros_Totales")
                        .HasColumnType("int");

                    b.Property<int?>("PlayersId_Player")
                        .HasColumnType("int");

                    b.HasKey("Id_Player_Achievement");

                    b.HasIndex("AchievementsId_Achievement");

                    b.HasIndex("PlayersId_Player");

                    b.ToTable("Player_Achievements");
                });

            modelBuilder.Entity("Pomponet.Model.Players", b =>
                {
                    b.Property<int>("Id_Player")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Player"));

                    b.Property<int>("Id_Person")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PeopleId_Person")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Player");

                    b.HasIndex("PeopleId_Person");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Pomponet.Model.Pompon_Parts", b =>
                {
                    b.Property<int>("Id_Pompon_Part")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Pompon_Part"));

                    b.Property<string>("Part")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Pompon_Part");

                    b.ToTable("Pompon_Parts");
                });

            modelBuilder.Entity("Pomponet.Model.Sensors", b =>
                {
                    b.Property<int>("Id_Sensor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Sensor"));

                    b.Property<int?>("CropsId_Crop")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id_crop")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Sensor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Sensor");

                    b.HasIndex("CropsId_Crop");

                    b.ToTable("Sensors");
                });

            modelBuilder.Entity("Pomponet.Model.Types_Fungicides", b =>
                {
                    b.Property<int>("Id_Type_Fungicide")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Type_Fungicide"));

                    b.Property<int?>("FungicidesId_Fungicide")
                        .HasColumnType("int");

                    b.Property<int>("Id_Funicides")
                        .HasColumnType("int");

                    b.Property<string>("Type_Fungicide")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Type_Fungicide");

                    b.HasIndex("FungicidesId_Fungicide");

                    b.ToTable("Types_Fungicides");
                });

            modelBuilder.Entity("Pomponet.Model.Crops", b =>
                {
                    b.HasOne("Pomponet.Model.Players", "Players")
                        .WithMany()
                        .HasForeignKey("PlayersId_Player");

                    b.Navigation("Players");
                });

            modelBuilder.Entity("Pomponet.Model.Fungicide_X_Pompon_Part", b =>
                {
                    b.HasOne("Pomponet.Model.Fungicides", "Fungicides")
                        .WithMany()
                        .HasForeignKey("FungicidesId_Fungicide");

                    b.HasOne("Pomponet.Model.Pompon_Parts", "Pompon_Parts")
                        .WithMany()
                        .HasForeignKey("Pompon_PartsId_Pompon_Part");

                    b.Navigation("Fungicides");

                    b.Navigation("Pompon_Parts");
                });

            modelBuilder.Entity("Pomponet.Model.Fungicides", b =>
                {
                    b.HasOne("Pomponet.Model.Crops", "Crops")
                        .WithMany()
                        .HasForeignKey("CropsId_Crop");

                    b.Navigation("Crops");
                });

            modelBuilder.Entity("Pomponet.Model.Inventories", b =>
                {
                    b.HasOne("Pomponet.Model.AplicationTools", "Aplication_Tools")
                        .WithMany()
                        .HasForeignKey("Aplication_ToolsId_AplicationTool");

                    b.HasOne("Pomponet.Model.Epps", "Epps")
                        .WithMany()
                        .HasForeignKey("EppsId_Epp");

                    b.HasOne("Pomponet.Model.People", "People")
                        .WithMany()
                        .HasForeignKey("PeopleId_Person");

                    b.Navigation("Aplication_Tools");

                    b.Navigation("Epps");

                    b.Navigation("People");
                });

            modelBuilder.Entity("Pomponet.Model.Money", b =>
                {
                    b.HasOne("Pomponet.Model.Players", "Players")
                        .WithMany()
                        .HasForeignKey("PlayersId_Player");

                    b.Navigation("Players");
                });

            modelBuilder.Entity("Pomponet.Model.Pest_X_Fungicide", b =>
                {
                    b.HasOne("Pomponet.Model.Fungicides", "Fungicides")
                        .WithMany()
                        .HasForeignKey("FungicidesId_Fungicide");

                    b.HasOne("Pomponet.Model.Pests", "Pests")
                        .WithMany()
                        .HasForeignKey("PestsId_Pest");

                    b.Navigation("Fungicides");

                    b.Navigation("Pests");
                });

            modelBuilder.Entity("Pomponet.Model.Player_Achievements", b =>
                {
                    b.HasOne("Pomponet.Model.Achievements", "Achievements")
                        .WithMany()
                        .HasForeignKey("AchievementsId_Achievement");

                    b.HasOne("Pomponet.Model.Players", "Players")
                        .WithMany()
                        .HasForeignKey("PlayersId_Player");

                    b.Navigation("Achievements");

                    b.Navigation("Players");
                });

            modelBuilder.Entity("Pomponet.Model.Players", b =>
                {
                    b.HasOne("Pomponet.Model.People", "People")
                        .WithMany()
                        .HasForeignKey("PeopleId_Person");

                    b.Navigation("People");
                });

            modelBuilder.Entity("Pomponet.Model.Sensors", b =>
                {
                    b.HasOne("Pomponet.Model.Crops", "Crops")
                        .WithMany()
                        .HasForeignKey("CropsId_Crop");

                    b.Navigation("Crops");
                });

            modelBuilder.Entity("Pomponet.Model.Types_Fungicides", b =>
                {
                    b.HasOne("Pomponet.Model.Fungicides", "Fungicides")
                        .WithMany()
                        .HasForeignKey("FungicidesId_Fungicide");

                    b.Navigation("Fungicides");
                });
#pragma warning restore 612, 618
        }
    }
}