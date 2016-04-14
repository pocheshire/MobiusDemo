using System;
using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class SystemExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var item in self)
                action(item);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> self)
        {
            return self == null || !self.Any();
        }

        public static string FormatWith (this string self, params object[] args)
        {
            return string.Format (self, args);
        }

        public static bool IsNullOrEmpty (this string self)
        {
            return string.IsNullOrEmpty(self);
        }
    }
}
