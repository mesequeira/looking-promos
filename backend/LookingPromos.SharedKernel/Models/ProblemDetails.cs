namespace LookingPromos.SharedKernel.Models;

/// <summary>
/// An object that represents a problem with the details like status, type, title, detail, and errors.
/// </summary>
public record ProblemDetails(
    int Status,
    string Type,
    string Title,
    string Detail,
    IEnumerable<object>? Errors
);
