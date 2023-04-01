using System.Collections.Generic;

namespace CobrArWeb.Models.RechercheArbo
{
    public class ListViewModel
    {
        public List<EquipeViewModel> EquipeViewModelList { get; set; }
        public bool IsStockView { get; set; }
    }
}