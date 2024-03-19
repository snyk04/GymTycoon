using System.Threading.Tasks;
using Code.Scripts.Save.Models;

namespace Code.Scripts.Save.Interfaces
{
    public interface IGameSaveManager
    {
        public SaveData SaveData { get; }
        
        public void Save();
        public Task SaveAsync();
    }
}