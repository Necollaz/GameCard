using System;
using UnityEngine;

namespace GameComponents.Scripts.CardComponents.CardDataComponents
{
    [Serializable]
    public class TraitData
    {
        [Tooltip("Название черты характера")]
        [SerializeField] private string _traitName;
        [Tooltip("Иконка черты характера")]
        [SerializeField] private Sprite _traitIcon;
        
        public string TraitName => _traitName;
        public Sprite TraitIcon => _traitIcon;
    }
}