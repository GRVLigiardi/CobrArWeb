using CobrArWeb.Data;

public class CategoryViewModel
{
    public string Categorie { get; set; }
    public string SousCategorie { get; set; }
    public List<Product> Products { get; set; }
}


public class SousCategoryViewModel
{
    public string Equipe { get; set; }
    public string SousCategorie { get; set; }
    public List<Product> Products { get; set; }
}