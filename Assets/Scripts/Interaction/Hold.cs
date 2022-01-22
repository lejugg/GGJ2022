using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Interaction
{
    public class Hold : Interaction
    {
        
        [SerializeField] private float duration = 3f;
        
        private Coroutine _routine;
        private bool completed;

        private void OnMouseDown()
        {
            transform.DOScale(1.1f, 0.25f).SetEase(Ease.OutBounce);
            InvokeBegin();

            completed = false;
            _routine = StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(duration);
            
            transform.DOScale(1f, 0.25f).SetEase(Ease.InBounce);
            InvokeComplete();
            completed = true;
        }

        private void OnMouseUp()
        {
            base.OnMouseUp();
            
            if (completed)
                return;

            StopCoroutine(_routine);
            transform.DOScale(1f, 0.25f).SetEase(Ease.InBounce);
            InvokeFail();
        }
    }
}