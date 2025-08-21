using System.Threading;
using System.Threading.Tasks;

namespace Lunar.Interfaces
{
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
        ///     Release resource.
        /// </summary>
        /// <param name="resource">Resource</param>
        /// <typeparam name="T">Resource type</typeparam>
        /// <returns></returns>
        void Release<T>(T resource);

        /// <summary>
        ///     (Async) Loading resources.
        /// </summary>
        /// <param name="path">The path to the resource</param>
        /// <param name="ct">CancellationToken</param>
        /// <typeparam name="T">Resource type</typeparam>
        /// <returns>Resource</returns>
        Task<T> LoadAsync<T>(string path, CancellationToken ct = default);
    }
}