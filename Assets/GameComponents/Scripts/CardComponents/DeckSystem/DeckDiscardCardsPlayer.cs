using System.Collections.Generic;
using UnityEngine;
using GameComponents.Scripts.CardComponents.CardAnimations;
using GameComponents.Scripts.CardComponents.CardInterfaces;

namespace GameComponents.Scripts.CardComponents.DeckSystem
{
    public class DeckDiscardCardsPlayer : MonoBehaviour
    {
        [SerializeField] private float _moveDuration = 0.5f;
        
        private readonly List<CardView> _discardCards = new List<CardView>();
        private readonly ICardAnimationHelper _animator = new CardAnimationHelper();
        
        public bool HasCards => _discardCards.Count > 0;
        
        public void AddCard(CardView card)
        {
            float rotationOffset = Random.Range(-5f, 5f);
            
            if(card.TryGetComponent(out CardFlipper flipper))
                flipper.ResetToBackSide();

            if(_discardCards.Count == 0)
                _discardCards.Add(card);
            else
                _discardCards.Insert(0, card);
            
            card.transform.SetParent(transform, false);
            _animator.MoveCard(card.transform, Vector3.zero, _moveDuration);
            _animator.RotateCard(card.transform, new Vector3(0, 0, rotationOffset), _moveDuration);
            
            for (int i = 0; i < _discardCards.Count; i++)
            {
                _discardCards[i].transform.SetSiblingIndex(i);
            }
        }
        
        public List<CardView> ClearPile()
        {
            List<CardView> cards = new List<CardView>(_discardCards);
            
            _discardCards.Clear();
            
            return cards;
        }
    }
}