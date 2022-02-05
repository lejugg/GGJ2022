using System;
using System.Collections.Generic;
using Days;
using UniRx;
using UnityEngine;

namespace Interactions
{
    public abstract class Interaction : MonoBehaviour
    {
        [Header("Interaction")] 
        [SerializeField] private string description = "Do this task!";
        [SerializeField] private AudioClip BeginSound;
        [SerializeField] private AudioClip CompleteSound;
        [SerializeField] private AudioClip FailSound;
        [SerializeField] private List<Animator> animators;
        [SerializeField] private AudioSource _audioSource;
        
        private ReactiveProperty<bool> wasCompletedToday = new ReactiveProperty<bool>();

        public string Description => description;
        public int TotalCompletions { get; private set; }
        public int DailyCompletions { get; private set; }

        public event Action<Interaction> OnBegin = delegate {  };
        public event Action<Interaction> OnComplete = delegate {  };
        public event Action<Interaction> OnFail = delegate {  };

        public bool IsCurrentTask { get; private set; }
        public ReactiveProperty<bool> WasCompletedToday => wasCompletedToday;
        public bool IsForbidden { get; private set; }
        public bool IsUnlocked { get; private set; }
        public bool IsBroken { get; private set; }
        
        private void Awake()
        {
            DayManager.Instance.CurrentDay.Subscribe(OnDayChanged);
        }

        protected void InvokeBegin()
        {
            OnBegin.Invoke(this);
            
            Game.IsInteracting = true;
            
            AnimateTrigger("Begin");
            PlaySound(BeginSound);
        }

        protected void InvokeComplete()
        {
            OnComplete.Invoke(this);
            
            TotalCompletions++;
            DailyCompletions++;
            
            AnimateTrigger("Complete");
            PlaySound(CompleteSound);
        }

        protected void InvokeFail()
        {
            OnFail.Invoke(this);
            
            AnimateTrigger("Fail");
            PlaySound(FailSound);
        }

        protected virtual void OnMouseUp()
        {
            Game.IsInteracting = false;
        }

        private void OnDayChanged(Day day)
        {
            AnimateTrigger("DayStart");
            
            DailyCompletions = 0;
            
            WasCompletedToday.Value = false;
            Debug.Log(gameObject.name + " was done today:" + WasCompletedToday);
        }
        
        public void SetIsCurrentTask(bool active)
        {
            IsCurrentTask = active;
            
            WasCompletedToday.Value = !active;
            AnimateBool("IsCurrentTask", active);
        }
        
        public void SetIsForbidden(bool active)
        {
            IsForbidden = active;
            AnimateBool("IsForbidden", active);
        }
        
        public void SetIsUnlocked(bool active)
        {
            IsUnlocked = active;
            AnimateBool("IsUnlocked", active);
        }
        
        public void SetIsBroken(bool active)
        {
            IsBroken = active;
            AnimateBool("IsBroken", active);
        }

        private void AnimateBool(string boolName, bool active)
        {
            foreach (var animator in animators)
            {
                if (animator != null) animator.SetBool(boolName, active);
            }
        }

        private void AnimateTrigger( string trigger )
        {
            foreach (var animator in animators)
            {
                if (animator != null) animator.SetTrigger(trigger);
            }
        }

        private void PlaySound(AudioClip clip)
        {
            if (_audioSource == null || clip == null)
                return;

            _audioSource.clip = clip;
            _audioSource.Play();
        }

    }
}