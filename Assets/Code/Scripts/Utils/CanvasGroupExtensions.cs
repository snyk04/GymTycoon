using UnityEngine;

namespace Code.Scripts.Utils
{
    public static class CanvasGroupExtensions
    {
        public static void SetActive(this CanvasGroup canvasGroup, bool isActive)
        {
            canvasGroup.alpha = isActive ? 1 : 0;
            canvasGroup.interactable = isActive;
            canvasGroup.blocksRaycasts = isActive;
        }
    }
}