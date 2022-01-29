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

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            DayManager.OnStartNewDay += HandleStartNewDay;
            close.onClick.AddListener( HandleClose);
        }

        private void HandleClose()
        {
            toggle = !toggle;

            _rect.DOAnchorPosY(toggle ? 600f : 0f, 0.5f).SetEase(Ease.OutBack);
        }

        private void HandleStartNewDay(int index)
        {
            var tasks = "";
            var interactions = _days[index].Interactions;
            
            foreach (var interaction in interactions)
            {
                tasks += interaction.Description + "\n";
            }

            listText.text = tasks;
        }
    }
}