using System.Text.Json;

namespace ClientLibrary.Helper
{
    public static class Serialization
    {
        public static string Serializeobj<T>(T modelobj) => JsonSerializer.Serialize(modelobj);
        public static T Deserializjsonsrting<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString);
        public static IList<T> DeserializjsonsrtingList<T>(string jsonString) => JsonSerializer.Deserialize<IList<T>>(jsonString);
    }
}
