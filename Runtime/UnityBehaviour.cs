﻿using UnityEngine;
using GameKit.Events;

namespace GameKit
{
    /// <summary>
    /// Base class that provides a lot of utility and helper functionality.
    /// Note: this might require all of UnityLib to be included in your project
    /// </summary>
    public abstract class UnityBehaviour : MonoBehaviour
    {
        private Animation _animation;
        private Animator _animator;
        private AudioListener _audioListener;
        private AudioSource _audioSource;
        private BoxCollider2D _boxCollider2D;
        private Camera _camera;
        private CanvasGroup _canvasGroup;
        private Collider _collider;
        private Collider2D _collider2D;
        private RectTransform _rectTransform;
        private Rigidbody _rigidbody;
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;

        private EventBehaviour _eventBehaviour;

        protected new Animation animation { get { return _animation ? _animation : (_animation = GetComponent<Animation>()); } }
        protected Animator animator { get { return _animator ? _animator : (_animator = GetComponent<Animator>()); } }
        protected AudioListener audioListener { get { return _audioListener ? _audioListener : (_audioListener = GetComponent<AudioListener>()); } }
        protected AudioSource audioSource { get { return _audioSource ? _audioSource : (_audioSource = GetComponent<AudioSource>()); } }
        protected BoxCollider2D boxCollider2D { get { return _boxCollider2D ? _boxCollider2D : (_boxCollider2D = GetComponent<BoxCollider2D>()); } }
        protected new Camera camera { get { return _camera ? _camera : (_camera = GetComponent<Camera>()); } }
        protected CanvasGroup canvasGroup { get { return _canvasGroup ? _canvasGroup : (_canvasGroup = GetComponent<CanvasGroup>()); } }
        protected new Collider collider { get { return _collider ? _collider : (_collider = GetComponent<Collider>()); } }
        protected new Collider2D collider2D { get { return _collider2D ? _collider2D : (_collider2D = GetComponent<Collider2D>()); } }
        protected RectTransform rectTransform { get { return _rectTransform ? _rectTransform : (_rectTransform = GetComponent<RectTransform>()); } }
        protected new Rigidbody rigidbody { get { return _rigidbody ? _rigidbody : (_rigidbody = GetComponent<Rigidbody>()); } }
        protected new Rigidbody2D rigidbody2D { get { return _rigidbody2D ? _rigidbody2D : (_rigidbody2D = GetComponent<Rigidbody2D>()); } }
        protected SpriteRenderer spriteRenderer { get { return _spriteRenderer ? _spriteRenderer : (_spriteRenderer = GetComponent<SpriteRenderer>()); } }

        /// <summary>
        /// EventManager that is used for communication between MonoBehaviours on the same GameObject.
        /// </summary>
        public EventManager EventManager
        {
            get
            {
                if (_eventBehaviour == null)
                {
                    _eventBehaviour = GetComponent<EventBehaviour>();

                    if (_eventBehaviour == null)
                    {
                        _eventBehaviour = gameObject.AddComponent<EventBehaviour>();
                    }
                }

                return _eventBehaviour.EventManager;
            }
        }

        //protected T GetComponentSafe<T>()
        //{
        //    return gameObject.GetComponentSafe<T>();
        //}
    }
}