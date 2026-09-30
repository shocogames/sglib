using Newtonsoft.Json;

namespace SGLib.Utility.SaveSystem.FileHandler
{
    public interface ISerializer
    {
        public abstract string Extension { get; }

        public string Serialize<T>(T serializableObject);

        public T Deserialize<T>(string json, JsonSerializerSettings settings);
    }
}
