using System;

namespace Marx.Utilities
{
    public static class PipeExtensions
    {

        /// <summary>
        /// Take an object, pass it as an argument to a function, return the result.
        /// </summary>
        public static TResult Pipe<T, TResult>(this T argument, Func<T, TResult> function) => function(argument);

        /// <summary>
        /// Take an object, pass it as an argument to a function, return the object.
        /// </summary>
        public static T PipeKeep<T, TResult>(this T argument, Func<T, TResult> function)
        {
            function(argument);
            return argument;
        }

        /// <summary>
        /// Take an object, pass it as an argument to a function, return the object.
        /// </summary>
        public static T PipeKeep<T>(this T argument, Action<T> function)
        {
            function(argument);
            return argument;
        }
    }
}
