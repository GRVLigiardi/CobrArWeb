using CobrArWeb.Data;

namespace CobrArWeb.Models.RechercheArbo
{
    public class SousCategorieViewModel
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public EquipeViewModel Equipe { get; set; }
        public List<SousCategorieViewModel> SousCategories { get; set; }

        public static List<SousCategorieViewModel> Clean(List<Product> produits, EquipeViewModel equipe)
        {
            var result = new List<SousCategorieViewModel>();
            foreach (var item in produits.GroupBy(c => c.SousCategorie))
            {
                result.Add(new SousCategorieViewModel
                {
                    Name = item.Key,
                    Products = item.ToList(),
                    Equipe = equipe
                });
            }
            return result;
        }
    }
}