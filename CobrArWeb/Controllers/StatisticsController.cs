using CobrArWeb.Data;
using CobrArWeb.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                TotalVentes = _context.Ventes.Sum(v => v.Quantity),
                TotalRevenue = _context.Ventes.Sum(v => v.Quantity * v.Prix.GetValueOrDefault()),

                // Ajoutez les nouvelles statistiques ici
                EquipeLaPlusVendue = _context.Ventes
                    .GroupBy(v => v.Equipe)
                    .OrderByDescending(g => g.Sum(v => v.Quantity))
                    .Select(g => g.Key)
                    .FirstOrDefault(),

                MdpLePlusUtilise = _context.Ventes
                    .GroupBy(v => v.MDPNom)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key)
                    .FirstOrDefault().ToString(),

                HorairePlusDeVentes = _context.Ventes
                    .GroupBy(v => v.Date.Hour)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key)
                    .FirstOrDefault().ToString(),

                MoisPlusDeVentes = _context.Ventes
                    .GroupBy(v => v.Date.Month)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key)
                    .FirstOrDefault().ToString(),

                RevenuPeriode = "" // Vous pouvez définir cette valeur en fonction de l'affichage souhaité (par jour, semaine, mois ou année)
            };

           return View(statistiques);
        }

       
    }
}