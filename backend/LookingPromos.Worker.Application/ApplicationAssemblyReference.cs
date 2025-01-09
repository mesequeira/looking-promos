using System.Reflection;

namespace LookingPromos.Worker.Application;

/// <summary>
/// The application assembly reference.
/// </summary>
internal sealed class ApplicationAssemblyReference
{
    /// <summary>
    /// An instance of the application assembly.
    /// </summary>
    internal static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
}
