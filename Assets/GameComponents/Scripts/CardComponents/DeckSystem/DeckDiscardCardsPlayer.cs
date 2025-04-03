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
            if(card.TryGetComponent(out CardFlipper flipper))
            {
                flipper.ResetToBackSide();
            }
            
            if(_discardCards.Count == 0)
                _discardCards.Add(card);
            else
                _discardCards.Insert(0, card);
            
            card.transform.SetParent(transform, false);
            card.transform.DOLocalMove(Vector3.zero, _moveDuration).SetEase(Ease.OutQuad);
            
            float rotationOffset = Random.Range(-5f, 5f);
            
            card.transform.DOLocalRotate(new Vector3(0, 0, rotationOffset), _moveDuration).SetEase(Ease.OutQuad);
            
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