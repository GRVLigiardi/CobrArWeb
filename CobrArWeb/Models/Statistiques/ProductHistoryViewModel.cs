﻿namespace CobrArWeb.Models.Statistiques
{
    public class ProductHistoryViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Action { get; set; }
        public string ColumnModified { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime Date { get; set; }
    }
}