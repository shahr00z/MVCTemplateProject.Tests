using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Business;
using MVCTemplateProject.Controllers;
using Moq;
using NUnit.Framework;
using Post = DomainModel.Post;

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
          
            var mockRepository = new Mock<IBusinessContext>();
            mockRepository.Setup(mr => mr.Post.List(It.IsAny<Expression<Func<Post, bool>>>())).Returns(posts.ToList());
            mockRepository.SetupGet(p=>p.Post).Returns(mockRepository.Object.Post);

            var controller = new HomeController(mockRepository.Object);
            var result = controller.Index() as ViewResult;
            var model = result.Model as List<Post>;

            Assert.AreEqual(model.Count, 4);
            Assert.AreEqual(string.Empty,result.ViewName);
        }
    }
}