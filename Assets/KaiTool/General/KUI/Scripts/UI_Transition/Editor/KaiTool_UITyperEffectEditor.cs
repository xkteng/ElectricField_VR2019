using UnityEditor;
using UnityEngine;
namespace KaiTool.UI
{
    [CustomEditor(typeof(KaiTool_UITyperEffect))]
    public class KaiTool_UITyperEffectEditor : Editor
    {
        private SerializedProperty m_duration;
        private SerializedProperty m_delay;
        private SerializedProperty m_isShownAtAwake;
        private SerializedProperty m_isAutoPlay;

        private KaiTool_UITyperEffect m_typerEffect;

        private void OnEnable()
        {
            m_duration = serializedObject.FindProperty("m_duration");
            m_delay = serializedObject.FindProperty("m_delay");
            m_typerEffect = (KaiTool_UITyperEffect)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            GUI.skin.label.fontSize = 24;
            GUI.skin.label.fontStyle = FontStyle.Bold;
            GUILayout.Label("TyperEffect");

            GUI.skin.label.fontSize = 24;
            GUI.skin.label.fontStyle = FontStyle.Normal;

            EditorGUILayout.PropertyField(m_duration);
            EditorGUILayout.PropertyField(m_delay);
            GUILayout.EndVertical();
        }

    }
}