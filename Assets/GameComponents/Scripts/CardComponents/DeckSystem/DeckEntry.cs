using System;
using UnityEngine;

namespace GameComponents.Scripts.CardComponents.DeckSystem
{
    [Serializable]
    public class DeckEntry
    {
        [SerializeField] private CardView _cardPrefab;
        [SerializeField] private int _count;
        
        public CardView CardPrefab => _cardPrefab;
        public int Count => _count;
    }
}