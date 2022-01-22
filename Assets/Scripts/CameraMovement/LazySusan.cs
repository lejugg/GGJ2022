using UnityEngine;

namespace CameraMovement
{
    public class LazySusan : MonoBehaviour
    {
        [SerializeField, Range(0f, 5f)] private float rotationSpeed = 1f;
        
        private Vector3 _previousInput;
        private Transform _cameraTransform;

        private void Awake()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _previousInput = Input.mousePosition;
            }
            
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Game.IsInteracting)
                {
                    _previousInput = Input.mousePosition;
                    return;
                }
                
                var delta = Input.mousePosition - _previousInput;
                _cameraTransform.RotateAround(transform.position, Vector3.up, delta.x * rotationSpeed);
                _previousInput = Input.mousePosition;
            }
        }
    }
}