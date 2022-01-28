using System;
using System.Collections.Generic;
using UnityEngine;

namespace Day
{
    public class DayManager : MonoBehaviour
    {
        [SerializeField] private List<Day> _days;
        
        public Day CurrentDay => _days[_currentDayIndex];
        public static event Action<int> OnDayComplete = delegate {  }; 
        public static event Action<int> OnStartNewDay = delegate {  }; 
        public static event Action<float> OnCurrentDayInteraction = delegate {  }; 
        
        private int _currentDayIndex = 0;
        
        private void Awake()
        {
            InitializeAndSubscribeToDay();
        }

        private void InitializeAndSubscribeToDay()
        {
            Debug.Log($"Today it's: {CurrentDay.gameObject.name}");
            OnStartNewDay(_currentDayIndex);
            CurrentDay.Initialize();
            CurrentDay.OnDayComplete += HandleCurrentDayComplete;
            CurrentDay.OnInteractionComplete += HandleInteractionComplete;
        }

        private void HandleCurrentDayComplete()
        {
            OnDayComplete(_currentDayIndex);
            
            CurrentDay.OnDayComplete -= HandleCurrentDayComplete;
            CurrentDay.OnInteractionComplete -= HandleInteractionComplete;

            _currentDayIndex++;

            if (_currentDayIndex == _days.Count)
            {
                Debug.Log("Game Over");
                return;
            }
            
            InitializeAndSubscribeToDay();
        }
        
        
        private void HandleInteractionComplete(float dayProgress)
        {
            OnCurrentDayInteraction(dayProgress);
        }

    }
}