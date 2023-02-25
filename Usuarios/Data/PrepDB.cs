using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosService.Data
{
    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {   //context.Database.EnsureCreated();
            context.Database.Migrate();

            if (!context.Usuarios.Any())
            {

                Console.WriteLine("--> Seeding Data...");

                context.Usuarios.AddRange(
                    new Models.Usuario() { Nome = "Fulano", Email = "fulano@gmail.com" },
                    new Models.Usuario() { Nome = "Ciclano", Email = "ciclano@hotmail.com" },
                    new Models.Usuario() { Nome = "Kerberos", Email = "kerberos@yahoo.com" }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }

    }
}




