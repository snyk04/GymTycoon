using UnityEngine;

namespace Code.Scripts.Utils
{
    public static class PhysicsExtensions
    {
        public static bool RaycastUnderMouse(out RaycastHit hitInfo)
        {
            var camera = UnityEngine.Camera.main;
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(ray, out hitInfo) ;
        }
    }
}