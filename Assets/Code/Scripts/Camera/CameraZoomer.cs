using System;
using UnityEngine;

namespace Code.Scripts.Camera
{
    public sealed class CameraZoomer : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera camera;
        [SerializeField] private float zoomSpeed;
        [SerializeField] private float minZoom;
        [SerializeField] private float maxZoom;
        
        private void Update()
        {
            if (!(Input.mouseScrollDelta.magnitude > 0))
            {
                return;
            }
            
            camera.orthographicSize -= Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime;
            camera.orthographicSize = Math.Clamp(camera.orthographicSize, minZoom, maxZoom);
        }
    }
}