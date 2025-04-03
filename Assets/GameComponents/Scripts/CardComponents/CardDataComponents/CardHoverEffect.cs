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
        
        private int _originalSiblingIndex;
        private bool _stateRecorded = false;
        
        public void SetOrigin(Vector3 pos, Vector3 rot, int siblingIndex)
        {
            _originalLocalPosition = pos;
            _originalLocalRotation = rot;
            _originalSiblingIndex = siblingIndex;
            _stateRecorded = false;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_cardFlipper != null && (!_cardFlipper.IsInteractive || _cardFlipper.IsPlayed))
                return;
            
            if (!_stateRecorded)
            {
                _originalLocalPosition = transform.localPosition;
                _originalLocalRotation = transform.localEulerAngles;
                _originalSiblingIndex = transform.GetSiblingIndex();
                _stateRecorded = true;
            }
            
            transform.SetAsLastSibling();
            
            transform.DOKill();
            transform.DOLocalMove(_originalLocalPosition + Vector3.up * _hoverOffset, _hoverDuration).SetEase(Ease.OutQuad);
            transform.DOLocalRotate(_originalLocalRotation, _hoverDuration).SetEase(Ease.OutQuad);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            ForceExitHover();
        }

        public void ForceExitHover()
        {
            if (!_stateRecorded) 
                return;
            
            transform.DOKill();
            transform.DOLocalMove(_originalLocalPosition, _hoverDuration).SetEase(Ease.OutQuad);
            transform.DOLocalRotate(_originalLocalRotation, _hoverDuration).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                transform.SetSiblingIndex(_originalSiblingIndex);
                _stateRecorded = false;
            });
        }
    }
}