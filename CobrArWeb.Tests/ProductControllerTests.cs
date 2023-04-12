using CobrArWeb.Controllers;
using CobrArWeb.Data;
using CobrArWeb.Models.RechercheArbo;
using CobrArWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace CobrArWeb.Tests
{
    [TestClass]
    public class ProductControllerTests
    {
        private readonly ProductController _controller;
        private readonly Mock<ILogger<ProductController>> _loggerMock;
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<CobrArWebContext> _contextMock;

        public ProductControllerTests()
        {
            _loggerMock = new Mock<ILogger<ProductController>>();
            _productServiceMock = new Mock<IProductService>();
            _contextMock = new Mock<CobrArWebContext>();

            _controller = new ProductController(_loggerMock.Object, _productServiceMock.Object, _contextMock.Object);
        }

        [TestMethod]
        public void ProductsBySubcategory_ReturnsViewResult_WithListOfProducts()
        {
            // Arrange
            var testSubcategory = "TestCategory";
            var testProducts = new List<Product>
            {
                new Product { Id = 1, Produit = "Product1", SousCategorie = new SousCategorie { Nom = testSubcategory } },
                new Product { Id = 2, Produit = "Product2", SousCategorie = new SousCategorie { Nom = testSubcategory } },
            };

            _contextMock.Setup(c => c.Products).ReturnsDbSet(testProducts);

            // Act
            var result = _controller.ProductsBySubcategory(testSubcategory);



            // Assert
            Assert.AreEqual(2, ((result as ViewResult).Model as IEnumerable<Product>).Count());
            //var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            //var model = Xunit.Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.Model);
            //Xunit.Assert.Equal(2, model.Count());
        }
    }
}