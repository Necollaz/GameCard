using UnityEditor;
using UnityEngine;
using GameComponents.Scripts.CardComponents.CardDataComponents;

namespace GameComponents.Scripts.CardComponents.CardEditor.InspectorEditors
{
    [CustomEditor(typeof(CardData))]
    public class CardDataEditor : Editor
    {
        private const string CardViewPrefab = "_cardViewPrefab";
        private const string CardName = "_cardName";
        private const string Trait = "_trait";
        private const string DeckSymbol = "_deckSymbol";
        private const string DeckSymbolName = "_deckSymbolName";
        private const string RaceTypeField = "_raceType";
        private const string Cost = "_cost";
        private const string CurrentIndexInDeck = "_currentIndexInDeck";
        private const string TotalCardsInDeck = "_totalCardsInDeck";
        private const string RarityLevel = "_rarityLevel";
        private const string CardEffect = "_cardEffect";
        private const string ValueInPlayerDeck = "_valueInPlayerDeck";
        private const string ValueInInnerCircle = "_valueInInnerCircle";
        private const string BackgroundImage = "_backgroundImage";
        private const string SymbolIcon = "_symbolIcon";

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty(CardViewPrefab));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(CardName));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(Trait));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(BackgroundImage));

            SerializedProperty deckSymbolProp = serializedObject.FindProperty(DeckSymbol);
            SerializedProperty raceTypeProp = deckSymbolProp.FindPropertyRelative(RaceTypeField);
            
            EditorGUILayout.PropertyField(raceTypeProp);

            if((RaceType)raceTypeProp.enumValueIndex == RaceType.Base)
            {
                SerializedProperty deckSymbolNameProp = deckSymbolProp.FindPropertyRelative(DeckSymbolName);
                EditorGUILayout.PropertyField(deckSymbolNameProp, new GUIContent("Deck Symbol (Text)"));
            }
            else
            {
                SerializedProperty symbolIconProp = deckSymbolProp.FindPropertyRelative(SymbolIcon);
                EditorGUILayout.PropertyField(symbolIconProp, new GUIContent("Deck Symbol Icon"));
            }

            CardData cardData = (CardData)target;
            RaceType currentRaceType = cardData.DeckSymbol.RaceType;

            if(currentRaceType != RaceType.Base)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty(Cost));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(CurrentIndexInDeck));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(TotalCardsInDeck));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(RarityLevel));
            }
            else
            {
                EditorGUILayout.HelpBox("Parameters of the purchase and card numbers are not used for basic cards.", MessageType.Info);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty(CardEffect));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(ValueInPlayerDeck));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(ValueInInnerCircle));

            serializedObject.ApplyModifiedProperties();
        }
    }
}