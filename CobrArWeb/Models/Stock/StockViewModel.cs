using CobrArWeb.Data;
using CobrArWeb.Models.RechercheArbo;

namespace CobrArWeb.Models.Stock
{
    public class StockViewModel
    {
        public Product NewProduct { get; set; }
        public List<Product> Products { get; set; }
        public List<Equipe> Equipes { get; set; }
        public List<Categorie> Categories { get; set; }
        public List<SousCategorie> SousCategories { get; set; }
        public List<Taille> Tailles { get; set; }
        public List<Fournisseur> Fournisseurs { get; set; }

        public string Equipe { get; set; }

        public List<CategoryViewModel> Categorie { get; set; }
        public List<EquipeViewModel> EquipeViewModelList { get; set; }

        public ListViewModel ListViewModel { get; set; }

 
    }
}