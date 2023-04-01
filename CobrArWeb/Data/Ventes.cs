using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CobrArWeb.Data
{
    public class Ventes
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string? CodeBarre { get; set; }

        public string Produit { get; set; }

        public string Categorie { get; set; }

        public string SousCategorie { get; set; }

        public string Equipe { get; set; }

        public string? Taille { get; set; }

        public int? Quantite { get; set; }

        public decimal? Prix { get; set; }

        public string? Fournisseur { get; set; }

        public int MDPId { get; set; }
        public string MDPNom { get; set; }
        public virtual MDP MDP { get; set; }

        public decimal? AjustementPrix { get; set; }

        public int? MDPId2 { get; set; }
        public string? MDPNom2 { get; set; }
    }
}
