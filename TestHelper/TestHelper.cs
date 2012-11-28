using System.Collections.Generic;
using Data;
using DomainModel;
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

		public static Mock<IServiceDbSet<Post>> GetMockPostDbSet()
		{
			var iDbSet = new Mock<IServiceDbSet<Post>>();
			return iDbSet;
		}
	}
}