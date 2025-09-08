using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lunar.Core.Interfaces;

/// <summary>
///     Resource system interface.
/// </summary>
public interface IResources
{
    /// <summary>
    ///     Loading resources.
    /// </summary>
    /// <param name="path">The path to the resource</param>
    /// <typeparam name="T">Resource type</typeparam>
    /// <returns>Resource</returns>
    T Load<T>(string path);

    /// <summary>
    ///     Loading resources from folder path
    /// </summary>
    /// <param name="path">The path to the folder</param>
    /// <typeparam name="T">Resource type</typeparam>
    /// <returns>Resources</returns>
    IEnumerable<T> LoadAll<T>(string path);

    /// <summary>
    ///     Loading resources from folder path
    /// </summary>
    /// <param name="paths">The paths to the resources</param>
    /// <typeparam name="T">Resource type</typeparam>
    /// <returns>Resources</returns>
    IEnumerable<T> LoadAll<T>(IEnumerable<string> paths);

    /// <summary>
    ///     Release resource.
    /// </summary>
    /// <param name="resource">Resource</param>
    /// <typeparam name="T">Resource type</typeparam>
    /// <returns></returns>
    void Release<T>(T resource);
}

/// <summary>
///     (Async) Resource system interface.
/// </summary>
public interface IResourcesAsync
{
    /// <summary>
    ///     (Async) Loading resources.
    /// </summary>
    /// <param name="path">The path to the resource</param>
    /// <param name="ct">CancellationToken</param>
    /// <typeparam name="T">Resource type</typeparam>
    /// <returns>Resource</returns>
    Task<T> LoadAsync<T>(string path, CancellationToken ct = default);

    /// <summary>
    ///     (Async) Loading resources from folder path
    /// </summary>
    /// <param name="path">The path to the folder</param>
    /// <param name="ct">CancellationToken</param>
    /// <typeparam name="T">Resource type</typeparam>
    /// <returns>Resources</returns>
    Task<IEnumerable<T>> LoadAllAsync<T>(string path, CancellationToken ct = default);

    /// <summary>
    ///     (Async) Loading resources from folder path
    /// </summary>
    /// <param name="paths">The paths to the resources</param>
    /// <param name="ct">CancellationToken</param>
    /// <typeparam name="T">Resource type</typeparam>
    /// <returns>Resources</returns>
    Task<IEnumerable<T>> LoadAllAsync<T>(IEnumerable<string> paths, CancellationToken ct = default);
}