using CobrArWeb.Data;

namespace CobrArWeb.Models.Panier;

public class PanierViewModel
{
    public List<Equipe> Equipes { get; set; }
    public List<Categorie> Categories { get; set; }
    public List<SousCategorie> SousCategories { get; set; }
    public List<Product> Products { get; set; }
    public bool IsExchangeEnabled { get; set; }
}