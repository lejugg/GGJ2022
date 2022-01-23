using System;
using System.Collections.Generic;
using UnityEngine;

namespace Day
{
    public class DayManager : MonoBehaviour
    {
        [SerializeField] private List<Day> _days;
        
        public Day CurrentDay => _days[_currentDayIndex];
        public event Action<int> OnDayComplete = delegate {  }; 
        
        private int _currentDayIndex = 0;
        
        private void Awake()
        {
            InitializeAndSubscribeToDay();
        }

        private void HandleCurrentDayComplete()
        {
            OnDayComplete(_currentDayIndex);
            
            CurrentDay.OnDayComplete -= HandleCurrentDayComplete;
            _currentDayIndex++;

            if (_currentDayIndex == _days.Count)
            {
                Debug.Log("Game Over");
                return;
            }
            
            InitializeAndSubscribeToDay();
        }

        private void InitializeAndSubscribeToDay()
        {
            Debug.Log($"Today it's: {CurrentDay.gameObject.name}");
            CurrentDay.Initialize();
            CurrentDay.OnDayComplete += HandleCurrentDayComplete;
        }
    }
}