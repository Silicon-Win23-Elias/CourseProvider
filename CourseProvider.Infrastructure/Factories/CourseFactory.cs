using Azure.Core;
using CourseProvider.Infrastructure.Data.Entities;
using CourseProvider.Infrastructure.Models;

namespace CourseProvider.Infrastructure.Factories;

public static class CourseFactory
{
    public static CourseEntity Create(CourseCreateRequest request)
    {
        return new CourseEntity
        {
            ImageUri = request.ImageUri,
            ImageHeaderUri = request.ImageHeaderUri,
            IsBestSeller = request.IsBestSeller,
            IsDigital = request.IsDigital,
            Categories = request.Categories,
            Title = request.Title,
            Subtitle = request.Subtitle,
            StarRating = request.StarRating,
            Reviews = request.Reviews,
            LikesPercent = request.LikesPercent,
            Likes = request.Likes,
            Duration = request.Duration,
            Authors = request.Authors?.Select(a => new AuthorEntity
            {
                Name = a.Name
            }).ToList(),
            Prices = request.Prices == null ? null : new PricesEntity
            {
                Currency = request.Prices.Currency,
                Price = request.Prices.Price,
                Discount = request.Prices.Discount
            },
            Content = request.Content == null ? null : new ContentEntity
            {
                Description = request.Content.Description,
                Includes = request.Content.Includes,
                ProgramDetails = request.Content.ProgramDetails?.Select(pd => new ProgramDetailItemEntity
                {
                    Id = pd.Id,
                    Title = pd.Title,
                    Description = pd.Description
                }).ToList()
            }
        };
    }

    public static CourseEntity Create(CourseUpdateRequest request)
    {
        return new CourseEntity
        {
            Id = request.Id,
            ImageUri = request.ImageUri,
            ImageHeaderUri = request.ImageHeaderUri,
            IsBestSeller = request.IsBestSeller,
            IsDigital = request.IsDigital,
            Categories = request.Categories,
            Title = request.Title,
            Subtitle = request.Subtitle,
            StarRating = request.StarRating,
            Reviews = request.Reviews,
            LikesPercent = request.LikesPercent,
            Likes = request.Likes,
            Duration = request.Duration,
            Authors = request.Authors?.Select(a => new AuthorEntity
            {
                Name = a.Name
            }).ToList(),
            Prices = request.Prices == null ? null : new PricesEntity
            {
                Currency = request.Prices.Currency,
                Price = request.Prices.Price,
                Discount = request.Prices.Discount
            },
            Content = request.Content == null ? null : new ContentEntity
            {
                Description = request.Content.Description,
                Includes = request.Content.Includes,
                ProgramDetails = request.Content.ProgramDetails?.Select(pd => new ProgramDetailItemEntity
                {
                    Id = pd.Id,
                    Title = pd.Title,
                    Description = pd.Description
                }).ToList()
            }
        };
    }
    public static Course Create(CourseEntity entity)
    {
        return new Course
        {
            Id = entity.Id,
            ImageUri = entity.ImageUri,
            ImageHeaderUri = entity.ImageHeaderUri,
            IsBestSeller = entity.IsBestSeller,
            IsDigital = entity.IsDigital,
            Categories = entity.Categories,
            Title = entity.Title,
            Subtitle = entity.Subtitle,
            StarRating = entity.StarRating,
            Reviews = entity.Reviews,
            LikesPercent = entity.LikesPercent,
            Likes = entity.Likes,
            Duration = entity.Duration,
            Authors = entity.Authors?.Select(a => new Author
            {
                Name = a.Name
            }).ToList(),
            Prices = entity.Prices == null ? null : new Prices
            {
                Currency = entity.Prices.Currency,
                Price = entity.Prices.Price,
                Discount = entity.Prices.Discount
            },
            Content = entity.Content == null ? null : new Content
            {
                Description = entity.Content.Description,
                Includes = entity.Content.Includes,
                ProgramDetails = entity.Content.ProgramDetails?.Select(pd => new ProgramDetailItem
                {
                    Id = pd.Id,
                    Title = pd.Title,
                    Description = pd.Description
                }).ToList()
            }
        };
    }
}
