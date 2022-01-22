using DG.Tweening;

namespace Interaction
{
    public class Click : Interaction
    {
        
        private void OnMouseDown()
        {
            transform.DOJump(transform.position, 0.5f, 2, 0.25f);
            
            InvokeBegin();
            InvokeComplete();
        }
    }
}