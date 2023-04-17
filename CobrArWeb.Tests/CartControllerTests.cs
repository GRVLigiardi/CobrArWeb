using Microsoft.VisualStudio.TestTools.UnitTesting;
using CobrArWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using CobrArWeb.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using CobrArWeb.Controllers;
using CobrArWeb.Tests;
using CobrArWeb.Helpers;
using Microsoft.AspNetCore.Http;

namespace CobrArWeb.Tests
{
    [TestClass]
    public class CartControllerTests
    {
        [DataTestMethod]
        [DataRow("day")]
        [DataRow("week")]
        [DataRow("month")]
        public void Caisse_ShouldReturnViewWithVentes(string filterType)
        {
            // Arrange
            var mockVentes = new List<Ventes>
            {
                // (Populate the list with mock Ventes objects)
            };

            var mockContext = new Mock<CobrArWebContext>();
            mockContext.Setup(x => x.Ventes).ReturnsDbSet(mockVentes);

            var controller = new CartController(mockContext.Object);

            // Act
            var result = controller.Caisse(filterType);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.IsInstanceOfType(viewResult.Model, typeof(List<Ventes>));

            // Add any additional assertions based on the filterType and expected results
        }

    }
}