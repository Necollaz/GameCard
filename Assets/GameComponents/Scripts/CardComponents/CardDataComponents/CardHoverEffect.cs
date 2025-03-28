using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace GameComponents.Scripts.CardComponents.CardDataComponents
{
    public class CardHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CardFlipper _cardFlipper;
        [SerializeField] private float _hoverOffset = 50f;
        [SerializeField] private float _hoverDuration = 0.25f;

        private Vector3 _originalLocalPosition;
        private Vector3 _originalLocalRotation;
        
        private bool _stateRecorded = false;
        private bool _isHovered = false;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_cardFlipper != null && _cardFlipper.IsPlayed)
                return;
            
            transform.SetAsLastSibling();
            
            if (!_stateRecorded)
            {
                _originalLocalPosition = transform.localPosition;
                _originalLocalRotation = transform.localEulerAngles;
                _stateRecorded = true;
            }
            
            transform.DOKill();
            
            _isHovered = true;
            
            transform.DOLocalMove(_originalLocalPosition + Vector3.up * _hoverOffset, _hoverDuration).SetEase(Ease.OutQuad);
            transform.DOLocalRotate(Vector3.zero, _hoverDuration).SetEase(Ease.OutQuad);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            ForceExitHover();
        }

        public void ForceExitHover()
        {
            if (!_stateRecorded) 
                return;
        
            _isHovered = false;
            
            transform.DOKill();
            transform.DOLocalMove(_originalLocalPosition, _hoverDuration).SetEase(Ease.OutQuad);
            transform.DOLocalRotate(_originalLocalRotation, _hoverDuration).SetEase(Ease.OutQuad).OnComplete(() => { _stateRecorded = false; });
        }
    }
}