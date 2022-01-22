using System;
using DG.Tweening;
using UnityEngine;

namespace Interaction
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
            transform.DOJump(transform.position, 0.5f, 2, 0.25f).OnComplete( () => _collider.enabled = true);
            
            InvokeBegin();
            InvokeComplete();
        }
    }
}