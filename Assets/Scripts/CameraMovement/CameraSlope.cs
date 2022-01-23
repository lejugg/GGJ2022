using UnityEngine;

namespace CameraMovement
{
    public class CameraSlope : MonoBehaviour
    {
        [SerializeField] private Transform bottom;
        [SerializeField] private Transform top;
        [SerializeField] private float scrollSpeed = 0.01f;
        
        private float _deltaPosition;

        private void Start()
        {
            _deltaPosition = 0.75f;
            LerpCamera();
        }

        private void Update()
        {
            LerpCamera();
            
            if (Input.mouseScrollDelta.y == 0f || Game.IsInteracting)
                return;

            _deltaPosition = Mathf.Clamp01(_deltaPosition + Input.mouseScrollDelta.y * scrollSpeed);
            // Debug.Log("Mousewheel " + _deltaPosition);
        }

        private void LerpCamera()
        {
            var targetPos = Vector3.Lerp(top.position, bottom.position, _deltaPosition);
            var targetRot = Quaternion.Lerp(top.rotation, bottom.rotation, _deltaPosition);
            
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.25f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 0.25f);
        }
    }
}