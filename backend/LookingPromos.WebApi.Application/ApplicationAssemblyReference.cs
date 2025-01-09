using System.Reflection;

namespace LookingPromos.WebApi.Application;

/// <summary>
/// The class that contains the reference to the application assembly.
/// </summary>
public class ApplicationAssemblyReference
{
    /// <summary>
    /// An instance of the <see cref="Assembly"/> class that represents the application assembly.
    /// </summary>
    internal static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
}
