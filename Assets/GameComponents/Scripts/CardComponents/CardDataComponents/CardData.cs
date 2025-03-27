using UnityEngine;

namespace GameComponents.Scripts.CardComponents.CardDataComponents
{
    [CreateAssetMenu(fileName = "NewCard", menuName = "Card Game/Card")]
    public class CardData : ScriptableObject
    {
        [Tooltip("Префаб карты")]
        [SerializeField] private CardView _cardViewPrefab;
        [Tooltip("Черта характера карты")]
        [SerializeField] private TraitData _trait;
        [Tooltip("Символ колоды, к которой принадлежит карта")]
        [SerializeField] private DeckSymbolData _deckSymbol;
        [Tooltip("Фоновое изображение на карте")]
        [SerializeField] private Sprite _backgroundImage;
        [Tooltip("Наименование карты")]
        [SerializeField] private string _cardName;
        [Tooltip("Описание эффекта карты")]
        [SerializeField] private string _cardEffect;
        [Tooltip("Стоимость карты, которую должен заплатить игрок на рынке, чтобы приобрести ее себе в колоду")]
        [SerializeField] private int _cost;
        [Tooltip("Порядковый номер карты в колоде")] 
        [SerializeField] private int _currentIndexInDeck;
        [Tooltip("Общее количество карт в колоде для этого символа")]
        [SerializeField] private int _totalCardsInDeck;
        [Tooltip("Ценность карты в колоде игрока")]
        [SerializeField] private int _valueInPlayerDeck;
        [Tooltip("Ценность карты во внутреннем круге игрока")]
        [SerializeField] private int _valueInInnerCircle;
        [Tooltip("Редкость карты в колоде")]
        [SerializeField, Range(1, 5)] private int _rarityLevel;
        
        public TraitData Trait => _trait;
        public DeckSymbolData DeckSymbol => _deckSymbol;
        public Sprite BackgroundImage => _backgroundImage;
        public string CardName => _cardName;
        public string CardEffect => _cardEffect;
        public int Cost => _cost;
        public int CurrentIndexInDeck => _currentIndexInDeck;
        public int TotalCardsInDeck => _totalCardsInDeck;
        public int ValueInPlayerDeck => _valueInPlayerDeck;
        public int ValueInInnerCircle => _valueInInnerCircle;
        public int RarityLevel => _rarityLevel;
        
        public string DeckSymbolLabel => _deckSymbol.RaceType == RaceType.Base ? _deckSymbol.DeckSymbolName : string.Empty;
    }
}