using UnityEditor;
using GameComponents.Scripts.CardComponents.CardDataComponents;

namespace GameComponents.Scripts.CardComponents.CardEditor.InspectorEditors
{
    [CustomEditor(typeof(CardView))]
    public class CardViewEditor : Editor
    {
        private const string CardData = "_cardData";
        private const string CardNameText = "_cardNameText";
        private const string CostText = "_costText";
        private const string DeckSymbolText = "_deckSymbolText";
        private const string DeckCountText = "_deckCountText";
        private const string CardEffectText = "_cardEffectText";
        private const string ValueInDeckText = "_valueInDeckText";
        private const string ValueInCircleText = "_valueInCircleText";
        private const string TraitIconImage = "_traitIconImage";
        private const string DeckSymbolIcon = "_deckSymbolIcon";
        private const string RarityContainer = "_rarityContainer";
        private const string TraitNameText = "_traitNameText";
        private const string BackgroundImage = "_backgroundImage";

        private SerializedProperty _cardDataProp;
        private SerializedProperty _cardNameTextProp;
        private SerializedProperty _costTextProp;
        private SerializedProperty _deckSymbolTextProp;
        private SerializedProperty _deckCountTextProp;
        private SerializedProperty _cardEffectTextProp;
        private SerializedProperty _valueInDeckTextProp;
        private SerializedProperty _valueInCircleTextProp;
        private SerializedProperty _traitIconImageProp;
        private SerializedProperty _deckSymbolIconProp;
        private SerializedProperty _rarityContainerProp;
        private SerializedProperty _traitNameTextProp;
        private SerializedProperty _backgroundImageProp;

        private void OnEnable()
        {
            _cardDataProp = serializedObject.FindProperty(CardData);
            _cardNameTextProp = serializedObject.FindProperty(CardNameText);
            _costTextProp = serializedObject.FindProperty(CostText);
            _deckSymbolTextProp = serializedObject.FindProperty(DeckSymbolText);
            _deckCountTextProp = serializedObject.FindProperty(DeckCountText);
            _cardEffectTextProp = serializedObject.FindProperty(CardEffectText);
            _valueInDeckTextProp = serializedObject.FindProperty(ValueInDeckText);
            _valueInCircleTextProp = serializedObject.FindProperty(ValueInCircleText);
            _traitIconImageProp = serializedObject.FindProperty(TraitIconImage);
            _deckSymbolIconProp = serializedObject.FindProperty(DeckSymbolIcon);
            _rarityContainerProp = serializedObject.FindProperty(RarityContainer);
            _traitNameTextProp = serializedObject.FindProperty(TraitNameText);
            _backgroundImageProp = serializedObject.FindProperty(BackgroundImage);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_cardDataProp);
            EditorGUILayout.PropertyField(_backgroundImageProp);

            CardData cardData = _cardDataProp.objectReferenceValue as CardData;

            if(cardData != null && cardData.DeckSymbol.RaceType == RaceType.Base)
            {
                EditorGUILayout.LabelField("Base Card Fields", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(_cardNameTextProp);
                EditorGUILayout.PropertyField(_deckSymbolTextProp);
                EditorGUILayout.PropertyField(_cardEffectTextProp);
                EditorGUILayout.PropertyField(_valueInDeckTextProp);
                EditorGUILayout.PropertyField(_valueInCircleTextProp);
                EditorGUILayout.PropertyField(_traitIconImageProp);
                EditorGUILayout.PropertyField(_traitNameTextProp);
            }
            else
            {
                EditorGUILayout.LabelField("All Card Fields", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(_cardNameTextProp);
                EditorGUILayout.PropertyField(_costTextProp);
                EditorGUILayout.PropertyField(_deckCountTextProp);
                EditorGUILayout.PropertyField(_cardEffectTextProp);
                EditorGUILayout.PropertyField(_valueInDeckTextProp);
                EditorGUILayout.PropertyField(_valueInCircleTextProp);
                EditorGUILayout.PropertyField(_traitIconImageProp);
                EditorGUILayout.PropertyField(_deckSymbolIconProp);
                EditorGUILayout.PropertyField(_rarityContainerProp);
                EditorGUILayout.PropertyField(_traitNameTextProp);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}