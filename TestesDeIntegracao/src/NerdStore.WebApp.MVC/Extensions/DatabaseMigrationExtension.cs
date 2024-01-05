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
            try
            {
                dataBaseContext.Database.Migrate();
            }
            catch (Exception)
            {

                throw new ArgumentException("Nao foi possivel estabelecer uma conexao com o banco de dados para garantir as Migrations, " +
                    "favor garantir que a connection string esteja correta");
            }
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
            catch (Exception)
            {
                Console.WriteLine("Banco de dados Ja populado"); 
            }
        }
    }
}
