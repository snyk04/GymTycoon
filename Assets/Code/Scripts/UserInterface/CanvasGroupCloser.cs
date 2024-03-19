using Code.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.UserInterface
{
    public sealed class CanvasGroupCloser : MonoBehaviour
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private CanvasGroup canvasGroup;

        private void Awake()
        {
            closeButton.onClick.AddListener(Close);
        }

        private void OnDestroy()
        {
            closeButton.onClick.RemoveListener(Close);
        }

        private void Close()
        {
            canvasGroup.SetActive(false);
        }
    }
}