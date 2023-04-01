using CobrArWeb.Services;
using CobrArWeb.Services.Interfaces;
using CobrArWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace CobrArWeb.Services
{
    public class ProductService : IProductService
    {
        private CobrArWebContext _Context;
        public ProductService(CobrArWebContext context)
        {
            _Context = context;
        }




        public List<Product> GetProducts()
        {
            var products = _Context.Products.ToList();
            return products;
        }


    }
}