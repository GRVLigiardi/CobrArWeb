namespace CobrArWeb.Data
{
    //public class Team
    //{
    //    public Team(List<Category> categories)
    //    {
    //        Categories = categories;
    //    }

    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public List<Category> Categories { get; set; }
    //}

    //public class Category
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public List<SubCategory> SousCategories { get; set; }
    //}

    //public class SubCategory
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public List<Product> Products { get; set; }
    //}

    public class Product
    {
        public int Id { get; set; }
        public string CodeBarre { get; set; }
        public string Produit { get; set; }
        public string Categorie { get; set; }
        public string SousCategorie { get; set; }
        public string Equipe { get; set; }
        public string Taille { get; set; }
        public int? Quantite { get; set; }
        public decimal? Prix { get; set; }
        public string Fournisseur { get; set; }
    }
}