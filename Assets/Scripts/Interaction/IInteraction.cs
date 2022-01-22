using System;

namespace Interaction
{
    public interface IInteraction
    {
        event Action<IInteraction> OnBegin;
        event Action<IInteraction> OnComplete;
    }
}