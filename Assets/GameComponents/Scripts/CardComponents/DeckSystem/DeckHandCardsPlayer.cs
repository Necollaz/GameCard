using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GameComponents.Scripts.CardComponents.CardDataComponents;

namespace GameComponents.Scripts.CardComponents.DeckSystem
{
    public class DeckHandCardsPlayer : MonoBehaviour
    {
        [SerializeField] private float _moveDuration = 0.5f;
        [SerializeField] private float _cardSpacing = 300f;
        [SerializeField] private float _fanAngle = 45f;
        [SerializeField] private Vector3 _centerPosition = Vector3.zero;
        
        private readonly List<CardView> _handCards = new List<CardView>();
        
        public void AddCard(CardView card)
        {
            if (card.TryGetComponent(out CardHoverEffect hoverEffect))
            {
                hoverEffect.ForceExitHover();
            }
            
            _handCards.Add(card);
            card.transform.SetParent(transform, false);
            
            if(card.TryGetComponent(out CardFlipper flipper))
            {
                flipper.ResetToFaceUp();
            }
        
            ArrangeCards();
        }
        
        public void RemoveCard(CardView card)
        {
            if (card.TryGetComponent(out CardHoverEffect hoverEffect))
            {
                hoverEffect.ForceExitHover();
            }
            
            _handCards.Remove(card);
            ArrangeCards();
        }
        
        public List<CardView> GetCards()
        {
            return new List<CardView>(_handCards);
        }
        
        private void ArrangeCards()
        {
            int count = _handCards.Count;
            
            if (count == 0)
                return;
            
            if (count == 1)
            {
                _handCards[0].transform.DOLocalMove(_centerPosition, _moveDuration).SetEase(Ease.OutQuad);
                _handCards[0].transform.DOLocalRotate(Vector3.zero, _moveDuration).SetEase(Ease.OutQuad);
                
                return;
            }
            
            float startAngle = -_fanAngle / 2;
            float angleStep = _fanAngle / (count - 1);
        
            for (int i = 0; i < count; i++)
            {
                float angle = startAngle + angleStep * i;
                float rad = Mathf.Deg2Rad * angle;
                Vector3 pos = _centerPosition + new Vector3(Mathf.Sin(rad) * _cardSpacing, 0, Mathf.Cos(rad) * _cardSpacing);
            
                _handCards[i].transform.DOLocalMove(pos, _moveDuration).SetEase(Ease.OutQuad);
                
                Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
                
                _handCards[i].transform.DOLocalRotateQuaternion(targetRotation, _moveDuration).SetEase(Ease.OutQuad);
            }
        }
    }
}