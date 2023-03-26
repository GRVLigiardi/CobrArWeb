using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CobrArWeb.Data
{
    public class Equipe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
