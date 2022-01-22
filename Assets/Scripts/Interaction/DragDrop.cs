using DG.Tweening;
using UnityEngine;

namespace Interaction
{
    public class DragDrop : Interaction
    {
        [SerializeField] private Transform start;
        [SerializeField] private Transform destination;
        [SerializeField] private float snapDistance = 1f;
        [SerializeField] private float hoverHeight = 0.5f;
        
        private float _distanceToDestination;
        private Plane _plane;

        private void OnMouseDown()
        {
            InvokeBegin();
            
            _plane = new Plane(Vector3.up, transform.position); // ground plane
        }

        private void OnMouseDrag()
        {
            MoveObject();
            ShowSnapDistance();
        }

        private void MoveObject()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(_plane.Raycast(ray, out var distance))
            {
                transform.DOMove(ray.GetPoint(distance) + Vector3.up * hoverHeight, 0.1f);
            }
        }

        private void ShowSnapDistance()
        {
            _distanceToDestination = Vector3.Distance(transform.position, destination.position);

            destination.DOScale(_distanceToDestination <= snapDistance ? 1.25f : 1f, 0.25f);
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