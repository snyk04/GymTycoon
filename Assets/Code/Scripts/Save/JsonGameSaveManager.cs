using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Code.Scripts.Save
{
    public sealed class JsonGameSaveManager : IGameSaveManager
    {
        private const string SaveFileName = "save.json";

        private string SaveFilePath => Path.Combine(Application.persistentDataPath, SaveFileName);
        
        public SaveData SaveData { get; private set; }

        public JsonGameSaveManager()
        {
            Initialize();
        }
        
        private void Initialize()
        {
            if (File.Exists(SaveFilePath))
            {
                ReadSaveFile();
                return;
            }

            SaveData = new SaveData();
            Save();
        }

        private void ReadSaveFile()
        {
            var json = File.ReadAllText(SaveFilePath);
            SaveData = JsonConvert.DeserializeObject<SaveData>(json);
        }
        
        public void Save()
        {
            var json = JsonConvert.SerializeObject(SaveData);
            File.WriteAllText(SaveFilePath, json);
        }
    }
}