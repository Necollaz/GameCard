using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GameComponents.Scripts.CardComponents.DeckSystem
{
    public class EmptyHandTransition : MonoBehaviour
    {
        [SerializeField] private DeckHandCardsPlayer _handDeck;
        [SerializeField] private DeckInputCardsPlayer _inputDeck;
        [SerializeField] private DeckDiscardCardsPlayer _discardDeck;
        [SerializeField] private DeckPlayedCardsPlayer _playedDeck;
        [SerializeField] private float _transitionDuration = 0.5f;
        [SerializeField] private int _desiredHandCount = 5;

        public void CheckAndHandleEmptyHand()
        {
            List<CardView> handCards = _handDeck.GetCards();
            
            if (handCards.Count == 0)
            {
                DOVirtual.DelayedCall(_transitionDuration, () =>
                {
                    List<CardView> playedCards = _playedDeck.ClearPile();
                
                    foreach (CardView card in playedCards)
                    {
                        _discardDeck.AddCard(card);
                    }
            
                    DOVirtual.DelayedCall(_transitionDuration, DealNewCards);
                });
            }
        }
        
        private void DealNewCards()
        {
            int missingCards = _desiredHandCount - _handDeck.GetCards().Count;
            
            if (_inputDeck.Count < missingCards && _discardDeck.HasCards)
                _inputDeck.RefillFromDiscard(_discardDeck);
            
            while (_handDeck.GetCards().Count < _desiredHandCount && _inputDeck.HasCards)
            {
                CardView card = _inputDeck.DrawCard();
                
                _handDeck.AddCard(card);
            }
        }
    }
}