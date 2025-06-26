using Microsoft.EntityFrameworkCore;
using TarefaProcessorApi.Configuration;
using TarefaProcessorApi.Models;

namespace TarefaProcessorApi.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TarefaConfiguration());

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(PosTechDesafioContexto).Assembly);
        }
    }
}
