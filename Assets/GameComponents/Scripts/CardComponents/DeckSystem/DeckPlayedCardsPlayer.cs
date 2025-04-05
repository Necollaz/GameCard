using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GameComponents.Scripts.CardComponents.CardAnimations;
using GameComponents.Scripts.CardComponents.CardInterfaces;

namespace GameComponents.Scripts.CardComponents.DeckSystem
{
    public class DeckPlayedCardsPlayer : MonoBehaviour
    {
        [SerializeField] private float _moveDuration = 0.5f;
    
        private readonly List<CardView> _playedCards = new List<CardView>();
        private readonly ICardAnimationHelper _animator = new CardAnimationHelper();
        
        public void AddCard(CardView card)
        {
            if (card.TryGetComponent(out CardFlipper flipper))
            {
                flipper.ResetToBackSide();
                flipper.IsInteractive = false;
            }
            
            card.transform.SetParent(transform, false);
            card.transform.DOKill();
            _animator.MoveCard(card.transform, Vector3.zero, _moveDuration);
            _animator.RotateCard(card.transform, Vector3.zero, _moveDuration);
            
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