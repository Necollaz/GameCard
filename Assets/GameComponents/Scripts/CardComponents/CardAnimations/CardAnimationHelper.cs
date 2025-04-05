using UnityEngine;
using DG.Tweening;
using GameComponents.Scripts.CardComponents.CardInterfaces;

namespace GameComponents.Scripts.CardComponents.CardAnimations
{
    public class CardAnimationHelper : ICardAnimationHelper
    {
        public Tween MoveCard(Transform cardTransform, Vector3 targetPosition, float duration)
        {
            cardTransform.DOKill(true);
            
            return cardTransform.DOLocalMove(targetPosition, duration).SetEase(Ease.InOutSine);
        }

        public Tween RotateCard(Transform cardTransform, Vector3 targetEulerAngles, float duration)
        {
            cardTransform.DOKill(true);
            
            return cardTransform.DOLocalRotate(targetEulerAngles, duration).SetEase(Ease.InOutSine);
        }

        public void ResetCard(Transform cardTransform, Vector3 position, Vector3 eulerAngles)
        {
            cardTransform.DOKill(true);
            cardTransform.localPosition = position;
            cardTransform.localEulerAngles = eulerAngles;
        }
    }
}