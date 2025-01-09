using System.Reflection;

namespace LookingPromos.SharedKernel.Persistence;

/// <summary>
/// The class that contains the reference to the persistence assembly.
/// </summary>
public class PersistenceAssemblyReference
{
    /// <summary>
    /// An instance of the <see cref="Assembly"/> class that represents the persistence assembly.
    /// </summary>
    internal static readonly Assembly Assembly = typeof(PersistenceAssemblyReference).Assembly;
}
