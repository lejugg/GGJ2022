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

            if (dayProgress < 0.333f)
            {
                daytime = "Morning";
            }
            else if (dayProgress < 0.666f)
            {
                daytime = "Midday";
            }
            else
            {
                daytime = "Evening";
            }
            
            field.text = daytime;
        }

        private void HandleOnStartNewDay(int dayIndex)
        {
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