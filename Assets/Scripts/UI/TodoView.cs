using System;
using System.Collections.Generic;
using Day;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TodoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI listText;
        [SerializeField] private Button close;
        [SerializeField] private List<Day.Day> _days;

        private bool toggle = false;
        private RectTransform _rect;
        private int _currentDayIndex;

        private void Awake()
        {
            DayManager.OnStartNewDay += HandleStartNewDay;
            _rect = GetComponent<RectTransform>();
            close.onClick.AddListener( HandleClose);
        }

        private void Start()
        {
            
            DayManager.OnCurrentDayInteraction += HandleNewInteraction;
        }

        private void HandleNewInteraction(float dayProgress)
        {
            HandleStartNewDay(_currentDayIndex);
        }

        private void HandleClose()
        {
            toggle = !toggle;

            _rect.DOAnchorPosY(toggle ? 600f : 0f, 0.5f).SetEase(Ease.OutBack);
        }

        private void HandleStartNewDay(int index)
        {
            _currentDayIndex = index;
            
            var tasks = "";
            var interactions = _days[index].Interactions;
            
            foreach (var interaction in interactions)
            {
                tasks += interaction.WasCompletedToday ? "DONE: " : "";
                tasks += interaction.Description + "\n";
            }

            listText.text = tasks;
        }
    }
}