using CourseProvider.Infrastructure.Data.Entities;

namespace CourseProvider.Infrastructure.GraphQL.ObjectTypes;

public class CourseType : ObjectType<CourseEntity>
{
    protected override void Configure(IObjectTypeDescriptor<CourseEntity> descriptor)
    {
        descriptor.Field(c => c.Id).Type<NonNullType<IdType>>();
        descriptor.Field(c => c.IsBestSeller).Type<BooleanType>();
        descriptor.Field(c => c.IsDigital).Type<BooleanType>();
        descriptor.Field(c => c.Categories).Type<ListType<StringType>>();
        descriptor.Field(c => c.Title).Type<StringType>();
        descriptor.Field(c => c.Subtitle).Type<StringType>();
        descriptor.Field(c => c.StarRating).Type<DecimalType>();
        descriptor.Field(c => c.Reviews).Type<StringType>();
        descriptor.Field(c => c.LikesPercent).Type<StringType>();
        descriptor.Field(c => c.Likes).Type<StringType>();
        descriptor.Field(c => c.Duration).Type<StringType>();
        descriptor.Field(c => c.Authors).Type<ListType<AuthorType>>();
        descriptor.Field(c => c.Prices).Type<PricesType>();
        descriptor.Field(c => c.Content).Type<ContentType>();
    }

    public class AuthorType : ObjectType<AuthorEntity>
    {
        protected override void Configure(IObjectTypeDescriptor<AuthorEntity> descriptor)
        {
            descriptor.Field(a => a.Name).Type<StringType>();
        }
    }

    public class PricesType : ObjectType<PricesEntity>
    {
        protected override void Configure(IObjectTypeDescriptor<PricesEntity> descriptor)
        {
            descriptor.Field(p => p.Currency).Type<StringType>();
            descriptor.Field(p => p.Price).Type<DecimalType>();
            descriptor.Field(p => p.Discount).Type<DecimalType>();
        }
    }

    public class ContentType : ObjectType<ContentEntity>
    {
        protected override void Configure(IObjectTypeDescriptor<ContentEntity> descriptor)
        {
            descriptor.Field(c => c.Description).Type<StringType>();
            descriptor.Field(c => c.Includes).Type<ListType<StringType>>();
            descriptor.Field(c => c.ProgramDetails).Type<ListType<ProgramDetailItemType>>();
        }
    }

    public class ProgramDetailItemType : ObjectType<ProgramDetailItemEntity>
    {
        protected override void Configure(IObjectTypeDescriptor<ProgramDetailItemEntity> descriptor)
        {
            descriptor.Field(pd => pd.Id).Type<IntType>();
            descriptor.Field(pd => pd.Title).Type<StringType>();
            descriptor.Field(pd => pd.Description).Type<StringType>();
        }
    }
}
