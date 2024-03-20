using Code.Scripts.Save.Interfaces;
using Code.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace Code.Scripts.Tutorial
{
    public sealed class TutorialActivator : MonoBehaviour
    {
        [SerializeField] private CanvasGroup tutorialCanvasGroup;

        private IGameSaveManager gameSaveManager;

        [Inject]
        private void Construct(IGameSaveManager gameSaveManager)
        {
            this.gameSaveManager = gameSaveManager;
        }

        private void Start()
        {
            if (gameSaveManager.SaveData.TutorialShown)
            {
                return;
            }
            
            tutorialCanvasGroup.SetActive(true);
            gameSaveManager.SaveData.TutorialShown = true;
            gameSaveManager.Save();
        }
    }
}