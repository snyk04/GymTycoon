using Code.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.UserInterface
{
    public sealed class SettingsController : MonoBehaviour
    {
        [Header("Canvas groups")]
        [SerializeField] private CanvasGroup settingsCanvasGroup;
        [SerializeField] private CanvasGroup mainMenuCanvasGroup;

        [Header("Buttons")] 
        [SerializeField] private Button backButton;

        private void Awake()
        {
            backButton.onClick.AddListener(BackButtonClicked);
        }

        private void OnDestroy()
        {
            backButton.onClick.RemoveListener(BackButtonClicked);
        }

        private void BackButtonClicked()
        {
            settingsCanvasGroup.SetActive(false);
            mainMenuCanvasGroup.SetActive(true);
        }
    }
}