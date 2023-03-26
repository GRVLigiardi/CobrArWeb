using System.Collections.Generic;

namespace CobrArWeb.Data
{
    public class Fournisseur
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
