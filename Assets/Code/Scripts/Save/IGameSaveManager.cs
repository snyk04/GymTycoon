using System.Threading.Tasks;

namespace Code.Scripts.Save
{
    public interface IGameSaveManager
    {
        public SaveData SaveData { get; }
        
        public void Save();
        public Task SaveAsync();
    }
}