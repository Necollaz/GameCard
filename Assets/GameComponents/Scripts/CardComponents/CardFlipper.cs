using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace GameComponents.Scripts.CardComponents
{
    public class CardFlipper : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private CardView _cardView;
        
        [Header("Side Groups (CanvasGroup)")]
        [Tooltip("CanvasGroup лицевой стороны карты")]
        [SerializeField] private CanvasGroup _faceSideGroup;
        [Tooltip("CanvasGroup рубашки карты")]
        [SerializeField] private CanvasGroup _backSideGroup;
        
        private bool _isAnimating = false;
        private bool _isPlayed = false;
        
        public bool IsPlayed => _isPlayed;
        
        private void Start()
        {
            _cardView.SetCard(_cardView.CardData);
            
            if (_faceSideGroup != null)
                _faceSideGroup.alpha = 1;
            
            if(_backSideGroup != null)
                _backSideGroup.alpha = 0;
        }
        
        public void ResetToFaceUp()
        {
            _isPlayed = false;
            _isAnimating = false;
            transform.localEulerAngles = Vector3.zero;
            
            if (_faceSideGroup != null)
                _faceSideGroup.alpha = 1;
            
            if(_backSideGroup != null)
                _backSideGroup.alpha = 0;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isAnimating || _isPlayed || eventData.button != PointerEventData.InputButton.Left)
                return;
        
            FlipCard();
        }
        
        private void FlipCard()
        {
            _isAnimating = true;
            float duration = 0.5f;
            Sequence seq = DOTween.Sequence();
            
            seq.Append(transform.DOLocalRotate(new Vector3(0, 90, 0), duration / 2).SetEase(Ease.InQuad));
            
            seq.AppendCallback(() =>
            {
                if (_faceSideGroup != null && _backSideGroup != null)
                {
                    _faceSideGroup.alpha = 0;
                    _backSideGroup.alpha = 1;
                }
            });
            
            seq.Append(transform.DOLocalRotate(new Vector3(0, 180, 0), duration / 2).SetEase(Ease.OutQuad));
    
            seq.OnComplete(() =>
            {
                _isPlayed = true;
                _isAnimating = false;
                transform.localEulerAngles = new Vector3(0, 180, 0);
                
                if (_backSideGroup != null)
                {
                    _backSideGroup.DOFade(0.5f, 0.2f);
                }
            });
        }
    }
}