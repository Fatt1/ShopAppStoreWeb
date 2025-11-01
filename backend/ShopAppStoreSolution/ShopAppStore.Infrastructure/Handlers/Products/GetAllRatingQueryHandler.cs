using Microsoft.EntityFrameworkCore;
using ShopAppStore.Application.Abstractions.CQRS.Query;
using ShopAppStore.Application.Features.Products.DTOs;
using ShopAppStore.Application.Features.Products.Queries.GetRating;
using ShopAppStore.Infrastructure.Repositories;
using ShopAppStore.Shared;

namespace ShopAppStore.Infrastructure.Handlers.Products
{
    public class GetAllRatingQueryHandler : IQueryHandler<GetAllRatingQuery, List<GetAllRatingDTO>>
    {
        private readonly AppDbContext _context;
        public GetAllRatingQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<GetAllRatingDTO>>> Handle(GetAllRatingQuery request, CancellationToken cancellationToken)
        {
            var ratings = await _context.Reviews
                .Where(r => r.AppId == request.Id)
                .Select(r => new GetAllRatingDTO
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreateAt,
                    UserName = r.User.UserName!
                }).ToListAsync();

            return Result<List<GetAllRatingDTO>>.Success(ratings);
        }
    }
}
