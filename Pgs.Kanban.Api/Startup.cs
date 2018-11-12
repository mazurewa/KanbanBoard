using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pgs.Kanban.Domain;
using Pgs.Kanban.Domain.Services;
using Pgs.Kanban.Domain.Services.Interfaces;

namespace Pgs.Kanban.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();
            services.AddAutoMapper();
            services.AddSwaggerGen(c =>
          c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
          {
              Title = "PGS Kanban",
              Version = "v1",
          }));

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<KanbanContext>(x => x.UseSqlServer(connectionString));

            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<IListService, ListService>();
            services.AddScoped<ICardService, CardService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));

            app.UseCors(builder => builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin()
            );
            app.UseMvc();
        }
    }
}
