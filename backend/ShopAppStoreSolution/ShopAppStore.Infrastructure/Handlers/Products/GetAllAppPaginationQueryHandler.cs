using ShopAppStore.Application.Abstractions.CQRS.Query;
using ShopAppStore.Application.Features.Products.DTOs;
using ShopAppStore.Application.Features.Products.Queries.GetAllAppPagination;
using ShopAppStore.Domain.Constants;
using ShopAppStore.Infrastructure.Repositories;
using ShopAppStore.Shared;

namespace ShopAppStore.Infrastructure.Handlers.Products
{
    public class GetAllAppPaginationQueryHandler : IQueryHandler<GetAllAppPaginationQuery, PagedList<GetAllAppDTO>>
    {
        private readonly AppDbContext _context;
        public GetAllAppPaginationQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public Task<Result<PagedList<GetAllAppDTO>>> Handle(GetAllAppPaginationQuery request, CancellationToken cancellationToken)
        {
            var apps = _context.Apps
                .Select(app => new GetAllAppDTO
                {
                    Id = app.Id,
                    Name = app.AppName,
                    ThumbnailUrl = app.ThumbnailUrl,
                    CurrentPrice = app.CurrentPrice,
                    OriginalPrice = app.OriginalPrice,
                    Slug = app.Slug,
                    Stock = app.AppAccounts.Count(a => a.Status == AppAccountStatus.Available)
                });
            var pagedApps = PagedList<GetAllAppDTO>.ToPagedList(apps, request.PageNumber, request.PageSize);
            return Task.FromResult(Result<PagedList<GetAllAppDTO>>.Success(pagedApps));
        }
    }
}
