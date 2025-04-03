using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GameComponents.Scripts.CardComponents.CardDataComponents;

namespace GameComponents.Scripts.CardComponents.DeckSystem
{
    public class DeckHandCardsPlayer : MonoBehaviour
    {
        [SerializeField] private EmptyHandTransition _emptyHandTransition;
        [SerializeField] private DeckPlayedCardsPlayer _playedDeck;
        [SerializeField] private Vector3 _centerPosition = Vector3.zero;
        [SerializeField] private float _moveDuration = 0.5f;
        [SerializeField] private float _cardSpacing = 300f;
        [SerializeField] private float _fanAngle = 45f;

        private readonly List<CardView> _handCards = new List<CardView>();

        public void AddCard(CardView card)
        {
            card.transform.DOKill(true);
            
            if(card.TryGetComponent(out CardHoverEffect hoverEffect))
            {
                hoverEffect.ForceExitHover();
            }

            _handCards.Add(card);
            card.transform.SetParent(transform, false);

            if(card.TryGetComponent(out CardFlipper flipper))
            {
                flipper.ResetToFaceUp();
            }

            ArrangeCards();
        }
        
        public void RemoveCard(CardView card)
        {
            if(card.TryGetComponent(out CardHoverEffect hoverEffect))
            {
                hoverEffect.ForceExitHover();
            }

            _handCards.Remove(card);

            ArrangeCards();
        }
        
        public void HandleCardPlayed(CardView card)
        {
            if (_handCards.Contains(card))
            {
                RemoveCard(card);
                _playedDeck.AddCard(card);
                
                if (_handCards.Count == 0)
                {
                    _emptyHandTransition.CheckAndHandleEmptyHand();
                }
            }
        }

        public List<CardView> GetCards()
        {
            return new List<CardView>(_handCards);
        }

        private void ArrangeCards()
        {
            foreach (CardView card in _handCards)
            {
                if(card.TryGetComponent(out CardHoverEffect hoverEffect))
                {
                    hoverEffect.ForceExitHover();
                }
            }
            
            foreach (CardView card in _handCards)
            {
                card.transform.DOKill(true);
            }
        
            int count = _handCards.Count;

            if(count > 0)
            {
                float totalWidth = (count - 1) * _cardSpacing;
                List<(CardView card, float offsetX, float angle)> cardArrangement = new List<(CardView, float, float)>();

                for (int i = 0; i < count; i++)
                {
                    float offsetX = i * _cardSpacing - totalWidth / 2;
                    float t = count > 1 ? (float)i / (count - 1) : 0.5f;
                    float angle = Mathf.Lerp(-_fanAngle / 2, _fanAngle / 2, t);
                    cardArrangement.Add((_handCards[i], offsetX, angle));
                }

                cardArrangement.Sort((a, b) => a.offsetX.CompareTo(b.offsetX));

                for (int i = 0; i < cardArrangement.Count; i++)
                {
                    var (card, offsetX, angle) = cardArrangement[i];
                    Vector3 pos = _centerPosition + new Vector3(offsetX, 0, 0);
                    Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

                    card.transform.DOKill(true);
                    card.transform.DOLocalMove(pos, _moveDuration).SetEase(Ease.OutQuad).OnComplete(() => { card.transform.localPosition = pos; });
                    card.transform.DOLocalRotateQuaternion(targetRotation, _moveDuration).SetEase(Ease.OutQuad).OnComplete(() => { card.transform.localEulerAngles = targetRotation.eulerAngles; });
                    card.transform.SetSiblingIndex(count - 1 - i);
                }
            }
        
            DOVirtual.DelayedCall(_moveDuration, () =>
            {
                foreach(CardView card in _handCards)
                {
                    if (card.TryGetComponent(out CardHoverEffect hoverEffect))
                    {
                        hoverEffect.SetOrigin(card.transform.localPosition, card.transform.localEulerAngles, card.transform.GetSiblingIndex());
                    }
                    
                    if (card.TryGetComponent(out CardFlipper flipper))
                    {
                        if (!flipper.IsPlayed)
                            flipper.IsInteractive = true;
                    }
                }
            });
        }
    }
}