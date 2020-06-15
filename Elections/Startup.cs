using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elections.Models;
using Elections.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Elections
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
            services.AddDbContext<ElectionsContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:ElectionsDB"]));
            services.AddScoped<IDataRepository<Voters>, VotersRepository>();
            services.AddScoped<IDataRepository<Candidates>, CandidatesRepository>();
            services.AddScoped<IDataRepository<Category>, CategoryRepository>();
            services.AddScoped<IDataRepository<CandidateCategory>, CandidateCategoryRepository>();
            services.AddScoped<IDataRepository<Vote>, VoteRepository>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
