namespace FacilitaRhApi.Domain.Models;

public class Vacancy
{
    public int Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public int QtdeVagas { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public string Setor { get; set; } = string.Empty;
    public string Senioridade { get; set; } = string.Empty;
    public string Diversidade { get; set; } = string.Empty;
    public string Pcd { get; set; } = string.Empty;
    public string Salario { get; set; } = string.Empty;
    public string Contrato { get; set; } = string.Empty;
    public string Turno { get; set; } = string.Empty;
    public string Local { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public string DataAbertura { get; set; } = string.Empty;
    public string DataFechamento { get; set; } = string.Empty;

    // Navigation
    public ICollection<Application> Applications { get; set; } = new List<Application>();
}
