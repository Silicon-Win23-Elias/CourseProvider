using Azure.Core;
using CourseProvider.Infrastructure.Data.Contexts;
using CourseProvider.Infrastructure.Data.Entities;
using CourseProvider.Infrastructure.Factories;
using CourseProvider.Infrastructure.Models;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CourseProvider.Infrastructure.Services;

public interface ICourseService
{
    Task<Course> CreateCourseAsync(CourseCreateRequest request);
    Task<Course> GetCourseByIdAsync(string id);
    Task<IEnumerable<Course>> GetCoursesAsync();
    Task<Course> UpdateCourseAsync(CourseUpdateRequest request);
    Task<bool> DeleteCourseAsync(string id);
}

public class CourseService(IDbContextFactory<DataContext> contextFactory) : ICourseService
{
    private readonly IDbContextFactory<DataContext> _contextFactory = contextFactory;

    public async Task<Course> CreateCourseAsync(CourseCreateRequest request)
    {
        await using var context = _contextFactory.CreateDbContext();

        var courseEntity = CourseFactory.Create(request);
        context.Courses.Add(courseEntity);
        await context.SaveChangesAsync();

        return CourseFactory.Create(courseEntity);
    }

    public async Task<Course> GetCourseByIdAsync(string id)
    {
        await using var context = _contextFactory.CreateDbContext();
        var courseEntity = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);

        if (courseEntity != null)
        {
            return CourseFactory.Create(courseEntity);
        }
        else
            return null!;
    }

    public async Task<IEnumerable<Course>> GetCoursesAsync()
    {
        await using var context = _contextFactory.CreateDbContext();
        var courseEntities = await context.Courses.ToListAsync();

        return courseEntities.Select(CourseFactory.Create);
    }
    public async Task<Course> UpdateCourseAsync(CourseUpdateRequest request)
    {
        await using var context = _contextFactory.CreateDbContext();
        var existingCourse = await context.Courses.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (existingCourse == null) return null!;

        existingCourse.ImageUri = request.ImageUri;
        existingCourse.ImageHeaderUri = request.ImageHeaderUri;
        existingCourse.IsBestSeller = request.IsBestSeller;
        existingCourse.IsDigital = request.IsDigital;
        existingCourse.Categories = request.Categories;
        existingCourse.Title = request.Title;
        existingCourse.Subtitle = request.Subtitle;
        existingCourse.StarRating = request.StarRating;
        existingCourse.Reviews = request.Reviews;
        existingCourse.LikesPercent = request.LikesPercent;
        existingCourse.Likes = request.Likes;
        existingCourse.Duration = request.Duration;

        existingCourse.Authors = request.Authors?.Select(a => new AuthorEntity
        {
            Name = a.Name
        }).ToList();

        existingCourse.Prices = request.Prices == null ? null : new PricesEntity
        {
            Currency = request.Prices.Currency,
            Price = request.Prices.Price,
            Discount = request.Prices.Discount
        };

        existingCourse.Content = request.Content == null ? null : new ContentEntity
        {
            Description = request.Content.Description,
            Includes = request.Content.Includes,
            ProgramDetails = request.Content.ProgramDetails?.Select(pd => new ProgramDetailItemEntity
            {
                Id = pd.Id,
                Title = pd.Title,
                Description = pd.Description
            }).ToList()
        };

        await context.SaveChangesAsync();
        return CourseFactory.Create(existingCourse);
    }
    public async Task<bool> DeleteCourseAsync(string id)
    {
        await using var context = _contextFactory.CreateDbContext();
        var courseEntity = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);

        if (courseEntity == null)
            return false;

        context.Courses.Remove(courseEntity);
        await context.SaveChangesAsync();
        return true;
    }
}
