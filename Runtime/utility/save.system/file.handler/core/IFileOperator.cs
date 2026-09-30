using Newtonsoft.Json;
using SGLib.Utility.SaveSystem.FileData;

namespace SGLib.Utility.SaveSystem.FileHandler
{
    public interface IFileOperator
    {
        public void SaveFile<T>(T gameData, bool overwrite) where T : GameDataBase;

        public T LoadFile<T>(string fileName, JsonSerializerSettings settings) where T : GameDataBase;

        public void DeleteFile(string fileName);

        public void DeleteAllFiles();
    }
}

