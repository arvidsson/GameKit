using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace GameKit.Tween
{
    public class TweenScale : MonoBehaviour
    {
        public Vector3 targetScale = Vector3.one;

        public float duration = 1f;
        public Ease ease = Ease.Linear;
        public UnityEvent onCompleteTween;

        public float undoDuration = 1f;
        public Ease undoEase = Ease.Linear;
        public UnityEvent onCompleteUndo;

        private Vector3 originalScale;

        private void Awake()
        {
            originalScale = transform.localScale;
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }

        public void Tween()
        {
            transform.DOKill();
            transform.DOScale(targetScale, duration).SetEase(ease).OnComplete(() => { onCompleteTween?.Invoke(); });
        }

        public void UndoTween()
        {
            transform.DOKill();
            transform.DOScale(originalScale, undoDuration).SetEase(undoEase).OnComplete(() => { onCompleteUndo?.Invoke(); });
        }
    }
}