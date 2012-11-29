using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Data;
using DomainModel;
using MVCTemplateProject.Controllers;
using MVCTemplateProject.Mappers;
using MVCTemplateProject.ViewModel;
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
		public void DetailShouldRedirectToHomePageWhenNotFoundPostOrPostIdIs0()
		{
			PostDbSet.Setup(x => x.Get(It.IsAny<Func<Post, bool>>())).Returns(Posts.Find(post => post.Id == 100));

			var redirectToRoutResult =
				(RedirectToRouteResult) new PostController(UnitOfWork.Object, PostDbSet.Object).Details(100);

			Assert.AreEqual("Index", redirectToRoutResult.RouteValues["action"]);
			Assert.AreEqual("Home", redirectToRoutResult.RouteValues["Controller"]);
		}

		[Test]
		public void DetailShouldShowPostById()
		{
			PostDbSet.Setup(x => x.Get(It.IsAny<Func<Post, bool>>())).Returns(Posts.First());
			var result = PostController.Details(1) as ViewResult;
			var model = result.Model as PostViewModel;

			Assert.AreNotEqual(null, model);
			Assert.AreEqual("I Can", model.Title);
			Assert.AreEqual(string.Empty, result.ViewName);
		}

		[Test]
		public void ShouldCreatePostAndRedirectToDetailsWithPostViewModel()
		{
			var postViewModel = new PostViewModel {Title = "Test", Id = 1, Content = "st"};
			Post post = new PostMapper().MappViewModelToModel(postViewModel);

			var result = PostController.Create(postViewModel) as RedirectToRouteResult;

			PostDbSet.Verify(set => set.Add(It.IsAny<Post>()), Times.Once());
			Assert.AreEqual("Details", result.RouteValues["action"]);
		}

		[Test]
		public void ShouldShowCreatePostWithPostViewModel()
		{
			var postViewModel = new PostViewModel {};

			var result = PostController.Create() as ViewResult;
			var model = result.Model as PostViewModel;

			Assert.AreEqual(string.Empty, result.ViewName);
			Assert.AreEqual(model,postViewModel);
		}
	}
}