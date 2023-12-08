using AutoMapper;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using System.Text.Json;

namespace ExamonimyWeb.Controllers
{
    public class GenericController<TEntity> : Controller where TEntity : class
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<TEntity> _genericRepository;

        public GenericController(IMapper mapper, IGenericRepository<TEntity> genericRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
        }

        protected async Task<ActionResult> Get<TGetDto>(RequestParams? requestParams, Expression<Func<TEntity, bool>>? filter, List<string>? includedProperties)
        {
            var items = await _genericRepository.GetAsync(requestParams, filter, includedProperties);

            var itemsToReturn = items.Select(e => _mapper.Map<TGetDto>(e));

            var paginationMetadata = new PaginationMetadata
            {
                TotalCount = items.TotalCount,
                PageSize = items.PageSize,
                CurrentPage = items.PageNumber,
                TotalPages = items.TotalPages
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(itemsToReturn);
        }

        public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();

            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        
    }

}
