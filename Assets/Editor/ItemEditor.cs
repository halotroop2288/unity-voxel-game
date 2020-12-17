using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnifiedFactorization;

namespace Minecraft.ItemsData {
    [CustomEditor(typeof(Item))]
    public sealed class ItemEditor : Editor {
        public override void OnInspectorGUI() {
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("m_ItemName"));
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("m_Icon"));

            this.serializedObject.ApplyModifiedProperties();
        }
    }
}