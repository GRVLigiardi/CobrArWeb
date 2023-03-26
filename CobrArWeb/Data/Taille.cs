namespace CobrArWeb.Data
{
    public class Taille
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
