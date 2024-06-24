using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

    [CustomPropertyDrawer(typeof(SerializedDictionary<,>))]
    public class SerializedDictionaryPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Calculate the total height required
            float totalHeight = EditorGUIUtility.singleLineHeight;

            // Draw label and foldout
            Rect foldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label);
            position.y += EditorGUIUtility.singleLineHeight;

            // Exit early if not expanded
            if (!property.isExpanded)
            {
                EditorGUI.EndProperty();
                return;
            }

            // Find the _serializedList property
            SerializedProperty serializedListProperty = property.FindPropertyRelative("_serializedList");

            if (serializedListProperty == null)
            {
                Debug.LogError("_serializedList property is null");
                EditorGUI.EndProperty();
                return;
            }

            // Draw each element in the _serializedList
            for (int i = 0; i < serializedListProperty.arraySize; i++)
            {
                SerializedProperty elementProperty = serializedListProperty.GetArrayElementAtIndex(i);
                SerializedProperty keyProperty = elementProperty.FindPropertyRelative("Key");
                SerializedProperty valueProperty = elementProperty.FindPropertyRelative("Value");

                if (keyProperty != null && valueProperty != null)
                {
                    Rect keyRect = new Rect(position.x, position.y, position.width / 3, EditorGUIUtility.singleLineHeight);
                    Rect audioClipRect = new Rect(position.x + position.width / 3, position.y, position.width / 3, EditorGUIUtility.singleLineHeight);
                    Rect volumeRect = new Rect(position.x + 2 * (position.width / 3), position.y, position.width / 3, EditorGUIUtility.singleLineHeight);

                    // Draw the key
                    EditorGUI.PropertyField(keyRect, keyProperty, GUIContent.none);
                    
                    // Draw the AudioClip
                    SerializedProperty audioClipProperty = valueProperty.FindPropertyRelative("Clip");
                    EditorGUI.PropertyField(audioClipRect, audioClipProperty, GUIContent.none);

                    // Draw the volume
                    SerializedProperty volumeProperty = valueProperty.FindPropertyRelative("Volume");
                    EditorGUI.PropertyField(volumeRect, volumeProperty, GUIContent.none);

                    position.y += EditorGUIUtility.singleLineHeight;
                    totalHeight += EditorGUIUtility.singleLineHeight;
                }
            }

            // Increase the total height for the add button
            totalHeight += EditorGUIUtility.singleLineHeight;

            // Apply modified properties
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float totalHeight = EditorGUIUtility.singleLineHeight;

            // Find the _serializedList property
            SerializedProperty serializedListProperty = property.FindPropertyRelative("_serializedList");

            if (property.isExpanded && serializedListProperty != null)
            {
                // Calculate the height for each element in the _serializedList
                for (int i = 0; i < serializedListProperty.arraySize; i++)
                {
                    totalHeight += EditorGUIUtility.singleLineHeight;
                }

                // Add height for the add button
                totalHeight += EditorGUIUtility.singleLineHeight;
            }

            return totalHeight;
        }
    }
