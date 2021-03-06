using UnityEngine;

namespace CameraMovement
{
    public class LazySusan : MonoBehaviour
    {
        [SerializeField, Range(0f, 5f)] private float rotationSpeed = 1f;
        [SerializeField] private bool invert = true;
        
        private Vector3 _previousInput;

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
                transform.RotateAround(transform.position, Vector3.up, delta.x * rotationSpeed * (invert ? -1f : 1f));
                _previousInput = Input.mousePosition;
            }
        }
    }
}