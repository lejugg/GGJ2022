using System;
using UnityEngine;

namespace Interaction
{
    public abstract class Interaction : MonoBehaviour
    {
        event Action<Interaction> OnBegin = delegate {  };
        event Action<Interaction> OnComplete = delegate {  };
        event Action<Interaction> OnFail = delegate {  };

        protected void InvokeBegin()
        {
            Debug.Log("Begin interaction");
            OnBegin.Invoke(this);
            Game.IsInteracting = true;
        }
        
        protected void InvokeComplete()
        {
            Debug.Log("Complete interaction");
            OnComplete.Invoke(this);
            Game.IsInteracting = false;
        }
        
        protected void InvokeFail()
        {
            Debug.Log("Failed interaction");
            OnFail.Invoke(this);
            Game.IsInteracting = false;
        }
    }
}