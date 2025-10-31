using ShopAppStore.Application.Abstractions.CQRS.Command;
using ShopAppStore.Application.Features.Categories.DTOs;
using ShopAppStore.Application.Services.Interfaces;
using ShopAppStore.Domain.Interfaces.Repositories;
using ShopAppStore.Infrastructure.Entities;
using ShopAppStore.Shared;
using static ShopAppStore.Infrastructure.Entities.Category;

namespace ShopAppStore.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : ICommand<CreateCateogoryDTO>
    {
        public string CategoryName { get; set; } = null!;

        public Guid? ParentId { get; set; } = null;
        public string Slug { get; set; } = string.Empty;
    }

    public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, CreateCateogoryDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISlugGenerator _slugGenerator;
        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, ISlugGenerator slugGenerator)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _slugGenerator = slugGenerator;
        }

        public async Task<Result<CreateCateogoryDTO>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {

            // Generate slug nếu không được cung cấp
            var slug = string.IsNullOrWhiteSpace(request.Slug)
                ? _slugGenerator.GenerateSlug(request.CategoryName)
                : _slugGenerator.GenerateSlug(request.Slug);

            // Check if slug already exists
            var isSlugUnique = await _categoryRepository.IsSlugUniqueAsync(slug, cancellationToken);
            if (!isSlugUnique)
            {
                return Result<CreateCateogoryDTO>.Failure(CategoryErrors.SlugAlreadyExists(request.Slug));
            }

            // Check if parent category exists (if ParentId is provided)
            if (request.ParentId.HasValue && request.ParentId.Value != Guid.Empty)
            {
                var parentExists = await _categoryRepository.ExistsAsync(request.ParentId.Value, cancellationToken);
                if (!parentExists)
                {
                    return Result<CreateCateogoryDTO>.Failure(CategoryErrors.ParentCategoryNotFound(request.ParentId.Value));
                }
            }



            // Create new category
            var category = new Category
            {
                CategoryName = request.CategoryName,
                Slug = slug,
                ParentId = request.ParentId,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            // Add to repository
            await _categoryRepository.AddAsync(category, cancellationToken);

            // Save changes
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Return DTO
            var result = new CreateCateogoryDTO
            {
                Id = category.Id
            };

            return Result<CreateCateogoryDTO>.Success(result);
        }
    }
}
