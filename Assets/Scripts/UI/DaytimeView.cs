using System.Collections.Generic;
using Days;
using DG.Tweening;
using Interactions;
using TMPro;
using UniRx;
using UnityEngine;

namespace UI
{
    public class DaytimeView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI field;
        [SerializeField] private List<Animator> animators;
        private RectTransform _rect;
        private bool morningShown;
        private bool middayShown;
        private bool eveningShown;

        private void Awake()
        {
            DayManager.Instance.CurrentDay.Subscribe(OnDayChanged);
            DayManager.Instance.CurrentDay.Value.CurrentInteraction.Subscribe(HandleInteraction);
            _rect = GetComponent<RectTransform>();
            _rect.localScale = Vector3.zero;
        }
        
        private void HandleInteraction(Interaction interaction)
        {
            var daytime = "";
            var dayProgress = DayManager.Instance.CurrentDay.Value.DayProgress;
            
            if (dayProgress < 0.3f)
            {
                daytime = "Morning";
                if(!morningShown) Animate();
                morningShown = true;
                AnimateTrigger("morningShown");
            }
            else if (dayProgress < 0.7f)
            {
                daytime = "Midday";
                if(!middayShown) Animate();
                middayShown = true;
                AnimateTrigger("middayShown");
            }
            else
            {
                daytime = "Evening";
                if(!eveningShown) Animate();
                eveningShown = true;
                AnimateTrigger("eveningShown");
            }
        
            field.text = daytime;
        }
        
        private void OnDayChanged(Day day)
        {
            morningShown = false;
            middayShown = false;
            eveningShown = false;
            AnimateTrigger("dayEnd");
        
            field.text = "Day " + (day.DayIndex + 1);
            Animate();
        }
        
        private void AnimateTrigger( string trigger )
        {
            foreach (var animator in animators)
            {
                if (animator != null) animator.SetTrigger(trigger);
            }
        }
        
        private void Animate()
        {
            _rect.DOKill();
        
            _rect.DOScale(1f, 0.2f).SetEase(Ease.InCubic);
            _rect.DOScale(0f, 0.2f).SetEase(Ease.InCubic).SetDelay(3f);
        }
    }
}
