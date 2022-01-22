using System;
using UnityEngine;

namespace Interaction
{
    public class DragDrop : MonoBehaviour, IInteraction
    {
        public event Action<IInteraction> OnBegin = delegate {  };
        public event Action<IInteraction> OnComplete = delegate {  };

        private Vector3 _screenPoint;
        private Vector3 _offset;

        private void OnMouseDown()
        {
            OnBegin.Invoke(this);
            Game.IsInteracting = true;

            _screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            _offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
        }

        private void OnMouseDrag()
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;
            transform.position = curPosition;
        }

        private void OnMouseUp()
        {
            OnComplete.Invoke(this);
            Game.IsInteracting = false;
        }
    }
}