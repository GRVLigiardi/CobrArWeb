using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CobrArWeb.Models
{
    public class AddProductViewModel
    {
        [Required(ErrorMessage = "Le champ 'Equipe' est obligatoire.")]
        public string Equipe { get; set; }

        [Required(ErrorMessage = "Le champ 'Catégorie' est obligatoire.")]
        public string Categorie { get; set; }

        [Required(ErrorMessage = "Le champ 'Sous-catégorie' est obligatoire.")]
        public string SousCategorie { get; set; }

        [Required(ErrorMessage = "Le champ 'Fournisseur' est obligatoire.")]
        public string Fournisseur { get; set; }

        [Required(ErrorMessage = "Le champ 'Nom' est obligatoire.")]
        public string Nom { get; set; }

        
        public int? Quantite { get; set; }

        
        public string Taille { get; set; }

        [Required(ErrorMessage = "Le champ 'Prix' est obligatoire.")]
        public decimal? Prix { get; set; }

        public IEnumerable<string> Equipes { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<string> SousCategories { get; set; }
        public IEnumerable<string> Fournisseurs { get; set; }
        public IEnumerable<string> Tailles { get; set; }
    }
}