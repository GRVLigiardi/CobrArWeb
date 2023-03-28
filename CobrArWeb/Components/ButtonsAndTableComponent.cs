using Microsoft.AspNetCore.Mvc;
using CobrArWeb.Data;
using System.Threading.Tasks;
using CobrArWeb.Models.RechercheArbo;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewComponents;

public class ButtonsAndTableComponent : ViewComponent
{
    private readonly CobrArWebContext _context;

    public ButtonsAndTableComponent(CobrArWebContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var equipes = await _context.Products
            .Include(p => p.Equipe)
            .Include(p => p.Categorie)
            .Include(p => p.SousCategorie)
            .Include(p => p.Taille)
            .Include(p => p.Fournisseur)
            .GroupBy(p => p.Equipe.Nom)
            .Select(g => new EquipeViewModel
            {
                Equipe = g.Key,
                Categorie = g.ToList().GroupBy(p => p.Categorie.Nom).Select(gc => new CategoryViewModel { Categorie = gc.Key, SousCategorie = SousCategorieViewModel.Clean(gc.ToList(), new EquipeViewModel { Equipe = g.Key }) }).ToList(),
            }).ToListAsync();

        return View("ButtonsAndTable", equipes);
    }
}