using System.Collections.Generic;
using UnityEngine;

namespace Day
{
    public class DayManager : MonoBehaviour
    {
        [SerializeField] private List<Day> _days;
        
        private int _currentDayIndex = 0;
        public Day CurrentDay => _days[_currentDayIndex];
        
        private void Awake()
        {
            InitializeAndSubscribeToDay();
        }

        private void HandleCurrentDayComplete()
        {
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