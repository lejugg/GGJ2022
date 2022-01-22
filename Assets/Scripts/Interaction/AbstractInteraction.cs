using System;

namespace Interaction
{
    public interface AbstractInteraction
    {
        event Action<AbstractInteraction> OnBegin;
        event Action<AbstractInteraction> OnComplete;
    }
}