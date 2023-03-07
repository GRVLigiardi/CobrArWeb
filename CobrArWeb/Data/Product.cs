namespace CobrArWeb.Data
{
    public class Team
    {
        public Team(List<Category> categories)
        {
            Categories = categories;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Category> Categories { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubCategory> SousCategories { get; set; }
    }

    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string CodeBarre { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}