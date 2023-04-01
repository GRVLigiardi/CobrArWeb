using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CobrArWeb.Data
{
    public class SousCategorie
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        [ForeignKey("Categorie")] 
        public int CategorieId { get; set; }
        public Categorie Category { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}