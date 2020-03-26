namespace HappyThoughts.Services.Data.Tests.Common
{
    using System.Reflection;

    using HappyThoughts.Services.Data.Topics;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.Topics;

    public class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
               typeof(TopicsService).GetTypeInfo().Assembly,
               typeof(TopicDetailsViewModel).GetTypeInfo().Assembly);
        }
    }
}
