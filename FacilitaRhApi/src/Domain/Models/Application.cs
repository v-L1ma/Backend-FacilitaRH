namespace FacilitaRhApi.Domain.Models;

public class Application
{
    public int Id { get; set; }
    public int VacancyId { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string DataNasc { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string ResumoProfissional { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public string Empresa { get; set; } = string.Empty;
    public string DataInicioEmpresa { get; set; } = string.Empty;
    public string DataTerminoEmpresa { get; set; } = string.Empty;
    public string DescricaoATVD { get; set; } = string.Empty;
    public string Situacao { get; set; } = string.Empty;
    public string Escolaridade { get; set; } = string.Empty;
    public string Curso { get; set; } = string.Empty;
    public string Instituicao { get; set; } = string.Empty;
    public string DataInicioEstudo { get; set; } = string.Empty;
    public string DataTerminoEstudos { get; set; } = string.Empty;

    // Navigation
    public Vacancy? Vacancy { get; set; }
}
