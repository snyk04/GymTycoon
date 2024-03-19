using Code.Scripts.Utils;
using UnityEngine;

namespace Code.Scripts.Highlight
{
    public sealed class ObjectHighlighter : MonoBehaviour
    {
        private HighlightableObject highlightedObject;
        
        private void Update()
        {
            if (!PhysicsExtensions.RaycastUnderMouse(out var hitInfo))
            {
                if (highlightedObject == null)
                {
                    return;
                }
                
                highlightedObject.Unhighlight();
                highlightedObject = null;
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
    }
}