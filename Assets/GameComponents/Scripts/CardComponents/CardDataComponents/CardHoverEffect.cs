using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using GameComponents.Scripts.CardComponents.CardAnimations;
using GameComponents.Scripts.CardComponents.CardInterfaces;

namespace GameComponents.Scripts.CardComponents.CardDataComponents
{
    public class CardHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CardFlipper _cardFlipper;
        [SerializeField] private float _hoverOffset = 50f;
        [SerializeField] private float _hoverDuration = 0.4f;

        private readonly ICardAnimationHelper _animator = new CardAnimationHelper();
        
        private Vector3 _originalLocalPosition;
        private Vector3 _originalLocalRotation;
        
        private int _originalSiblingIndex;
        private bool _stateRecorded = false;
        
        public bool IsHovering => _stateRecorded;
        
        public void SetOrigin(Vector3 pos, Vector3 rot, int siblingIndex)
        {
            _originalLocalPosition = pos;
            _originalLocalRotation = rot;
            _originalSiblingIndex = siblingIndex;
            _stateRecorded = false;
        }
        
        public void ForceExitHover()
        {
            if (!_stateRecorded) 
                return;
            
            _animator.MoveCard(transform, _originalLocalPosition, _hoverDuration).OnComplete(() =>
                {
                    transform.SetSiblingIndex(_originalSiblingIndex);
                    _stateRecorded = false;
                });
            _animator.RotateCard(transform, _originalLocalRotation, _hoverDuration);
        }
        
        public void ForceExitHoverImmediate()
        {
            if (!_stateRecorded)
                return;
            
            _animator.ResetCard(transform, _originalLocalPosition, _originalLocalRotation);
            transform.SetSiblingIndex(_originalSiblingIndex);
            _stateRecorded = false;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_cardFlipper != null && (!_cardFlipper.IsInteractive || _cardFlipper.IsPlayed))
                return;
            
            if (transform.parent != null)
            {
                CardHoverEffect[] allHoverEffects = transform.parent.GetComponentsInChildren<CardHoverEffect>();
                
                foreach (CardHoverEffect hover in allHoverEffects)
                {
                    if (hover != this)
                        hover.ForceExitHoverImmediate();
                }
            }
            
            if (!_stateRecorded)
            {
                _originalLocalPosition = transform.localPosition;
                _originalLocalRotation = transform.localEulerAngles;
                _originalSiblingIndex = transform.GetSiblingIndex();
                _stateRecorded = true;
            }
            
            transform.SetAsLastSibling();
            _animator.MoveCard(transform, _originalLocalPosition + Vector3.up * _hoverOffset, _hoverDuration);
            _animator.RotateCard(transform, _originalLocalRotation, _hoverDuration);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            ForceExitHover();
        }
    }
}