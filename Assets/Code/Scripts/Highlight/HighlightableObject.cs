using UnityEngine;

namespace Code.Scripts.Highlight
{
    public sealed class HighlightableObject : MonoBehaviour
    {
        [SerializeField] private Color highlightColorTint;
        [SerializeField] private Renderer renderer;

        private Color defaultColor;

        private void Awake()
        {
            defaultColor = renderer.material.color;
        }

        public void Highlight()
        {
            renderer.material.color = defaultColor + highlightColorTint;
        }

        public void Unhighlight()
        {
            renderer.material.color = defaultColor;
        }
    }
}