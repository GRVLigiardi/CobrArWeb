using CobrArWeb.Data;

public class AddProductViewModel
{
    public Product NewProduct { get; set; }
    public List<Product> Products { get; set; }
    public List<Equipe> Equipes { get; set; }
    public List<Categorie> Categories { get; set; }
    public List<Fournisseur> Fournisseurs { get; set; }
}