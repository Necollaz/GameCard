using UnityEngine;
using UnityEngine.UI;
using GameComponents.Scripts.CardComponents.CardDataComponents;
using TMPro;

namespace GameComponents.Scripts.CardComponents
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private CardData _cardData;

        [Header("UI: Text")]
        [Tooltip("Наименование карты")]
        [SerializeField] private TextMeshProUGUI _cardNameText;
        [Tooltip("Стоимость карты, которую должен заплатить игрок на рынке, чтобы приобрести ее себе в колоду")]
        [SerializeField] private TextMeshProUGUI _costText;
        [Tooltip("Текст вместо иконки символа колоды (если карта стартовая)")]
        [SerializeField] private TextMeshProUGUI _deckSymbolText;
        [Tooltip("Текущий номер карты в колоде / общее количество")]
        [SerializeField] private TextMeshProUGUI _deckCountText;
        [Tooltip("Текст с описанием эффекта карты")]
        [SerializeField] private TextMeshProUGUI _cardEffectText;
        [Tooltip("Ценность карты в колоде игрока (победные очки)")]
        [SerializeField] private TextMeshProUGUI _valueInDeckText;
        [Tooltip("Ценность карты во внутреннем круге игрока (победные очки)")]
        [SerializeField] private TextMeshProUGUI _valueInCircleText;
        [Tooltip("Название черты характера")]
        [SerializeField] private TextMeshProUGUI _traitNameText;

        [Header("UI: Images")]
        [Tooltip("Фоновое изображение на карте")]
        [SerializeField] private Image _backgroundImage;
        [Tooltip("Иконка черты характера")]
        [SerializeField] private Image _traitIconImage;
        [Tooltip("Иконка символа колоды (если не стартовая карта)")]
        [SerializeField] private Image _deckSymbolIcon;
        
        [Header("Rarity")]
        [Tooltip("Контейнер с иконками редкости")]
        [SerializeField] private Transform _rarityContainer;
        [Tooltip("Префаб иконки редкости")]
        [SerializeField] private Image _rarityIconPrefab;

        public CardData CardData => _cardData;

        public void SetCard(CardData data)
        {
            if(data == null)
                return;

            if(_backgroundImage != null)
            {
                _backgroundImage.sprite = data.BackgroundImage;
            }

            if(_cardNameText != null)
                _cardNameText.text = data.CardName;

            if(_traitIconImage != null)
                _traitIconImage.sprite = data.Trait.TraitIcon;

            if(_traitNameText != null)
            {
                _traitNameText.gameObject.SetActive(true);
                _traitNameText.text = data.Trait.TraitName;
            }

            if(data.DeckSymbol.RaceType == RaceType.Base)
            {
                if(_deckSymbolText != null)
                {
                    _deckSymbolText.gameObject.SetActive(true);
                    _deckSymbolText.text = data.DeckSymbolLabel;
                }

                if(_deckSymbolIcon != null)
                    _deckSymbolIcon.gameObject.SetActive(false);

                if(_cardEffectText != null)
                {
                    _cardEffectText.gameObject.SetActive(true);
                    _cardEffectText.text = data.CardEffect;
                }

                if(_valueInDeckText != null)
                {
                    _valueInDeckText.gameObject.SetActive(true);
                    _valueInDeckText.text = data.ValueInPlayerDeck.ToString();
                }

                if(_valueInCircleText != null)
                {
                    _valueInCircleText.gameObject.SetActive(true);
                    _valueInCircleText.text = data.ValueInInnerCircle.ToString();
                }

                if(_costText != null)
                    _costText.gameObject.SetActive(false);

                if(_deckCountText != null)
                    _deckCountText.gameObject.SetActive(false);

                UpdateRarity(0);
            }
            else
            {
                if(_costText != null)
                {
                    _costText.gameObject.SetActive(true);
                    _costText.text = data.Cost.ToString();
                }

                if(_deckCountText != null)
                {
                    _deckCountText.gameObject.SetActive(true);
                    _deckCountText.text = $"{data.CurrentIndexInDeck}/{data.TotalCardsInDeck}";
                }
                
                if(_cardEffectText != null)
                {
                    _cardEffectText.gameObject.SetActive(true);
                    _cardEffectText.text = data.CardEffect;
                }
                
                if(_valueInDeckText != null)
                {
                    _valueInDeckText.gameObject.SetActive(true);
                    _valueInDeckText.text = data.ValueInPlayerDeck.ToString();
                }
                
                if(_valueInCircleText != null)
                {
                    _valueInCircleText.gameObject.SetActive(true);
                    _valueInCircleText.text = data.ValueInInnerCircle.ToString();
                }
                
                if(_deckSymbolIcon != null)
                {
                    _deckSymbolIcon.gameObject.SetActive(true);
                    _deckSymbolIcon.sprite = data.DeckSymbol.SymbolIcon;
                }
                
                if(_deckSymbolText != null)
                    _deckSymbolText.gameObject.SetActive(false);
                
                UpdateRarity(data.RarityLevel);
            }
        }
        
        private void UpdateRarity(int level)
        {
            if(_rarityContainer == null)
                return;
            
            int i = 0;
            
            foreach(Transform child in _rarityContainer)
            {
                if(child != null)
                    child.gameObject.SetActive(i < level);
                
                i++;
            }
        }
    }
}