using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Data;
using DomainModel;
using MVCTemplateProject.Controllers;
using Moq;
using NUnit.Framework;
using ServiceLayer.ServiceDbSet;

namespace MVCTemplateProject.Tests.Controllers
{
    [TestFixture]
    public class Home
    {
        [Test]
        public void IndexShouldReturnListOfPage()
        {
            var uow = new Mock<IUnitOfWork>();
            var iDbSet = new Mock<IServiceDbSet<Post>>();

            var post = new List<Post>
                           {
                               new Post {Id = 1, Name = "I Can"},
                               new Post {Id = 2, Name = "I I Can"}
                           };
            iDbSet.Setup(x => x.List(It.IsAny<Func<Post, bool>>())).Returns(post);

            var homeConteoller = new HomeController(uow.Object, iDbSet.Object);
            var result = homeConteoller.Index() as ViewResult;
            var model = result.Model as List<Post>;
            Assert.AreEqual(2, model.Count);
            Assert.AreEqual("", result.ViewName);
        }
    }
}