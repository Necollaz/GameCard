using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GameComponents.Scripts.CardComponents.DeckSystem
{
    public class DeckDiscardCardsPlayer : MonoBehaviour
    {
        [SerializeField] private float _moveDuration = 0.5f;
        
        private readonly List<CardView> _discardCards = new List<CardView>();
        public bool HasCards => _discardCards.Count > 0;
        
        public void AddCard(CardView card)
        {
            _discardCards.Add(card);
            card.transform.SetParent(transform, false);
            card.transform.DOMove(transform.position, _moveDuration).SetEase(Ease.OutQuad);
        }
        
        public List<CardView> ClearPile()
        {
            List<CardView> cards = new List<CardView>(_discardCards);
            _discardCards.Clear();
            return cards;
        }
    }
}