using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Data;
using DomainModel;
using MVCTemplateProject.Controllers;
using Moq;
using NUnit.Framework;
using ServiceLayer.EfServices;

namespace MVCTemplateProject.Tests.Controllers
{
    [TestFixture]
    public class Home
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<EfContext>();
            _services = new Mock<ServiceLayer.Services>(_unitOfWork.Object);
        }

        #endregion

        private Mock<EfContext> _unitOfWork;
        private Mock<ServiceLayer.Services> _services;

        [Test]
        public void IndxShouldReturnListOfPage()
        {
            var posts = new List<Post>
                            {
                                new Post {Name = "Test"},
                                new Post {Name = "Test"},
                                new Post {Name = "Test"},
                                new Post {Name = "Test"}
                            };

            _services.Setup(x => x.Post.List(It.IsAny<Func<Post, bool>>())).Returns(posts);

            var controller = new HomeController(_unitOfWork.Object);
            var result = controller.Index() as ViewResult;
            var model = result.Model as List<Post>;


            Assert.AreEqual(model.Count, 4);
            Assert.AreEqual(string.Empty, result.ViewName);
        }
    }
}