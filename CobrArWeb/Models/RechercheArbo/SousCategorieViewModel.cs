using CobrArWeb.Data;

namespace CobrArWeb.Models.RechercheArbo
{
    public class SousCategorieViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public EquipeViewModel Equipe { get; set; }
        public List<SousCategorieViewModel> SousCategories { get; set; }

        public static List<SousCategorieViewModel> Clean(List<Product> produits, EquipeViewModel equipe)
        {
            var result = new List<SousCategorieViewModel>();
            foreach (var item in produits.GroupBy(c => c.SousCategorie.Nom))
            {
                result.Add(new SousCategorieViewModel
                {
                    Id = item.First().SousCategorie.Id,
                    Name = item.Key,
                    Products = item.ToList(),
                    Equipe = equipe
                });
            }
            return result;
        }
    }
}