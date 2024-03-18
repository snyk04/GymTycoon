using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Code.Scripts.Save
{
    public sealed class JsonGameSaveManager : IGameSaveManager
    {
        private const string SaveFileName = "save.json";

        private readonly string saveFilePath;
        
        public SaveData SaveData { get; private set; }

        public JsonGameSaveManager()
        {
            saveFilePath = Path.Combine(Application.persistentDataPath, SaveFileName);
            
            Initialize();
        }
        
        private void Initialize()
        {
            if (File.Exists(saveFilePath))
            {
                ReadSaveFile();
                return;
            }

            SaveData = new SaveData();
            Save();
        }

        private void ReadSaveFile()
        {
            var json = File.ReadAllText(saveFilePath);
            SaveData = JsonConvert.DeserializeObject<SaveData>(json);
        }
        
        public void Save()
        {
            var json = JsonConvert.SerializeObject(SaveData);
            File.WriteAllText(saveFilePath, json);
        }
        
        public async Task SaveAsync()
        {
            var json = JsonConvert.SerializeObject(SaveData);
            await File.WriteAllTextAsync(saveFilePath, json);
        }
    }
}