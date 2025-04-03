using System.Collections.Generic;
using UnityEngine;

namespace GameComponents.Scripts.CardComponents.DeckSystem
{
    [CreateAssetMenu(fileName = "NewDeck", menuName = "Card Game/Deck")]
    public class DeckData : ScriptableObject
    {
        [SerializeField] private List<DeckEntry> _deckEntries;
        
        public List<DeckEntry> DeckEntries => _deckEntries;
    }
}