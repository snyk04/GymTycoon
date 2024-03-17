namespace Code.Scripts.Save
{
    public interface IGameSaveManager
    {
        public SaveData SaveData { get; }
        
        public void Save();
    }
}