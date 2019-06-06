using UnityEditor;
using UnityEngine;
namespace KaiTool.LightGenerator
{
    [CustomEditor(typeof(KaiTool_LightGenerator))]
    [CanEditMultipleObjects]
    public class KaiTool_LightGeneratorEditor : Editor
    {
        private SerializedProperty m_light;
        private SerializedProperty m_lightAnchors;
        private SerializedProperty m_relativePosition;
        private SerializedProperty m_relativeEuler;
        private SerializedProperty m_gizmosSize;
        private KaiTool_LightGenerator m_lightGenerator;


        private void OnEnable()
        {

            m_lightGenerator = (KaiTool_LightGenerator)target;
            m_light = serializedObject.FindProperty("m_light");
            m_lightAnchors = serializedObject.FindProperty("m_lightAnchors");
            m_relativePosition = serializedObject.FindProperty("m_relativePosition");
            m_relativeEuler = serializedObject.FindProperty("m_relativeEuler");
            m_gizmosSize = serializedObject.FindProperty("m_gizmosSize");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            GUILayout.BeginVertical();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            GUI.skin.label.fontSize = 24;
            GUI.skin.label.fontStyle = FontStyle.Bold;
            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            GUILayout.Label("Light Generator");
            GUI.skin.label.fontSize = 12;
            GUI.skin.label.fontStyle = FontStyle.Normal;
            EditorGUILayout.PropertyField(m_light);
            EditorGUILayout.PropertyField(m_lightAnchors, true);
            EditorGUILayout.Space();
            GUI.skin.label.fontStyle = FontStyle.Bold;
            GUILayout.Label("Relative Position & Relative Rotation");
            GUI.skin.label.fontStyle = FontStyle.Normal;
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(m_relativePosition);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(m_relativeEuler);
            EditorGUILayout.Space();
            GUI.skin.label.fontStyle = FontStyle.Bold;
            EditorGUILayout.PropertyField(m_gizmosSize);
            EditorGUILayout.Space();
            GUI.skin.label.fontStyle = FontStyle.Normal;
            if (GUILayout.Button("Update"))
            {
                m_lightGenerator.GenerateLights();
            }
            if (GUILayout.Button("Reset"))
            {
                m_lightGenerator.Reset();
            }
            GUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }
    }
}