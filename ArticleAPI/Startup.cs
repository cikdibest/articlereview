using ArticleReview.Common.Business.Article;
using ArticleReview.Common.Data;
using ArticleReview.Common.Dto.Article;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;

namespace ArticleAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            int setMaxTop = Configuration.GetSection("ODataConfig").GetValue<int>("SetMaxTop");
            services.AddControllers()
                    .AddOData(opt => opt.AddRouteComponents("odata", GetEdmModel()).Filter().Select().Count().OrderBy().Expand().SkipToken().SetMaxTop(setMaxTop));

            services.AddDbContext<ArticleReviewDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DbConnection")));

            services.AddScoped<IArticleService, ArticleService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ArticleAPI", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ArticleAPI v1"));
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        IEdmModel GetEdmModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EntitySet<ArticleDto>("Article");
            return odataBuilder.GetEdmModel();
        }
    }
}
