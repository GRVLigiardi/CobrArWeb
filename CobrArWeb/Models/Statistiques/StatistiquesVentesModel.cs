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
    }
}