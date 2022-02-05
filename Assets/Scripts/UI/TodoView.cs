using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Days;
using DG.Tweening;
using Interactions;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TodoView : MonoBehaviour
    {
        [SerializeField] private TodoListElement listTextElement;
        [SerializeField] private RectTransform textParent;
        [SerializeField] private Button close;

        private bool toggle;
        private RectTransform _rect;
        private Day _currentDay;
        private List<TodoListElement> _textLines = new List<TodoListElement>();
        private CompositeDisposable _disposable = new CompositeDisposable();

        private void Awake()
        {
            DayManager.Instance.CurrentDay.Subscribe(HandleDayStart);
            _rect = GetComponent<RectTransform>();
            close.onClick.AddListener( HandleClose);
        }

        private void HandleClose()
        {
            toggle = !toggle;

            var height = textParent.rect.height - 100f;
            _rect.DOAnchorPosY(toggle ? (height) : 0f, 0.5f).SetEase(Ease.OutBack);
        }
        
        private void HandleDayStart(Day day)
        {
            _disposable.Clear();
            
            _currentDay = day;
            var interactions = day.Interactions;

            foreach (var textLine in _textLines)
            {
                Destroy(textLine.gameObject);
            }
            _textLines.Clear();
            
            foreach (var interaction in interactions)
            {
                var textLine = Instantiate(listTextElement, textParent);
                _textLines.Add(textLine);
            }

            _currentDay.CurrentInteraction.Subscribe(OnNewInteraction).AddTo(_disposable);
        }

        private void OnNewInteraction(Interaction interaction)
        {
            interaction.WasCompletedToday.Subscribe(_ => UpdateTextLines());
        }

        private void UpdateTextLines()
        {
            for (var index = 0; index < _textLines.Count; index++)
            {
                var textLine = _textLines[index];
                var interaction = _currentDay.Interactions[index];
                textLine.Text.text = interaction.Description;
                textLine.Text.fontStyle = interaction.WasCompletedToday.Value ? FontStyles.Strikethrough : FontStyles.Normal;
            }
        }
    }
}