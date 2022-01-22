using System;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;

namespace Interaction
{
    public class Click : MonoBehaviour, AbstractInteraction
    {
        public event Action<AbstractInteraction> OnBegin = delegate {  };
        public event Action<AbstractInteraction> OnComplete = delegate {  };

        private void OnMouseDown()
        {
            transform.DOJump(transform.position, 0.5f, 2, 0.75f);
            
            OnBegin.Invoke(this);
            OnComplete.Invoke(this);
        }

    }
}