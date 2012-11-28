using System;
using System.Collections.Generic;
using System.Linq;
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
	public class PostControllerTest
	{
		#region Setup/Teardown

		[SetUp]
		public void Setup()
		{
			UnitOfWork = TestHelper.TestHelper.GetMockUnitOfWork();
			PostDbSet = TestHelper.TestHelper.GetMockPostDbSet();
			Posts = TestHelper.TestHelper.GetFakePosts();
			PostController = new PostController(UnitOfWork.Object, PostDbSet.Object);
		}

		#endregion

		private Mock<IServiceDbSet<Post>> PostDbSet { get; set; }
		private PostController PostController { get; set; }
		private List<Post> Posts { get; set; }

		private Mock<IUnitOfWork> UnitOfWork { get; set; }

		[Test]
		public void IndexShouldShowPostById()
		{
			PostDbSet.Setup(x => x.Get(It.IsAny<Func<Post, bool>>())).Returns(Posts.First());
			var result = PostController.Index(1) as ViewResult;
			var model = result.Model as Post;

			Assert.AreNotEqual(null, model);
			Assert.AreEqual("I Can", model.Title);
			Assert.AreEqual(string.Empty, result.ViewName);
		}
		[Test]
		public void IndexShouldRedirectToHomePageWhenNotFoundPostOrPostIdIs0()
		{
			PostDbSet.Setup(x => x.Get(It.IsAny<Func<Post, bool>>())).Returns(Posts.Find(post => post.Id == 100));

			var redirectToRoutResult = (RedirectToRouteResult)new PostController(UnitOfWork.Object, PostDbSet.Object).Index(100);

			Assert.AreEqual("Index", redirectToRoutResult.RouteValues["action"]);
			Assert.AreEqual("Home", redirectToRoutResult.RouteValues["Controller"]);
		}
	}
}