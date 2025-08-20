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
        /// <param name="path">The path to the resource</param>
        /// <typeparam name="T">Resource type</typeparam>
        /// <returns></returns>
        void Release<T>(string path);
    }
}