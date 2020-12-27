using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class CategoryParameters : QueryStringParameters
    {
        public CategoryParameters()
        {
            OrderBy = "Id";
        }
        public string Name { get; set; }
    }
}
