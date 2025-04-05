using UnityEngine;
using DG.Tweening;

namespace GameComponents.Scripts.CardComponents.CardInterfaces
{
    public interface ICardAnimationHelper
    {
        public Tween MoveCard(Transform cardTransform, Vector3 targetPosition, float duration);
        public Tween RotateCard(Transform cardTransform, Vector3 targetEulerAngles, float duration);
        public void ResetCard(Transform cardTransform, Vector3 position, Vector3 eulerAngles);
    }
}