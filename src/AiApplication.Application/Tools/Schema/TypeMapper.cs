using System;

namespace AiApplication.Application.Tools.Schema
{
    internal static class TypeMapper
    {
        public static string Map(Type type)
        {
            Type actualType =
                Nullable.GetUnderlyingType(type) ?? type;

            if (actualType == typeof(string))
            {
                return "string";
            }

            if (actualType == typeof(bool))
            {
                return "boolean";
            }

            if (actualType == typeof(int) ||
                actualType == typeof(long) ||
                actualType == typeof(short))
            {
                return "integer";
            }

            if (actualType == typeof(float) ||
                actualType == typeof(double) ||
                actualType == typeof(decimal))
            {
                return "number";
            }

            if (actualType.IsArray)
            {
                return "array";
            }

            return "object";
        }
    }
}