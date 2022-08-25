using System.IO;
using System.Linq;
using System.Text.Json;
using Crip.Extensions.Tests.Exceptions;

namespace Crip.Extensions.Tests;

/// <summary>
/// Extension methods to load resources.
/// </summary>
public static class ResourceLoadExtensions
{
    /// <summary>
    /// Load embedded resource file as a <c>string</c>.
    /// </summary>
    /// <param name="src">Any assembly object where load resource.</param>
    /// <param name="resource">Resource partial name.</param>
    /// <returns>Resource loaded as a <c>string</c>.</returns>
    /// <exception cref="ManifestLoadException">
    /// Thrown when resource not found by provided <paramref name="resource"/>.
    /// </exception>
    public static string LoadResource(this object src, string resource)
    {
        var asm = src.GetType().Assembly;
        var name =
            asm.GetManifestResourceNames().First(n => n.Contains(resource)) ??
            throw new ManifestLoadException($"Could not find {resource} manifest file.");

        using var stream = asm.GetManifestResourceStream(name)!;
        using var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }

    /// <summary>
    /// Load embedded resource file and deserialize from JSON.
    /// </summary>
    /// <param name="src">Any assembly object where load resource.</param>
    /// <param name="resource">Resource partial name.</param>
    /// <param name="options">JSON deserialization options.</param>
    /// <typeparam name="T">The type of object to deserialize as.</typeparam>
    /// <returns>Resource loaded as a <typeparamref name="T"/> JSON object.</returns>
    /// <exception cref="ManifestLoadException">
    /// Thrown when resource not found by provided <paramref name="resource"/>.
    /// </exception>
    public static T LoadJsonResource<T>(this object src, string resource, JsonSerializerOptions? options = null)
    {
        options ??= new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        var json = src.LoadResource(resource);

        return JsonSerializer.Deserialize<T>(json, options);
    }
}