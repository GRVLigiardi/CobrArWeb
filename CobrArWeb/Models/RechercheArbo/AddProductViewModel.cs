using System.Collections.Generic;
using CobrArWeb.Data;

namespace CobrArWeb.Models.RechercheArbo
{
    public class AddProductViewModel
    {
        public string ProductName { get; set; }
        public int SelectedCategoryId { get; set; }
        public int SelectedSubcategoryId { get; set; }
        public int SelectedTeamId { get; set; }
        public int SelectedSizeId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int SelectedSupplierId { get; set; }

        public List<EquipeViewModel> EquipesViewModel { get; set; }
        public List<CategoryViewModel> CategoriesViewModel { get; set; }
        public List<SousCategorieViewModel> SubcategoriesViewModel { get; set; }

        public List<Categorie> Categories { get; set; }
        public List<SousCategorie> Subcategories { get; set; }
        public List<Equipe> Teams { get; set; }
        public List<Taille> Sizes { get; set; }
        public List<Fournisseur> Suppliers { get; set; }
    }

   
}
