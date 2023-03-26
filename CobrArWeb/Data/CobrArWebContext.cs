using CobrArWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace CobrArWeb.Data
{
    public class CobrArWebContext : DbContext
    {
        public CobrArWebContext(DbContextOptions<CobrArWebContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Ventes> Ventes { get; set; }
        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<SousCategorie> SousCategories { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
        public DbSet<Taille> Tailles { get; set; }

    }
}
