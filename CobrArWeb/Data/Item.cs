namespace CobrArWeb.Data
{
    /// <summary>
    /// Utilise dans le panier
    /// </summary>
    public class Item
    {
        public Product Product { get; set; }
        public Ventes Ventes { get; set; }
        public int Quantite { get; set; }

         
    }
}