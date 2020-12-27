using Contracts;
using Entities;
using Entities.Extensions;
using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        private readonly ISortHelper<Category> _sortHelper;
        private readonly IDataShaper<Category> _dataShaper;

        public CategoryRepository(RepositoryContext repositoryContext, ISortHelper<Category> sortHelper, IDataShaper<Category> dataShaper) : base(repositoryContext)
        {
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }
        public void CreateCategory(Category category)
        {
            Create(category);
        }

        public void DeleteCategory(Category category)
        {
            Delete(category);
        }

        public PagedList<ShapedEntity> GetCategories(CategoryParameters categoryParameters)
        {
            var categorys = FindAll();
            SearchByName(ref categorys, categoryParameters.Name);
            var sortedOwners = _sortHelper.ApplySort(categorys, categoryParameters.OrderBy);
            var shapedOwners = _dataShaper.ShapeData(sortedOwners, categoryParameters.Fields);

            return PagedList<ShapedEntity>.ToPagedList(shapedOwners,
                categoryParameters.PageNumber,
                categoryParameters.PageSize);
        }

        private void SearchByName(ref IQueryable<Category> categorys, string categoryName)
        {
            if (!categorys.Any() || string.IsNullOrWhiteSpace(categoryName))
                return;

            if (string.IsNullOrEmpty(categoryName))
                return;

            categorys = categorys.Where(o => o.Name.ToLower().Contains(categoryName.Trim().ToLower()));
        }

        public ShapedEntity GetCategoryById(Guid categoryId, string fields)
        {
            var category = FindByCondition(category => category.Id.Equals(categoryId))
                .FirstOrDefault();

            return _dataShaper.ShapeData(category, fields);
        }

        public Category GetCategoryById(Guid categoryId)
        {
            return FindByCondition(category => category.Id.Equals(categoryId))
                .FirstOrDefault();
        }

        public Category GetCategoryWithDetails(Guid categoryId)
        {
            return FindByCondition(category => category.Id.Equals(categoryId))
                .FirstOrDefault();
        }

        public void UpdateCategory(Category dbCategory, Category category)
        {
            dbCategory.Map(category);
            Update(dbCategory);
        }
    }
}
