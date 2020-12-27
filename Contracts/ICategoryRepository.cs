using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
		PagedList<ShapedEntity> GetCategories(CategoryParameters votingParameters);
		ShapedEntity GetCategoryById(Guid votingId, string fields);
		Category GetCategoryWithDetails(Guid votingId);
		Category GetCategoryById(Guid votingId);

		void CreateCategory(Category voting);
		void UpdateCategory(Category dbCategory, Category voting);
		void DeleteCategory(Category voting);
	}
}
