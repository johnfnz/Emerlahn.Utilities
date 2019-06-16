using System;

namespace Utilities
{
    public static class New
    {
        public static Lazy<T> Lazy<T>(T instance) => new Lazy<T>(instance);
        public static Lazy<T> Lazy<T>(Func<T> factory) => new Lazy<T>(factory);
    }
}
