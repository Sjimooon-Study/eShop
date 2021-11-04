using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blazor.Helpers.LocalStorage
{
    public static class Extensions
    {
        /// <summary>
        /// Serialize object to string.
        /// </summary>
        /// <typeparam name="T">Type of object to serialize.</typeparam>
        /// <param name="obj"><typeparamref name="T"/> to serialize.</param>
        /// <returns>Serialized object.</returns>
        public static string Serialize<T>(this T obj)
        {
            try
            {
                return JsonSerializer.Serialize(obj);
            }
            catch (Exception e)
            {
                // TODO: Log exception.
            }

            return "";
        }

        /// <summary>
        /// Deserialize string to object.
        /// </summary>
        /// <typeparam name="T">Newable object.</typeparam>
        /// <param name="str"><see cref="string"/> to deserialize.</param>
        /// <returns>Deserialized <typeparamref name="T"/>; otherwise new <typeparamref name="T"/>.</returns>
        public static T Deserialize<T>(this string str, bool useCamelCasing = false) where T : new()
        {
            try
            {
                JsonSerializerOptions options = new();

                if (useCamelCasing)
                {
                    options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                }

                return JsonSerializer.Deserialize<T>(str, options);
            }
            catch (Exception e)
            {
                // TODO: Log exception.
            }

            return new T();
        }
    }
}
