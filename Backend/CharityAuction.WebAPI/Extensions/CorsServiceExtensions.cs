namespace CharityAuction.WebAPI.Extensions
{
    public static class CorsServiceExtensions
    {
        public static void AddCorsPolicies(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.WithOrigins("http://localhost:3000") // ????? ???? ?????
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials(); // ??????? ????, ??????????? ? ??????
                });
            });
        }
    }
}
