﻿// <auto-generated />
using System;
using CobrArWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CobrArWeb.Migrations
{
    [DbContext(typeof(CobrArWebContext))]
    partial class CobrArWebContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CobrArWeb.Data.Categorie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CobrArWeb.Data.Equipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Equipes");
                });

            modelBuilder.Entity("CobrArWeb.Data.Fournisseur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Fournisseurs");
                });

            modelBuilder.Entity("CobrArWeb.Data.MDP", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MDPs");
                });

            modelBuilder.Entity("CobrArWeb.Data.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategorieId")
                        .HasColumnType("int");

                    b.Property<int>("EquipeId")
                        .HasColumnType("int");

                    b.Property<int?>("FournisseurId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<decimal?>("Prix")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Produit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Quantite")
                        .HasColumnType("int");

                    b.Property<int>("SousCategorieId")
                        .HasColumnType("int");

                    b.Property<int?>("TailleId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategorieId");

                    b.HasIndex("EquipeId");

                    b.HasIndex("FournisseurId");

                    b.HasIndex("SousCategorieId");

                    b.HasIndex("TailleId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CobrArWeb.Data.SousCategorie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategorieId")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategorieId");

                    b.ToTable("SousCategories");
                });

            modelBuilder.Entity("CobrArWeb.Data.Taille", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tailles");
                });

            modelBuilder.Entity("CobrArWeb.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CobrArWeb.Data.Ventes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("AjustementPrix")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Categorie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodeBarre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Equipe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fournisseur")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MDPId")
                        .HasColumnType("int");

                    b.Property<int?>("MDPId2")
                        .HasColumnType("int");

                    b.Property<string>("MDPNom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MDPNom2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Prix")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Produit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Quantite")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("SousCategorie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Taille")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MDPId");

                    b.HasIndex("ProductId");

                    b.ToTable("Ventes");
                });

            modelBuilder.Entity("CobrArWeb.Data.Product", b =>
                {
                    b.HasOne("CobrArWeb.Data.Categorie", "Categorie")
                        .WithMany("Products")
                        .HasForeignKey("CategorieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CobrArWeb.Data.Equipe", "Equipe")
                        .WithMany("Products")
                        .HasForeignKey("EquipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CobrArWeb.Data.Fournisseur", "Fournisseur")
                        .WithMany("Products")
                        .HasForeignKey("FournisseurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CobrArWeb.Data.SousCategorie", "SousCategorie")
                        .WithMany("Products")
                        .HasForeignKey("SousCategorieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CobrArWeb.Data.Taille", "Taille")
                        .WithMany("Products")
                        .HasForeignKey("TailleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categorie");

                    b.Navigation("Equipe");

                    b.Navigation("Fournisseur");

                    b.Navigation("SousCategorie");

                    b.Navigation("Taille");
                });

            modelBuilder.Entity("CobrArWeb.Data.SousCategorie", b =>
                {
                    b.HasOne("CobrArWeb.Data.Categorie", "Category")
                        .WithMany("SousCategories")
                        .HasForeignKey("CategorieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CobrArWeb.Data.Ventes", b =>
                {
                    b.HasOne("CobrArWeb.Data.MDP", "MDP")
                        .WithMany()
                        .HasForeignKey("MDPId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CobrArWeb.Data.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MDP");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CobrArWeb.Data.Categorie", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("SousCategories");
                });

            modelBuilder.Entity("CobrArWeb.Data.Equipe", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("CobrArWeb.Data.Fournisseur", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("CobrArWeb.Data.SousCategorie", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("CobrArWeb.Data.Taille", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
