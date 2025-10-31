using Microsoft.EntityFrameworkCore;
using ShopAppStore.Application.Abstractions.CQRS.Query;
using ShopAppStore.Application.Features.Products.DTOs;
using ShopAppStore.Application.Features.Products.Queries.FilterAppPagination;
using ShopAppStore.Domain.Constants;
using ShopAppStore.Infrastructure.Entities;
using ShopAppStore.Infrastructure.Repositories;
using ShopAppStore.Shared;

namespace ShopAppStore.Infrastructure.Handlers.Products
{
    public class FilterAppPaginationQueryHandler : IQueryHandler<FilterAppPaginationQuery, PagedList<GetAllAppDTO>>
    {
        private readonly AppDbContext _context;

        public FilterAppPaginationQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PagedList<GetAllAppDTO>>> Handle(FilterAppPaginationQuery request, CancellationToken cancellationToken)
        {

            var existCategory = _context.Categories
                .AsNoTracking()
                .Any(c => c.Id == request.CategoryId);
            if (existCategory == false)
            {
                return Result<PagedList<GetAllAppDTO>>.Failure(new Error("CategoryNotFound", $"Categgory with id: {request.CategoryId} not found"));
            }

            // Base query
            var query = _context.Apps
                .AsNoTracking()
                .Where(app => !app.IsDeleted);

            // Filter by CategoryId(Required)

            query = query.Where(app => app.AppCategories.Any(ac => ac.CategoryId == request.CategoryId));

            // Filter by Price Range
            if (request.PriceFrom.HasValue && request.PriceFrom.Value > 0)
            {
                query = query.Where(app => app.CurrentPrice >= request.PriceFrom.Value);
            }

            if (request.PriceTo.HasValue && request.PriceTo.Value > 0)
            {
                query = query.Where(app => app.CurrentPrice <= request.PriceTo.Value);
            }

            // Filter by PlatFormId
            if (request.PlatFormId.HasValue && request.PlatFormId.Value != Guid.Empty)
            {
                query = query.Where(app => app.AppPlatForms.Any(ap => ap.PlatFormId == request.PlatFormId.Value));
            }

            // Filter by DurationId
            if (request.DurationId.HasValue && request.DurationId.Value != Guid.Empty)
            {
                query = query.Where(app => app.DurationId == request.DurationId.Value);
            }


            // Apply Sorting
            query = ApplySorting(query, request.SortBy, request.OrderBy ?? OrderBy.Descending);

            // Project to DTO
            var dtoQuery = query.Select(app => new GetAllAppDTO
            {
                Id = app.Id,
                Name = app.AppName,
                ThumbnailUrl = app.ThumbnailUrl,
                CurrentPrice = app.CurrentPrice,
                OriginalPrice = app.OriginalPrice,
                Stock = app.AppAccounts.Count(a => a.Status == AppAccountStatus.Available)
            });

            // Apply Pagination
            var pagedApps = PagedList<GetAllAppDTO>.ToPagedList(dtoQuery, request.PageNumber, request.PageSize);

            return Result<PagedList<GetAllAppDTO>>.Success(pagedApps);
        }

        private IQueryable<App> ApplySorting(IQueryable<App> query, string sortBy, string orderBy)
        {
            // Determine sort field
            var sortedQuery = sortBy?.ToLower() switch
            {
                SortBy.Price => orderBy?.ToLower() == OrderBy.Descending
                    ? query.OrderByDescending(app => app.CurrentPrice)
                    : query.OrderBy(app => app.CurrentPrice),

                SortBy.CTime => orderBy?.ToLower() == OrderBy.Descending
                    ? query.OrderByDescending(app => app.CreateAt)
                    : query.OrderBy(app => app.CreateAt),

                SortBy.Sales => orderBy?.ToLower() == OrderBy.Descending
                    ? query.OrderByDescending(app => app.SoldCount)
                    : query.OrderBy(app => app.SoldCount),

                // Default sorting by CreateAt Descending
                _ => query.OrderByDescending(app => app.CreateAt)
            };

            return sortedQuery;
        }
    }
}
