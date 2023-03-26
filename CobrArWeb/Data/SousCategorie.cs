using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CobrArWeb.Data
{
    public class SousCategorie
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int CategorieId { get; set; }
        public Categorie Category { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
