using System;

namespace Emerlahn.Utilities
{
    public static class Guard
    {
        public static T NotNull<T>(T value, string parameterName) where T : class
        {
            return value ?? throw new ArgumentNullException(parameterName);
        }

        public static string NotNullOrWhiteSpace(string value, string parameterName)
        {
            return !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException($"{parameterName} cannot be null or whitespace");
        }
    }
}