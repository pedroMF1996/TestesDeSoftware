using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NerdStore.WebApp.MVC.Extensions
{
    public static class DatabaseMigrationExtension
    {
        public static void UseEnsureDatabaseMigrations<T>(this IApplicationBuilder app) where T : DbContext
        {
            var dataBaseContext = app.ApplicationServices.CreateScope()
                                        .ServiceProvider.GetRequiredService<T>();

            dataBaseContext.Database.Migrate();
        }

        public static void UseEnsureSeedDatabase<T>(this IApplicationBuilder app) where T : DbContext
        {
            var dataBaseContext = app.ApplicationServices.CreateScope()
                                        .ServiceProvider.GetRequiredService<T>();
            Seed(dataBaseContext);
        }

        private static void Seed<T>(T dbContext) where T : DbContext
        {
            try
            {
                string caminhoFisico = Path.Combine("..\\..\\sql\\ScriptInsert.sql");

                using (StreamReader leitor = new StreamReader(caminhoFisico))
                {
                    dbContext.Database.OpenConnection();
                    while (leitor.ReadLine() is string linha)
                    {
                        dbContext.Database.ExecuteSqlRaw(linha);
                    }
                    dbContext.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Erro ao alimentar o banco: {ex.Message}");
            }
            
        }
    }
}
