using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FacilitaRhApi.Domain.Models;

namespace FacilitaRhApi.Infrastructure.Configurations;

public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
{
    public void Configure(EntityTypeBuilder<Vacancy> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Status).IsRequired();
        builder.Property(v => v.Titulo).IsRequired();
        builder.Property(v => v.QtdeVagas).IsRequired();
        builder.Property(v => v.Descricao).IsRequired();
        builder.Property(v => v.Setor).IsRequired();
        builder.Property(v => v.Senioridade).IsRequired();
        builder.Property(v => v.Diversidade).IsRequired();
        builder.Property(v => v.Pcd).IsRequired();
        builder.Property(v => v.Salario).IsRequired();
        builder.Property(v => v.Contrato).IsRequired();
        builder.Property(v => v.Turno).IsRequired();
        builder.Property(v => v.Local).IsRequired();
        builder.Property(v => v.Endereco).IsRequired();
        builder.Property(v => v.DataAbertura).IsRequired();
        builder.Property(v => v.DataFechamento).IsRequired();

        builder.HasMany(v => v.Applications)
               .WithOne(a => a.Vacancy)
               .HasForeignKey(a => a.VacancyId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
