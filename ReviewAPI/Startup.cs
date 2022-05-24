using ArticleReview.Common.Business.Review;
using ArticleReview.Common.Data;
using ArticleReview.Common.Dto.Article;
using ArticleReview.Common.Dto.Review;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using System.Linq;

namespace ReviewAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            int setMaxTop = Configuration.GetSection("ODataConfig").GetValue<int>("SetMaxTop");
            services.AddControllers()
                    .AddOData(opt => opt.AddRouteComponents("v1", GetEdmModel()).Filter().Select().Count().OrderBy().Expand().SkipToken().SetMaxTop(setMaxTop));

            services.AddDbContext<ArticleReviewDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DbConnection")));

            services.AddScoped<IReviewService, ReviewService>();

            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.IgnoreObsoleteActions();
                c.IgnoreObsoleteProperties();
                c.CustomSchemaIds(type => type.FullName);
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ArticleAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReviewAPI v1"));
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

            odataBuilder.EntitySet<ReviewDto>("Review");
            return odataBuilder.GetEdmModel();
        }
    }
}
