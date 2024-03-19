using Code.Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Scripts.Highlight
{
    public sealed class ObjectHighlighter : MonoBehaviour
    {
        private HighlightableObject highlightedObject;

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                UnhighlightCurrentObject();
                return;
            }
            
            if (!PhysicsExtensions.RaycastUnderMouse(out var hitInfo))
            {
                UnhighlightCurrentObject();
                return;
            }

            if (!hitInfo.collider.TryGetComponent<HighlightableObject>(out var highlightableObject))
            {
                return;
            }

            if (highlightedObject == highlightableObject)
            {
                return;
            }

            highlightableObject.Highlight();
            highlightedObject = highlightableObject;
        }

        private void UnhighlightCurrentObject()
        {
            if (highlightedObject == null)
            {
                return;
            }

            highlightedObject.Unhighlight();
            highlightedObject = null;
        }
    }
}