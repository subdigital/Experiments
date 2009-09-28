using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace evo.Core.Extensions
{
    public static class ReflectionExtensions
    {
        public static IEnumerable<Type> GetTypesImplementing<TBase>(this Assembly assembly)
        {
            return assembly.GetTypes().Where(t => t.Imlpements<TBase>());
        }

        public static bool Imlpements<TBase>(this Type type)
        {
            return (typeof (TBase).IsAssignableFrom(type));
        }

        public static bool HasAttribute<TAttribute>(this Type type) 
        {
            var attribs = type.GetCustomAttributes(typeof (TAttribute), true);
            return attribs != null && attribs.Length > 0;
        }

        public static TAttribute GetAttribute<TAttribute>(this Type type)
        {
            var attribs = type.GetCustomAttributes(typeof (TAttribute), true);
            return (TAttribute) attribs[0];
        }
    }
}