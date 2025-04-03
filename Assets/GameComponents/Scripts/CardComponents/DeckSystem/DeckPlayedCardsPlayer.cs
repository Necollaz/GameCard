using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GameComponents.Scripts.CardComponents.DeckSystem
{
    public class DeckPlayedCardsPlayer : MonoBehaviour
    {
        [SerializeField] private float _moveDuration = 0.5f;
    
        private readonly List<CardView> _playedCards = new List<CardView>();
        
        public void AddCard(CardView card)
        {
            if (card.TryGetComponent(out CardFlipper flipper))
            {
                flipper.ResetToBackSide();
                flipper.IsInteractive = false;
            }
            
            card.transform.SetParent(transform, false);
            card.transform.DOKill();
            card.transform.DOLocalMove(Vector3.zero, _moveDuration).SetEase(Ease.OutQuad);
            card.transform.DOLocalRotate(Vector3.zero, _moveDuration).SetEase(Ease.OutQuad);
            
            _playedCards.Add(card);
        }
        
        public List<CardView> ClearPile()
        {
            List<CardView> cards = new List<CardView>(_playedCards);
            
            _playedCards.Clear();
            
            return cards;
        }
    }
}