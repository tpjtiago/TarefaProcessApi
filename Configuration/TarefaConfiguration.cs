using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TarefaProcessorApi.Models;

namespace TarefaProcessorApi.Configuration
{
    public class TarefaConfiguration : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.ToTable("Tarefa");
            builder.HasKey(u => u.Id);

            builder
                .Property(u => u.Id);

            builder
                .Property(u => u.Descricao);

            builder
                .Property(u => u.Status);

            builder
                .Property(u => u.DataInicioProcessamento);

            builder
                .Property(u => u.DataFimProcessamento);

        }
    }
}
