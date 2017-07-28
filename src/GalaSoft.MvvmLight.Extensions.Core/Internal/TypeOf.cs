using Cactoos;
using System;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// typeof(T) which caches its content forever.
    /// </summary>
    /// <typeparam name="T">Target type.</typeparam>
    public struct TypeOf<T> : IScalar<Type>
    {
        private static readonly Type _type = typeof(T);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>Type of T.</returns>
        public Type Value() => _type;
    }
}
