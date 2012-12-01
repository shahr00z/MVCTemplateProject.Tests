using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Data;
using DomainModel;
using MVCTemplateProject.Controllers;
using MVCTemplateProject.ViewModel;
using Moq;
using NUnit.Framework;
using ServiceLayer.ServiceDbSet;

namespace MVCTemplateProject.Tests.Controllers
{

    [TestFixture]
    public class CategoryTestController
    {
		#region Setup/Teardown

		[SetUp]
		public void Setup()
		{
			UnitOfWork = TestHelper.TestHelper.GetMockUnitOfWork();
			CategoryDbSet = TestHelper.TestHelper.GetMockCategoryDbSet();
			CategoryController = new CategoryController(UnitOfWork.Object, CategoryDbSet.Object);
		}

		#endregion

		private Mock<IServiceDbSet<Category>> CategoryDbSet { get; set; }
		private CategoryController CategoryController { get; set; }

		private Mock<IUnitOfWork> UnitOfWork { get; set; }
        [Test]
        public void IndexShouldReturnListOfCategoryViewModel()
        {
			CategoryDbSet.Setup(x => x.List(It.IsAny<Func<Category, bool>>()))
				.Returns(TestHelper.TestHelper.GetFakeCategory());

	        var result = CategoryController.Index() as ViewResult;
	        var model = result.Model as List<CategoryViewModel>;

			Assert.IsNotNull(model);
			Assert.AreEqual(2, model.Count);
        }
		[Test]
		public void ShouldShowCreatePostWithPostViewModel()
		{
			var result = CategoryController.Create() as ViewResult;
			var model = result.Model;
			Assert.AreEqual(string.Empty, result.ViewName);
			Assert.AreEqual(model, model as CategoryViewModel);
		}
		[Test]
		public void ShouldCreatePostAndRedirectToIndex()
		{
			CategoryViewModel categoryViewModel = TestHelper.TestHelper.GetFakeCategoryViewModel();

			var result = CategoryController.Create(categoryViewModel) as RedirectToRouteResult;

			CategoryDbSet.Verify(set => set.Add(It.IsAny<Category>()), Times.Once());
			Assert.AreEqual("Index", result.RouteValues["action"]);
		}
		 
    }
}