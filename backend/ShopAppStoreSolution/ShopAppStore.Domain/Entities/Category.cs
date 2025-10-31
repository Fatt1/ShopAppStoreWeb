using ShopAppStore.Domain.Common;
using ShopAppStore.Domain.Interfaces;
using ShopAppStore.Shared;


namespace ShopAppStore.Infrastructure.Entities;

public class Category : IHasSlug, IEntity<Guid>
{
    public Guid Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public Guid? ParentId { get; set; }

    public DateTime CreateAt { get; set; }

    public DateTime UpdateAt { get; set; }

    public string Slug { get; set; } = null!;

    public virtual ICollection<AppCategory> AppCategories { get; set; } = new List<AppCategory>();

    public virtual ICollection<Category> Children { get; set; } = new List<Category>();

    public virtual Category? Parent { get; set; }



    public static class CategoryErrors
    {
        public static Error SlugAlreadyExists(string slug) =>
            new Error("Category.SlugAlreadyExists", $"Category với slug '{slug}' đã tồn tại.");

        public static Error ParentCategoryNotFound(Guid parentId) =>
            new Error("Category.ParentNotFound", $"Category cha với ID '{parentId}' không tồn tại.");

        public static Error CategoryNameRequired =>
            new Error("Category.NameRequired", "Tên category không được để trống.");

        public static Error SlugRequired =>
            new Error("Category.SlugRequired", "Slug không được để trống.");

        public static Error NotFound(Guid id) =>
            new Error("Category.NotFound", $"Category với ID '{id}' không tồn tại.");
    }
}
