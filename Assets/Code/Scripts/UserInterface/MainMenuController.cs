using Code.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.Scripts.UserInterface
{
    public sealed class MainMenuController : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button playButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitButton;
        
        [Header("Canvas groups")]
        [SerializeField] private CanvasGroup mainMenuCanvasGroup;
        [SerializeField] private CanvasGroup settingsCanvasGroup;
        
        [Header("Settings")]
        [SerializeField] private int gameSceneIndex;

        private void Awake()
        {
            playButton.onClick.AddListener(PlayButtonClicked);
            settingsButton.onClick.AddListener(SettingsButtonClicked);
            quitButton.onClick.AddListener(QuitButtonClicked);
        }

        private void OnDestroy()
        {
            playButton.onClick.RemoveListener(PlayButtonClicked);
            settingsButton.onClick.RemoveListener(SettingsButtonClicked);
            quitButton.onClick.RemoveListener(QuitButtonClicked);
        }

        private void PlayButtonClicked()
        {
            SceneManager.LoadScene(gameSceneIndex);
        }

        private void SettingsButtonClicked()
        {
            mainMenuCanvasGroup.SetActive(false);
            settingsCanvasGroup.SetActive(true);
        }

        private void QuitButtonClicked()
        {
            Application.Quit();
        }
    }
}