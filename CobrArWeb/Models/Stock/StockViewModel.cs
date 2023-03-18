using CobrArWeb.Data;

namespace CobrArWeb.Models.Stock
{
    public class StockViewModel
    {
        public Product NewProduct { get; set; }
        public List<Product> Products { get; set; }
        // Ajoutez les propriétés manquantes ici.
        public string CodeBarre { get; set; }
        public string Categorie { get; set; }
        public string SousCategorie { get; set; }
        public string Taille { get; set; }
        public string Fournisseur { get; set; }
        public int? Quantite { get; set; }
        public decimal? Prix { get; set; }
    }
}