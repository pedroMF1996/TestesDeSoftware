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

        }

        private static void LerArquivo()
        {
            // Caminho relativo para o arquivo dentro do projeto
            string caminhoArquivo = Path.Combine("sql", "ScriptInsert.sql");

            try
            {
                string caminhoFisico = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", caminhoArquivo);

                using (StreamReader leitor = new StreamReader(caminhoFisico))
                {
                    string conteudo = leitor.ReadToEnd();
                    Console.WriteLine(conteudo);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Erro ao ler o arquivo: {ex.Message}");
            }
        }
    }
}
