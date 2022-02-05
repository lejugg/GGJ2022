using DG.Tweening;
using UnityEngine;

namespace Interactions
{
    public class Click : Interaction
    {
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnMouseDown()
        {
            _collider.enabled = false;
            transform.DOJump(transform.position, 0.7f, 3, 0.5f).OnComplete( () => _collider.enabled = true);
            
            InvokeBegin();
            InvokeComplete();
        }
    }
}