using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Days
{
    public class DayManager : MonoBehaviour
    {
        [SerializeField] private List<Day> _days;
        
        public static DayManager Instance { get; private set; }

        public ReactiveProperty<Day> CurrentDay { get; private set; }

        private int _currentDayIndex = 0;
        
        private void Awake()
        {
            Instance = this;
            CurrentDay = new ReactiveProperty<Day>();
            InitializeAndSubscribeToDays();
        }

        private void InitializeAndSubscribeToDays()
        {
            var currentDay = _days[_currentDayIndex];
            Debug.Log($"Today is: {currentDay.gameObject.name}");
            
            currentDay.OnDayEnd += HandleCurrentDayEnd;
            currentDay.StartDay(_currentDayIndex);
            CurrentDay.Value = currentDay;
        }

        private void HandleCurrentDayEnd()
        {
            CurrentDay.Value.OnDayEnd -= HandleCurrentDayEnd;

            _currentDayIndex++;

            if (_currentDayIndex == _days.Count)
            {
                Debug.Log("Game Over");
                return;
            }
            
            InitializeAndSubscribeToDays();
        }
    }
}