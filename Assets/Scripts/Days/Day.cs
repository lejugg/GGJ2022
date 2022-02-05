using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using Interactions;
using UniRx;

namespace Days
{
    public class Day : MonoBehaviour
    {
        [SerializeField] private List<Interaction> sortedTasks;
        [SerializeField] private List<Interaction> forbiddenTasks;

        public List<Interaction> Interactions => sortedTasks.Where(interaction => interaction!= null).ToList();
        public event Action OnDayEnd = delegate {  };
        public int DayIndex { get; private set; }
        public ReactiveProperty<Interaction> CurrentInteraction { get; private set; }

        public float DayProgress => 1f - ((_taskQueue.Count + 0.5f) / Interactions.Count); 
        
        private Queue<Interaction> _taskQueue = new Queue<Interaction>();

        public void StartDay(int dayIndex)
        {
            DayIndex = dayIndex;
            
            foreach (var interaction in Interactions)
            {
                _taskQueue.Enqueue(interaction);
            }

            foreach (var forbiddenTask in forbiddenTasks)
            {
                forbiddenTask.SetIsForbidden(true);
            }
            
            SubscribeToNextTask();
        }
        
        private void SubscribeToNextTask()
        {
            if (CurrentInteraction == null)
            {
                CurrentInteraction = new ReactiveProperty<Interaction>();
            }
            
            CurrentInteraction.Value = _taskQueue.Dequeue();
            CurrentInteraction.Value.SetIsCurrentTask(true);
            CurrentInteraction.Value.OnComplete += InteractionCompleted;
            
            Debug.Log($"Next: {CurrentInteraction.Value.Description}");
        }
        
        private void InteractionCompleted(Interaction interaction)
        {
            interaction.OnComplete -= InteractionCompleted;
            CurrentInteraction.Value.SetIsCurrentTask(false);
            
            if (_taskQueue.Count == 0)
            {
                OnDayEnd();
                return;
            }
            
            SubscribeToNextTask();
        }


    }
}