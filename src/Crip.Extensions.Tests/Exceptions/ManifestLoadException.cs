using System;

namespace Crip.Extensions.Tests.Exceptions;

/// <summary>
/// Manifest load exception.
/// </summary>
public class ManifestLoadException : ApplicationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ManifestLoadException"/> class.
    /// </summary>
    /// <param name="resource">Path where resource load failed.</param>
    public ManifestLoadException(string resource)
        : base($"Could not find {resource} manifest file.")
    {
    }
}