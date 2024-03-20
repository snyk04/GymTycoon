using Code.Scripts.Save.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Scripts.UserInterface
{
    public sealed class MenuOpener : MonoBehaviour
    {
        [SerializeField] private int menuSceneIndex;

        private IGameSaveManager gameSaveManager;

        [Inject]
        private void Construct(IGameSaveManager gameSaveManager)
        {
            this.gameSaveManager = gameSaveManager;
        }
        
        private async void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape))
            {
                return;
            }
            
            await gameSaveManager.SaveAsync();
            SceneManager.LoadScene(menuSceneIndex);
        }
    }
}