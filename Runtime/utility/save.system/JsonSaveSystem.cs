using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using SGLib.Utility.SaveSystem.FileData;
using SGLib.Utility.SaveSystem.FileHandler;
using SGLib.Utility.Management.Component;

namespace SGLib.SaveSystem
{
    public class JsonSaveSystem : ComponentBase
    {
        public GameDataBase GameData { get; set; }

        private FileOperator fileOperator;

        public override void Initialize()
        {
            var serializer = new Utility.SaveSystem.FileHandler.JsonSerializer();
            fileOperator = new FileOperator(serializer);
        }

        public void Save()
        {
            fileOperator.SaveFile(GameData, true);
        }

        public void Load<T>(string fileName, JsonSerializerSettings settings = null) where T : GameDataBase
        {
            GameData = fileOperator.LoadFile<T>(fileName, settings);
        }

        public void Delete(string fileName)
        {
            fileOperator.DeleteFile(fileName);
        }

        public void DeleteAll()
        {
            fileOperator.DeleteAllFiles();
        }

        public async UniTask SaveAsync(CancellationToken ct)
        {
            await fileOperator.SaveFileAsync(GameData, true, ct);
        }

        public async UniTask LoadAsync<T>(string fileName, JsonSerializerSettings settings = null, CancellationToken ct = default) where T : GameDataBase
        {
            GameData = await fileOperator.LoadFileAsync<T>(fileName, settings, ct);
        }
    }
}