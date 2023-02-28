namespace CobrArWeb.Data
{
    public class Product
    {
        public string CodeBarre { get; set; }
        public int Id { get; set; }
        public string Produit { get; set; }

        public string Categorie { get; set; }
        public string SousCategorie { get; set; }
        public string Taille { get; set; }
        public int Quantite { get; set; }
        public string Prix { get; set; }
        public string Fournisseur { get; set; }
    }
}