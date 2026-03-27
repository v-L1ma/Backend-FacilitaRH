using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FacilitaRhApi.Infrastructure.Configurations;

public class ApplicationConfiguration : IEntityTypeConfiguration<Domain.Models.Application>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Application> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.NomeCompleto).IsRequired();
        builder.Property(a => a.Email).IsRequired();
        builder.Property(a => a.Telefone).IsRequired();
        builder.Property(a => a.DataNasc).IsRequired();
        builder.Property(a => a.Cpf).IsRequired();
        builder.Property(a => a.ResumoProfissional).IsRequired();
        builder.Property(a => a.Cargo).IsRequired();
        builder.Property(a => a.Empresa).IsRequired();
        builder.Property(a => a.DataInicioEmpresa).IsRequired();
        builder.Property(a => a.DataTerminoEmpresa).IsRequired();
        builder.Property(a => a.DescricaoATVD).IsRequired();
        builder.Property(a => a.Situacao).IsRequired();
        builder.Property(a => a.Escolaridade).IsRequired();
        builder.Property(a => a.Curso).IsRequired();
        builder.Property(a => a.Instituicao).IsRequired();
        builder.Property(a => a.DataInicioEstudo).IsRequired();
        builder.Property(a => a.DataTerminoEstudos).IsRequired();

        builder.HasIndex(a => a.Email).IsUnique();
        builder.HasIndex(a => a.Cpf).IsUnique();

        builder.HasOne(a => a.Vacancy)
               .WithMany(v => v.Applications)
               .HasForeignKey(a => a.VacancyId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
