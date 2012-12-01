using System.Collections.Generic;
using Data;
using DomainModel;
using MVCTemplateProject.ViewModel;
using Moq;
using ServiceLayer.ServiceDbSet;

namespace MVCTemplateProject.Tests.TestHelper
{
	internal class TestHelper
	{
		public static Mock<IUnitOfWork> GetMockUnitOfWork()
		{
			var uow = new Mock<IUnitOfWork>();
			return uow;
		}

		public static List<Post> GetFakePosts()
		{
			return new List<Post>
				       {
					       new Post {Id = 1, Title = "I Can"},
					       new Post {Id = 2, Title = "I I Can"}
				       };
		}

		public static List<Category> GetFakeCategory()
		{
			var child = new List<Category>
				            {
					            new Category {Id = 1, Name = "1390",},
					            new Category {Id = 2, Name = "1391"}
				            };
			return new List<Category>
				       {
					       new Category {Id = 1, Name = "I Can", Categories = child},
					       new Category {Id = 2, Name = "I I Can"}
				       };
		}

		public static CategoryViewModel GetFakeCategoryViewModel()
		{
			var child = new List<CategoryViewModel>
				            {
					            new CategoryViewModel {Id = 1, Name = "1390",},
					            new CategoryViewModel {Id = 2, Name = "1391"}
				            };
			return new CategoryViewModel
				       {
					       Id = 1,
					       Name = "I Can",
					       CategoryChild = child
				       };
		}

		public static PostViewModel GetFakePostViewModel()
		{
			var rawTags = new List<string> {"Test", "TEst2"};
			return new PostViewModel {Id = 1, Title = "I Can", RawTags = rawTags};
		}

		public static Mock<IServiceDbSet<Post>> GetMockPostDbSet()
		{
			var iDbSet = new Mock<IServiceDbSet<Post>>();
			return iDbSet;
		}

		public static Mock<IServiceDbSet<Category>> GetMockCategoryDbSet()
		{
			var iDbSet = new Mock<IServiceDbSet<Category>>();
			return iDbSet;
		}

		public static Mock<IServiceDbSet<Tag>> GetMockTagDbSet()
		{
			var iDbSet = new Mock<IServiceDbSet<Tag>>();
			return iDbSet;
		}
	}
}