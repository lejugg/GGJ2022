using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Interaction
{
    public abstract class Interaction : MonoBehaviour
    {
        [Header("Interaction")] 
        [SerializeField] private string description = "Do!";
        [SerializeField] private AudioClip BeginSound;
        [SerializeField] private AudioClip CompleteSound;
        [SerializeField] private AudioClip FailSound;
        [SerializeField] private List<Animator> animators;
        
        private AudioSource _audioSource;
        
        public bool IsCurrentInteraction { get; protected set; }
        public string Description => description;

        public event Action<Interaction> OnBegin = delegate {  };
        public event Action<Interaction> OnComplete = delegate {  };
        public event Action<Interaction> OnFail = delegate {  };

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>() != null ? GetComponent<AudioSource>() : gameObject.AddComponent<AudioSource>();
        }

        protected void InvokeBegin()
        {
            // Debug.Log("Begin interaction");
            OnBegin.Invoke(this);
            
            Game.IsInteracting = true;
            
            AnimateTrigger("Begin");
            PlaySound(BeginSound);
        }

        protected void InvokeComplete()
        {
            // Debug.Log("Complete interaction");
            OnComplete.Invoke(this);
            
            AnimateTrigger("Complete");
            PlaySound(CompleteSound);
            SetCurrentInteraction(false);
        }

        protected void InvokeFail()
        {
            // Debug.Log("Failed interaction");
            OnFail.Invoke(this);
            
            AnimateTrigger("Fail");
            PlaySound(FailSound);
            SetCurrentInteraction(false);
        }

        protected virtual void OnMouseUp()
        {
            // Debug.Log("Interaction over");
            Game.IsInteracting = false;
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

        public void SetCurrent()
        {
            SetCurrentInteraction(true);
        }

        private void SetCurrentInteraction(bool isCurrent)
        {
            IsCurrentInteraction = isCurrent;
            
            foreach (var animator in animators)
            {
                if (animator != null) animator.SetBool("isCurrentTask", isCurrent);
            }

        }
    }
}