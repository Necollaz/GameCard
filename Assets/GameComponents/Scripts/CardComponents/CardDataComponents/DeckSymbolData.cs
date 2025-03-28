using System;
using UnityEngine;

namespace GameComponents.Scripts.CardComponents.CardDataComponents
{
    [Serializable]
    public class DeckSymbolData
    {
        [Tooltip("Название символа колоды")]
        [SerializeField] private RaceType _raceType;
        [Tooltip("Название символа для базовой карты (отображается текстом)")]
        [SerializeField] private string _deckSymbolName;
        [Tooltip("Иконка символа колоды")]
        [SerializeField] private Sprite _symbolIcon;
        
        public RaceType RaceType => _raceType;
        public Sprite SymbolIcon => _symbolIcon;
        public string DeckSymbolName => _deckSymbolName;
    }
}