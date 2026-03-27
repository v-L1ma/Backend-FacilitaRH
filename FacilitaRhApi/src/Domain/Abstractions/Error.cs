namespace FacilitaRhApi.Domain.Abstractions;

public enum ErrorType { NotFound, Validation, Unauthorized }

public record Error(string Id, ErrorType Type, string Description);

public static class UserErrors
{
    public static Error EmailAlreadyRegistered => new("User.EmailAlreadyRegistered", ErrorType.Validation, "This email has been already registered.");
    public static Error NotFound => new("User.NotFound", ErrorType.NotFound, "User not found.");
    public static Error InvalidPassword => new("User.InvalidPassword", ErrorType.Validation, "Invalid password.");
    public static Error CreationFailed => new("User.CreationFailed", ErrorType.Validation, "Failed to create user.");
    public static Error SecretNotConfigured => new("User.SecretNotConfigured", ErrorType.Validation, "JWT Secret is not configured.");
}

public static class VacancyErrors
{
    public static Error NotFound => new("Vacancy.NotFound", ErrorType.NotFound, "Vacancy not found.");
    public static Error MissingData => new("Vacancy.MissingData", ErrorType.Validation, "Please provide all informations.");
}

public static class ApplicationErrors
{
    public static Error AlreadyApplied => new("Application.AlreadyApplied", ErrorType.Validation, "Already applied to this job vacancy.");
    public static Error NoneFound => new("Application.NoneFound", ErrorType.NotFound, "There are no applications.");
}
