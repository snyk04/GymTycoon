using UnityEngine;

namespace Code.Scripts.Camera
{
    public sealed class CameraMover : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera camera;
        [SerializeField] private float movementSpeed;

        private Vector3 lastMousePosition;

        private void Update()
        {
            if (Input.GetMouseButton(2))
            {
                if (lastMousePosition == default)
                {
                    lastMousePosition = Input.mousePosition;
                    return;
                }

                Move();

                lastMousePosition = Input.mousePosition;
                return;
            }

            lastMousePosition = default;
        }

        private void Move()
        {
            var delta = Input.mousePosition - lastMousePosition;
            var cameraPosition = camera.transform.position;
            var newPosition = cameraPosition +
                              -camera.transform.forward * (delta.y * movementSpeed * camera.orthographicSize * Time.deltaTime) +
                              -camera.transform.right * (delta.x * movementSpeed * camera.orthographicSize * Time.deltaTime);
            newPosition.y = cameraPosition.y;
            camera.transform.position = newPosition;
        }
    }
}