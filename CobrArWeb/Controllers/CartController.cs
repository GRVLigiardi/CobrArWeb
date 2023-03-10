using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class CartController : Controller
{
    public IActionResult AddToCart(string codeBarre)
    {
        // Vérifiez si la session de panier existe
        if (HttpContext.Session.GetString("Cart") == null)
        {
            // Si la session de panier n'existe pas, créez une nouvelle liste de produits
            List<string> cart = new List<string>();
            cart.Add(codeBarre);

            // Ajoutez la liste de produits dans la session de panier
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
        }
        else
        {
            // Si la session de panier existe, récupérez la liste de produits existants
            List<string> cart = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("Cart"));

            // Ajoutez le nouveau produit dans la liste de produits
            cart.Add(codeBarre);

            // Mettez à jour la session de panier
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
        }

        // Redirigez l'utilisateur vers la page de panier
        return RedirectToAction("Cart", "Cart");
    }
}
