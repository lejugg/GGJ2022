using Day;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DaytimeView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI field;
        private RectTransform _rect;
        private bool morningShown;
        private bool middayShown;
        private bool eveningShown;

        private void Awake()
        {
            DayManager.OnStartNewDay += HandleOnStartNewDay;
            DayManager.OnCurrentDayInteraction += HandleInteraction;
            _rect = GetComponent<RectTransform>();
            _rect.localScale = Vector3.zero;
        }

        private void HandleInteraction(float dayProgress)
        {
            var daytime = "";

            if (dayProgress < 0.3f)
            {
                daytime = "Morning";
                if(!morningShown) Animate();
                morningShown = true;
            }
            else if (dayProgress < 0.7f)
            {
                daytime = "Midday";
                if(!middayShown) Animate();
                middayShown = true;
            }
            else
            {
                daytime = "Evening";
                if(!eveningShown) Animate();
                eveningShown = true;
            }
            
            field.text = daytime;
        }

        private void HandleOnStartNewDay(int dayIndex)
        {
            morningShown = false;
            middayShown = false;
            eveningShown = false;
            
            field.text = "Day " + (dayIndex + 1);
            Animate();
        }

        private void Animate()
        {
            _rect.DOScale(1f, 0.2f).SetEase(Ease.InCubic);
            _rect.DOScale(0f, 0.2f).SetEase(Ease.InCubic).SetDelay(5f);
        }
    }
}