using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Data;
using DomainModel;
using MVCTemplateProject.Controllers;
using Moq;
using NUnit.Framework;
using ServiceLayer;

namespace MVCTemplateProject.Tests.Controllers
{
    [TestFixture]
    public class Home
    {
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
            var efContext = new Mock<EfContext>();
            var serviceContext = new ServiceLayer.Services(efContext.Object);
            var post = new ServiceLayer.EfServices.EfPostService(efContext.Object);

            var rep = new Moq.Mock<Services>(efContext);
            rep.Object.Post = post;
            rep.Setup(x => x.Post).Returns(post);
            rep.Setup(x => x.Post.List(It.IsAny<Func<Post, bool>>())).Returns(posts);
            var controller = new HomeController(efContext.Object);
            List<Post> model = controller.DataContext.Post.List();
            var result = controller.Index() as ViewResult;

            Assert.AreEqual(model.Count, 4);
            Assert.AreEqual(string.Empty, result.ViewName);
        }
    }
}