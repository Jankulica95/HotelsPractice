using Hoteli.Controllers;
using Hoteli.Interfaces;
using Hoteli.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Web.Http;

namespace Hoteli.Tests.Controllers
{
    [TestClass]
    public class LanacHotelasControllerTest
    {
        [TestMethod]
        public void GetReturnsLanacHotelaWithSameId()
        {
            // Arrange
            var mockRepository = new Mock<ILanacHotelaRepository>();
            mockRepository.Setup(x => x.GetById(42)).Returns(new LanacHotela { Id = 42 });

            var controller = new LanacHotelasController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(42);
            var contentResult = actionResult as OkNegotiatedContentResult<LanacHotela>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(42, contentResult.Content.Id);
        }

        // --------------------------------------------------------------------------------------

        [TestMethod]
        public void GetReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IDrzavaRepository>();
            var controller = new DrzavasController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteReturnsNotFound()
        {
            // Arrange 
            var mockRepository = new Mock<IDrzavaRepository>();
            var controller = new DrzavasController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Delete(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        // --------------------------------------------------------------------------------------

        [TestMethod]
        public void DeleteReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IDrzavaRepository>();
            mockRepository.Setup(x => x.GetById(10)).Returns(new Drzava { Id = 10 });
            var controller = new DrzavasController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Delete(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        // --------------------------------------------------------------------------------------

        [TestMethod]
        public void PutReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<IDrzavaRepository>();
            var controller = new DrzavasController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(10, new Drzava { Id = 9, Ime = "Drzava9", InternacionalniKod = "DAN" });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        // -------------------------------------------------------------------------------------

        [TestMethod]
        public void PostMethodSetsLocationHeader()
        {
            // Arrange
            var mockRepository = new Mock<IDrzavaRepository>();
            var controller = new DrzavasController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Post(new Drzava { Id = 10, Ime = "Drzava10", InternacionalniKod = "DEN" });
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Drzava>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(10, createdResult.RouteValues["id"]);
        }

        // ------------------------------------------------------------------------------------------

        [TestMethod]
        public void GetReturnsMultipleObjects()
        {
            // Arrange
            List<Drzava> drzave = new List<Drzava>();
            drzave.Add(new Drzava { Id = 1, Ime = "Drzava1", InternacionalniKod = "AUS" });
            drzave.Add(new Drzava { Id = 2, Ime = "Drzava2", InternacionalniKod = "HOL" });

            var mockRepository = new Mock<IDrzavaRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(drzave.AsEnumerable());
            var controller = new DrzavasController(mockRepository.Object);

            // Act
            IEnumerable<Drzava> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(drzave.Count, result.ToList().Count);
            Assert.AreEqual(drzave.ElementAt(0), result.ElementAt(0));
            Assert.AreEqual(drzave.ElementAt(1), result.ElementAt(1));
        }
    }
}
