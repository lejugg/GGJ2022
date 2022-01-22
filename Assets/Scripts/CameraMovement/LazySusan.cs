using UnityEngine;

namespace CameraMovement
{
    public class LazySusan : MonoBehaviour
    {
        [SerializeField, Range(0f, 5f)] private float rotationSpeed = 1f;
        
        private Vector3 _previousInput;

        private void Update()
        {
            if (Game.IsInteracting)
                return;
            
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                _previousInput = Input.mousePosition;
            }
            
            if (Input.GetKey(KeyCode.Mouse0))
            {
                var delta = Input.mousePosition - _previousInput;
                transform.RotateAround(transform.position, Vector3.up, delta.x * rotationSpeed);
                _previousInput = Input.mousePosition;
            }
        }
    }
}