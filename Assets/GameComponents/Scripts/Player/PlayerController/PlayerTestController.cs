using System.Collections.Generic;
using GameComponents.Scripts.CardComponents;
using GameComponents.Scripts.CardComponents.DeckSystem;
using UnityEngine;

namespace GameComponents.Scripts.Player.PlayerController
{
    public class PlayerTestController : MonoBehaviour
    {
        [SerializeField] private DeckInputCardsPlayer _inputDeck;
        [SerializeField] private DeckHandCardsPlayer _handDeck;
        [SerializeField] private DeckDiscardCardsPlayer _discardDeck;

        private bool _gameStarted = false;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(!_gameStarted)
                {
                    StartGame();
                }
            }
            
            if(_gameStarted && HandHasCards() && AllCardsPlayed())
            {
                DiscardHand();
                DrawCardsToHand(5);
            }
        }

        private void StartGame()
        {
            _gameStarted = true;
            DrawCardsToHand(5);
        }
        
        private void DrawCardsToHand(int count)
        {
            for(int i = 0; i < count; i++)
            {
                if(!_inputDeck.HasCards)
                {
                    _inputDeck.RefillFromDiscard(_discardDeck);

                    if(!_inputDeck.HasCards)
                        break;
                }

                CardView card = _inputDeck.DrawCard();

                if(card != null)
                {
                    _handDeck.AddCard(card);
                }
            }
        }
        
        private void DiscardHand()
        {
            List<CardView> cardsToDiscard = GetHandCards();

            foreach(CardView card in cardsToDiscard)
            {
                _handDeck.RemoveCard(card);
                _discardDeck.AddCard(card);
            }
        }
        
        private bool AllCardsPlayed()
        {
            foreach(CardView card in GetHandCards())
            {
                if(card.TryGetComponent(out CardFlipper flipper))
                {
                    if(flipper != null && !flipper.IsPlayed)
                        return false;
                }
            }

            return true;
        }
        
        private bool HandHasCards()
        {
            return GetHandCards().Count > 0;
        }
        
        private List<CardView> GetHandCards()
        {
            return _handDeck.GetCards();
        }
    }
}