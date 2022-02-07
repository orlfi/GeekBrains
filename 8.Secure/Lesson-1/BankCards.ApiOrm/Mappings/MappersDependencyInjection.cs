using AutoMapper;

namespace BankCards.ApiOrm.Mappings
{
    public static class MappersDependencyInjection
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            // MapperConfiguration mapperConfiguration = new MapperConfiguration(mc => mc.AddProfile(new MapperProfile()));
            // IMapper mapper = mapperConfiguration.CreateMapper();
            // services.AddSingleton(mapper);
            services.AddAutoMapper(typeof(MapperProfile));
            return services;
        }
    }
}
