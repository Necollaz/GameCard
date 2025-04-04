using System.Collections.Generic;
using UnityEngine;
using GameComponents.Scripts.CardComponents.CardAnimations;
using GameComponents.Scripts.CardComponents.CardInterfaces;

namespace GameComponents.Scripts.CardComponents.DeckSystem
{
    public class DeckInputCardsPlayer : MonoBehaviour, IShuffle
    {
        [SerializeField] private DeckData _deckData;
        [SerializeField] private float _moveDuration = 0.5f;
        
        private readonly List<CardView> _drawCards = new List<CardView>();
        private readonly ICardAnimationHelper _animator = new CardAnimationHelper();
        
        public bool HasCards => _drawCards.Count > 0;
        public int Count => _drawCards.Count;
        
        private void Start()
        {
            InitializeDeckFromData();
            Shuffle();
        }
        
        public CardView DrawCard()
        {
            if (_drawCards.Count == 0)
                return null;
            
            CardView card = _drawCards[_drawCards.Count - 1];
            _drawCards.RemoveAt(_drawCards.Count - 1);
            
            return card;
        }
        
        public void RefillFromDiscard(DeckDiscardCardsPlayer discardDeck)
        {
            if (discardDeck != null && discardDeck.HasCards)
            {
                List<CardView> discardCards = discardDeck.ClearPile();
                
                foreach (CardView card in discardCards)
                {
                    AddCard(card);
                }
                
                Shuffle();
            }
        }
        
        public void Shuffle()
        {
            for (int i = 0; i < _drawCards.Count; i++)
            {
                CardView temp = _drawCards[i];
                int randomIndex = Random.Range(i, _drawCards.Count);
                
                _drawCards[i] = _drawCards[randomIndex];
                _drawCards[randomIndex] = temp;
            }
        }
        
        private void InitializeDeckFromData()
        {
            if (_deckData == null)
                return;
        
            foreach (DeckEntry entry in _deckData.DeckEntries)
            {
                for (int i = 0; i < entry.Count; i++)
                {
                    float rotationOffset = Random.Range(-5f, 5f);
                    CardView cardInstance = Instantiate(entry.CardPrefab, transform.position, Quaternion.identity, transform);
                    
                    if(cardInstance.TryGetComponent(out CardFlipper flipper))
                        flipper.ResetToBackSide();
                    
                    _drawCards.Add(cardInstance);
                    cardInstance.transform.SetParent(transform, false);
                    _animator.MoveCard(cardInstance.transform, Vector3.zero, _moveDuration);
                    _animator.RotateCard(cardInstance.transform, new Vector3(0, 0, rotationOffset), _moveDuration);
                }
            }
        }
        
        private void AddCard(CardView card, bool toBottom = false)
        {
            float rotationOffset = Random.Range(-5f, 5f);
            
            if (card.TryGetComponent(out CardFlipper flipper))
                flipper.ResetToBackSide();
            
            if (toBottom)
                _drawCards.Insert(0, card);
            else
                _drawCards.Add(card);
            
            card.transform.SetParent(transform, false);
            _animator.MoveCard(card.transform, Vector3.zero, _moveDuration);
            _animator.RotateCard(card.transform, new Vector3(0, 0, rotationOffset), _moveDuration);
        }
    }
}