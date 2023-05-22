using System;
using System.Diagnostics.CodeAnalysis;

namespace PropterHoc
{
    public static class ParameterExtensions
    {
        public static string OrIfNullOrWhiteSpace(this string? parameter, string defaultValue)
        {
            return string.IsNullOrWhiteSpace(parameter)
                ? defaultValue
                : parameter;
        }

        public static T CheckNotNull<T>([NotNull] this T? parameter)
            where T : notnull
        {
            if (parameter is null)
            {
                throw new ArgumentNullException();
            }

            return parameter;
        }

        public static string CheckNotEmpty(this string parameter)
        {
            if (parameter.Length == 0)
            {
                throw new ArgumentException();
            }

            return parameter;
        }

        public static string CheckNotNullOrWhiteSpace([NotNull] this string? parameter)
        {
            if (string.IsNullOrWhiteSpace(parameter))
            {
                throw new ArgumentException();
            }

            return parameter;
        }

        public static TOut AndThen<TIn, TOut>(this TIn input, Func<TIn, TOut> func)
        {
            return func(input);
        }
    }
}