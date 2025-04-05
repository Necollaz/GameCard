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

            SetupImage(_backgroundImage, data.BackgroundImage, true);
            SetupText(_cardNameText, data.CardName, true);
            SetupImage(_traitIconImage, data.Trait.TraitIcon, true);
            SetupText(_traitNameText, data.Trait.TraitName, true);

            if (data.DeckSymbol.RaceType == RaceType.Base)
            {
                SetupText(_deckSymbolText, data.DeckSymbolLabel, true);
                SetActive(_deckSymbolIcon, false);
                SetupText(_cardEffectText, data.CardEffect, true);
                SetupText(_valueInDeckText, data.ValueInPlayerDeck.ToString(), true);
                SetupText(_valueInCircleText, data.ValueInInnerCircle.ToString(), true);
                SetActive(_costText, false);
                SetActive(_deckCountText, false);
            }
            else
            {
                SetupText(_costText, data.Cost.ToString(), true);
                SetupText(_deckCountText, $"{data.CurrentIndexInDeck}/{data.TotalCardsInDeck}", true);
                SetupText(_cardEffectText, data.CardEffect, true);
                SetupText(_valueInDeckText, data.ValueInPlayerDeck.ToString(), true);
                SetupText(_valueInCircleText, data.ValueInInnerCircle.ToString(), true);
                SetupImage(_deckSymbolIcon, data.DeckSymbol.SymbolIcon, true);
                SetActive(_deckSymbolText, false);
            }

            UpdateRarity(data.RarityLevel);
        }
        
        private void SetupText(TextMeshProUGUI textComponent, string value, bool active)
        {
            if (textComponent != null)
            {
                textComponent.gameObject.SetActive(active);
                textComponent.text = value;
            }
        }

        private void SetupImage(Image imageComponent, Sprite sprite, bool active)
        {
            if (imageComponent != null)
            {
                imageComponent.gameObject.SetActive(active);
                imageComponent.sprite = sprite;
            }
        }

        private void SetActive(Graphic graphicComponent, bool active)
        {
            if (graphicComponent != null)
                graphicComponent.gameObject.SetActive(active);
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