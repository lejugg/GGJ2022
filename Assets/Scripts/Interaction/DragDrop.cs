using DG.Tweening;
using UnityEngine;

namespace Interaction
{
    public class DragDrop : Interaction
    {
        [SerializeField] private Transform start;
        [SerializeField] private Transform destination;
        [SerializeField] private float snapDistance = 1f;
        
        private Vector3 _screenPoint;
        private Vector3 _offset;
        private float _distanceToDestination;


        private void OnMouseDown()
        {
            InvokeBegin();
            
            _screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            _offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
        }

        private void OnMouseDrag()
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;
            transform.position = curPosition;

            _distanceToDestination = Vector3.Distance(transform.position, destination.position);

            if (_distanceToDestination <= snapDistance)
            {
                destination.DOScale(1.25f, 0.25f);
            } 
            else
            {
                destination.DOScale(1f, 0.25f);
            }
        }

        private void OnMouseUp()
        {
            if (_distanceToDestination <= snapDistance)
            {
                InvokeComplete();
                transform.DOMove(destination.position, 0.25f).SetEase(Ease.OutCubic);
            } 
            else
            {
                InvokeFail();
                transform.DOMove(start.position, 0.75f).SetEase(Ease.OutCubic);
            }
            
            destination.DOScale(1f, 0.25f);
        }
    }
}