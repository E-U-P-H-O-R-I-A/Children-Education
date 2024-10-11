using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SpaceGame.Hero
{
    public class HeroAnimation : MonoBehaviour
    {
        [Space, Header("Idle")] 
        
        [SerializeField] private float strengthIdleX = 1f;
        [SerializeField] private float strengthIdleY = 1f;
        [SerializeField] private float strengthIdleZ = 1f;
        [SerializeField] private float durationIdle;
        
        [Space, Header("Move")] 
        
        [SerializeField] private float strengthMoveX = 1f;
        [SerializeField] private float strengthMoveZ = 1f;
        [SerializeField] private float durationMove;

        private Sequence _mySequence;
        private Vector3 _startPosition;
        private AnimationType _currentAnimation;

        public void Awake() => 
            _startPosition = transform.position;

        public void Start() => 
            PlayIdleAnimation(NextAnimation);

        [Button]
        public void PlayAnimation(AnimationType type)
        {
            _currentAnimation = type;
            PlayReset(NextAnimation);
        }

        private void NextAnimation()
        {
            switch (_currentAnimation)
            {
                case AnimationType.Idle:
                    PlayIdleAnimation(NextAnimation);
                    break;
                case AnimationType.Move:
                    PlayMoveAnimation(NextAnimation);
                    break;
            }
        }

        private void PlayReset(Action onComplete = null)
        {
            _mySequence?.Kill();
            _mySequence = DOTween.Sequence();
            
            _mySequence.SetEase(Ease.Linear);

            _mySequence.Append(transform.DOMove(_startPosition, 0.8f));
            _mySequence.Join(transform.DORotate(Vector3.zero, 0.8f));
            _mySequence.OnComplete(() => onComplete?.Invoke());
        }

        private void PlayIdleAnimation(Action onComplete = null)
        {
            _mySequence?.Kill();
            _mySequence = DOTween.Sequence();

            _mySequence.SetEase(Ease.Linear);
            
            _mySequence.Insert(0, transform.DOPunchRotation(Vector3.forward * strengthIdleZ, durationIdle / 2, 0));
            _mySequence.Insert(durationIdle / 2f, transform.DOPunchRotation(Vector3.forward * -strengthIdleZ, durationIdle / 2, 0));

            _mySequence.Insert(0, transform.DOMoveX(_startPosition.x + strengthIdleX, durationIdle / 4f).SetLoops(2, LoopType.Yoyo));
            _mySequence.Insert(durationIdle / 2f, transform.DOMoveX(_startPosition.x - strengthIdleX, durationIdle / 4f).SetLoops(2, LoopType.Yoyo));

            _mySequence.Insert(0, transform.DOMoveY(_startPosition.y + strengthIdleY, durationIdle / 8f).SetLoops(2, LoopType.Yoyo));
            _mySequence.Insert(durationIdle / 4f, transform.DOMoveY(_startPosition.y - strengthIdleY, durationIdle / 8f).SetLoops(2, LoopType.Yoyo));
            _mySequence.Insert(durationIdle / 2f, transform.DOMoveY(_startPosition.y + strengthIdleY, durationIdle / 8f).SetLoops(2, LoopType.Yoyo));
            _mySequence.Insert(durationIdle * 3 / 4f, transform.DOMoveY(_startPosition.y - strengthIdleY, durationIdle / 8f).SetLoops(2, LoopType.Yoyo));
            _mySequence.OnComplete(() => onComplete?.Invoke());
        }

        private void PlayMoveAnimation(Action onComplete = null)
        {
            _mySequence?.Kill();
            _mySequence = DOTween.Sequence();

            _mySequence.SetEase(Ease.Linear);
            
            _mySequence.Append(transform.DOMoveX(_startPosition.x + strengthMoveX, durationMove / 4f).SetLoops(2, LoopType.Yoyo));
            _mySequence.Join(transform.DORotate(Vector3.forward * strengthMoveZ, durationMove / 4f).SetLoops(2, LoopType.Yoyo));
            _mySequence.Append(transform.DOMoveX(_startPosition.x - strengthMoveX, durationMove / 4f).SetLoops(2, LoopType.Yoyo));
            _mySequence.Join(transform.DORotate(Vector3.forward * -strengthMoveZ, durationMove / 4f).SetLoops(2, LoopType.Yoyo));
            _mySequence.OnComplete(() => onComplete?.Invoke());
        }
    }
}