using System;
using System.Collections.Generic;
using UnityEngine;

namespace Day
{
    public class Day : MonoBehaviour
    {
        [SerializeField] private List<Interaction.Interaction> interactions;
        
        public event Action OnDayComplete = delegate {  };
        
        private Queue<Interaction.Interaction> _taskQueue = new Queue<Interaction.Interaction>();
        private Interaction.Interaction _currentInteraction;
        
        public void Initialize()
        {
            foreach (var interaction in interactions)
            {
                _taskQueue.Enqueue(interaction);
            }
            
            NextDayAndSubscribe();
        }

        private void InteractionCompleted(Interaction.Interaction interaction)
        {
            interaction.OnComplete -= InteractionCompleted;

            if (_taskQueue.Count == 0)
            {
                OnDayComplete();
                return;
            }
            
            NextDayAndSubscribe();
        }

        private void NextDayAndSubscribe()
        {
            _currentInteraction = _taskQueue.Dequeue();
            _currentInteraction.OnComplete += InteractionCompleted;
            
            Debug.Log($"Do {_currentInteraction.gameObject.name}: {_taskQueue.Count} left");
        }
    }
}