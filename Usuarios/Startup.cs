using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosService.Data;
using UsuariosService.Repository;
using Microsoft.EntityFrameworkCore;
using UsuariosService.AsyncDataService;

namespace Usuarios
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

            //services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
            //services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Integrated Security=true;")); //SQL ON LOCALHOST
            //services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(@"Server=localhost;database=UsuariosDb;User Id=sa;Password=A1b2c3d4e5f6", options => options.EnableRetryOnFailure())); //SQL ON DOCKER
            //services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), options => options.EnableRetryOnFailure())); //SQL ON DOCKER
            
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DockerConnection")));
            
            services.AddScoped<IUsuarioRepo, UsuarioRepo>();
            services.AddSingleton<IMessageBusClient, MessageBusClient>();

            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Usuarios", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Usuarios v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            PrepDB.PrepPopulation(app);            
        }
    }
}
