using AutoMapper;

namespace NerdStore.WebApp.MVC.Configurations
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfig(this IServiceCollection services, Type[] types)
        {
            var mapperConfig = new MapperConfiguration(mc => {
                                                                 types.ToList().ForEach(mc.AddProfile);
                                                             });

            services.AddSingleton(mapperConfig.CreateMapper());
        }
    }
}
