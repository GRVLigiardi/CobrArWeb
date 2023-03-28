using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CobrArWeb.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Produit { get; set; }

        [ForeignKey("Categorie")]
        public int CategorieId { get; set; }
        public virtual Categorie Categorie { get; set; }

        [ForeignKey("SousCategorie")]
        public int SousCategorieId { get; set; }
        public virtual SousCategorie SousCategorie { get; set; }

        [ForeignKey("Equipe")]
        public int EquipeId { get; set; }
        public virtual Equipe Equipe { get; set; }

        [ForeignKey("Taille")]
        public int? TailleId { get; set; }
        public virtual Taille Taille { get; set; }
        
        public int? Quantite { get; set; }
        public decimal? Prix { get; set; }

        [ForeignKey("Fournisseur")]
        public int? FournisseurId { get; set; }
        public virtual Fournisseur Fournisseur { get; set; }

        }
}
