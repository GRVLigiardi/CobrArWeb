﻿using CobrArWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace CobrArWeb.Data
{
    public class CobrArWebContext : DbContext
    {
        public CobrArWebContext(DbContextOptions<CobrArWebContext> options)
            : base(options)
        {
        }

        public CobrArWebContext()
           : base()
        {
        }

        public DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Ventes> Ventes { get; set; }
        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<SousCategorie> SousCategories { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
        public DbSet<Taille> Tailles { get; set; }
        public DbSet<MDP> MDPs { get; set; }
        public DbSet<ProductHistory> ProductHistories { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
}
