namespace CobrArWeb.Models
{
    public class StatistiquesVentesModel
    {
        public int TotalVentes { get; set; }
        public decimal TotalRevenue { get; set; }
        public string EquipeLaPlusVendue { get; set; }
        public string MdpLePlusUtilise { get; set; }
        public string HorairePlusDeVentes { get; set; }
        public string MoisPlusDeVentes { get; set; }
        public string RevenuPeriode { get; set; }
        public IList<TeamSales> TeamSales { get; set; }
        public IList<DailySales> WeeklySales { get; set; }
    }

    public class TeamSales
    {
        public string Team { get; set; }
        public int Sales { get; set; }
    }

    public class DailySales
    {
        public string Day { get; set; }
        public int Sales { get; set; }
    }
}