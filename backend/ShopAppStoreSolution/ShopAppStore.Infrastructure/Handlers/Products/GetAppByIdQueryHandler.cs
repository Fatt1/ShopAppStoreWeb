using Microsoft.EntityFrameworkCore;
using ShopAppStore.Application.Abstractions.CQRS.Query;
using ShopAppStore.Application.Features.Products.DTOs;
using ShopAppStore.Application.Features.Products.Queries.GetAppById;
using ShopAppStore.Domain.Constants;
using ShopAppStore.Infrastructure.Repositories;
using ShopAppStore.Shared;

namespace ShopAppStore.Infrastructure.Handlers.Products
{
    public class GetAppByIdQueryHandler : IQueryHandler<GetAppByIdQuery, GetAppDTO>
    {
        private readonly AppDbContext _context;
        public GetAppByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Result<GetAppDTO>> Handle(GetAppByIdQuery request, CancellationToken cancellationToken)
        {
            var app = await _context.Apps
                .Include(a => a.AttributeApps)
               .Include(a => a.Duration)
               .Include(a => a.AppCategories)
                   .ThenInclude(ac => ac.Category)
               .Include(a => a.AppImages)
               .Include(a => a.AppAccounts)
               .Include(a => a.Reviews)
               .FirstOrDefaultAsync(a => a.Id == request.Id && !a.IsDeleted, cancellationToken);

            if (app is null)
            {
                return Result<GetAppDTO>.Failure(new Error("App.NotFound", "App not found"));
            }

            var appDto = new GetAppDTO
            {
                Id = app.Id,
                AppName = app.AppName,

                Description = app.Description,
                ThumbnailUrl = app.ThumbnailUrl,
                ImageUrls = app.AppImages.Select(i => i.ImageUrl).ToList(),
                DurationId = app.DurationId,
                DurationName = app.Duration.DurationName,
                CategoryNames = app.AppCategories.Select(ac => ac.Category.CategoryName).ToList(),
                OriginalPrice = app.OriginalPrice,
                Slug = app.Slug,
                CurrentPrice = app.CurrentPrice,
                Status = app.Status,
                BlogId = app.BlogId,
                Stock = app.AppAccounts.Count(a => a.Status == AppAccountStatus.Available),
                TotalReviews = app.Reviews.Count,
                AverageRating = app.Reviews.Any() ? app.Reviews.Average(r => r.Rating) : 0,
                Note = app.Note,
                Attributes = app.AttributeApps.Select(aa => new GetAppAttributeDTO
                {
                    AttributeName = aa.AttributeName,
                    AttributeValue = aa.AttributeValue
                }).ToList()
            };

            return Result<GetAppDTO>.Success(appDto);
        }
    }
}
