using System;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;

namespace Interaction
{
    public class Click : MonoBehaviour, IInteraction
    {
        public event Action<IInteraction> OnBegin = delegate {  };
        public event Action<IInteraction> OnComplete = delegate {  };

        private void OnMouseDown()
        {
            transform.DOLocalJump(transform.position, 0.5f, 2, 0.25f);
            
            OnBegin.Invoke(this);
            OnComplete.Invoke(this);
        }

    }
}