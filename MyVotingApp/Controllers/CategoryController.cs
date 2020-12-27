using MyVotingApp.Filters;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Extensions;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCategoryApp.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private LinkGenerator _linkGenerator;
        public CategoryController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }
        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public IActionResult GetCategories([FromQuery] CategoryParameters categoryParameters)
        {
            var categories = _repository.Category.GetCategories(categoryParameters);

            var metadata = new
            {
                categories.TotalCount,
                categories.PageSize,
                categories.CurrentPage,
                categories.TotalPages,
                categories.HasNext,
                categories.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            _logger.LogInfo($"Returned {categories.TotalCount} categories from database.");

            var shapedCategorys = categories.Select(o => o.Entity).ToList();
            var mediaType = (MediaTypeHeaderValue)HttpContext.Items["AcceptHeaderMediaType"];
            if (!mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase))
            {
                return Ok(shapedCategorys);
            }
            for (var index = 0; index < categories.Count; index++)
            {
                var categoryLinks = CreateLinksForCategory(categories[index].Id, categoryParameters.Fields);
                shapedCategorys[index].Add("Links", categoryLinks);
            }
            var categoriesWrapper = new LinkCollectionWrapper<Entity>(shapedCategorys);
            return Ok(CreateLinksForCategorys(categoriesWrapper));
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] Category category)
        {
            if (category.IsObjectNull())
            {
                _logger.LogError("Category object sent from client is null.");
                return BadRequest("Category object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid category object sent from client.");
                return BadRequest("Invalid model object");
            }

            _repository.Category.CreateCategory(category);
            _repository.Save();

            return CreatedAtRoute("CategoryById", new { id = category.Id }, category);
        }

        [HttpGet("{id}", Name = "CategoryById")]

        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public IActionResult GetCategoryById(Guid id, [FromQuery] string fields)
        {
            var category = _repository.Category.GetCategoryById(id, fields);

            if (category.Id == Guid.Empty)
            {
                _logger.LogError($"Category with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            var mediaType = (MediaTypeHeaderValue)HttpContext.Items["AcceptHeaderMediaType"];

            if (!mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase))
            {
                _logger.LogInfo($"Returned shaped category with id: {id}");
                return Ok(category.Entity);
            }

            category.Entity.Add("Links", CreateLinksForCategory(category.Id, fields));

            return Ok(category.Entity);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(Guid id, [FromBody] Category category)
        {
            if (category.IsObjectNull())
            {
                _logger.LogError("Category object sent from client is null.");
                return BadRequest("Category object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid category object sent from client.");
                return BadRequest("Invalid model object");
            }

            var dbCategory = _repository.Category.GetCategoryById(id);
            if (dbCategory.IsEmptyObject())
            {
                _logger.LogError($"Category with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            _repository.Category.UpdateCategory(dbCategory, category);
            _repository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(Guid id)
        {
            var category = _repository.Category.GetCategoryById(id);
            if (category.IsEmptyObject())
            {
                _logger.LogError($"Category with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            _repository.Category.DeleteCategory(category);
            _repository.Save();

            return NoContent();
        }

        private IEnumerable<Link> CreateLinksForCategory(Guid id, string fields)
        {
            var links = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetCategoryById), values: new {id, fields}),
                "self",
                "GET"),

                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(DeleteCategory), values: new {id}),
                "delete_category",
                "DELETE"),

                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(UpdateCategory), values: new {id}),
                "update_category",
                "PUT")
            };

            return links;
        }

        private LinkCollectionWrapper<Entity> CreateLinksForCategorys(LinkCollectionWrapper<Entity> categoriesWrapper)
        {
            categoriesWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetCategories), values: new { }),
                    "self",
                    "GET"));

            return categoriesWrapper;
        }
    }
}
