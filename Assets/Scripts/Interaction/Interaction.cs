using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Interaction
{
    public abstract class Interaction : MonoBehaviour
    {
        [Header("Interaction")] 
        [SerializeField] private AudioClip BeginSound;
        [SerializeField] private AudioClip CompleteSound;
        [SerializeField] private AudioClip FailSound;
        
        private Animator _animator;
        private AudioSource _audioSource;

        public event Action<Interaction> OnBegin = delegate {  };
        public event Action<Interaction> OnComplete = delegate {  };
        public event Action<Interaction> OnFail = delegate {  };

        private void Awake()
        {
            _animator = GetComponent<Animator>();
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
        }

        protected void InvokeFail()
        {
            // Debug.Log("Failed interaction");
            OnFail.Invoke(this);
            
            AnimateTrigger("Fail");
            PlaySound(FailSound);
        }

        protected virtual void OnMouseUp()
        {
            // Debug.Log("Interaction over");
            Game.IsInteracting = false;
        }
        
        
        private void AnimateTrigger( string trigger )
        {
            if (_animator != null) _animator.SetTrigger(trigger);
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