using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace RegistroLegal.Core.Aplications.Helpers
{
    public static class SesionHelper 
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value is not null ? JsonConvert.DeserializeObject<T>(value) : default;
        }
    }
}