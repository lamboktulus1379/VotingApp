using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Extensions
{
    public static class CategoryExtensions
    {
        public static void Map(this Category dbCategory, Category category)
        {
            dbCategory.Name = category.Name;
        }
    }
}       