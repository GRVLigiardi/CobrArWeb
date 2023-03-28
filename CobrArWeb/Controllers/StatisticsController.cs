using CobrArWeb.Data;
using CobrArWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace CobrArWeb.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly CobrArWebContext _context;

        public StatisticsController(CobrArWebContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var statistiques = new StatistiquesVentesModel
            {
                TotalVentes = _context.Ventes.Where(v => v.Quantity != null).Sum(v => v.Quantity),
                TotalRevenue = _context.Ventes.Where(v => v.Quantity != null && v.Prix != null).Sum(v => v.Quantity * v.Prix.Value),

                EquipeLaPlusVendue = _context.Ventes
                    .Where(v => v.Quantity != null && v.Equipe != null)
                    .GroupBy(v => v.Equipe)
                    .OrderByDescending(g => g.Sum(v => v.Quantity))
                    .Select(g => g.Key)
                    .FirstOrDefault(),

                HorairePlusDeVentes = _context.Ventes
                    .Where(v => v.Date != null)
                    .GroupBy(v => v.Date.Hour)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key.ToString())
                    .FirstOrDefault(),

                MoisPlusDeVentes = _context.Ventes
                    .Where(v => v.Date != null)
                    .GroupBy(v => v.Date.Month)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key.ToString())
                    .FirstOrDefault(),

                RevenuPeriode = "" // Vous pouvez définir cette valeur en fonction de l'affichage souhaité (par jour, semaine, mois ou année)
            };

            return View(statistiques);
        }
    }
}


