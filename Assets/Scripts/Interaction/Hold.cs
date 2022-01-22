using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Interaction
{
    public class Hold : MonoBehaviour, IInteraction
    {
        public event Action<IInteraction> OnBegin = delegate {  };
        public event Action<IInteraction> OnComplete = delegate {  };

        [SerializeField] private float duration = 3f;
        private Coroutine _routine;

        private void OnMouseDown()
        {
            transform.DOScale(1.1f, 0.25f).SetEase(Ease.OutBounce);
            OnBegin.Invoke(this);
            Game.IsInteracting = true;

            _routine = StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            var t = 0f;
            while (t < duration)
            {
                yield return new WaitForEndOfFrame();
                t += Time.deltaTime;
            }
            
            transform.DOScale(1f, 0.25f).SetEase(Ease.InBounce);
            OnComplete.Invoke(this);
            Debug.Log("Success hold");
        }

        private void OnMouseUp()
        {
            Debug.Log("End Holding");
            Game.IsInteracting = false;
            transform.DOScale(1f, 0.25f).SetEase(Ease.InBounce);
            StopCoroutine(_routine);
        }
    }
}