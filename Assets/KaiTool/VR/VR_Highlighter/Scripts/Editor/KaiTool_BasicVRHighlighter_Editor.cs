using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.EditorGUILayout;

namespace KaiTool.VR.Highlighter
{
    [CustomEditor(typeof(KaiTool_VR_BasicHighlighter))]
    [CanEditMultipleObjects]
    public class KaiTool_BasicVRHighlighter_Editor : Editor
    {
        protected static bool s_isShowEvents = false;

        //private SerializedProperty m_isHighlighted;
        private SerializedProperty m_onHighlighted;
        private SerializedProperty m_onUnhighlighted;
        private KaiTool_VR_BasicHighlighter m_VRHighlighter;
        protected virtual void OnEnable()
        {
            //m_isHighlighted = serializedObject.FindProperty("m_isHighlighted");
            m_onHighlighted = serializedObject.FindProperty("m_onHighlighted");
            m_onUnhighlighted = serializedObject.FindProperty("m_onUnhighlighted");
            m_VRHighlighter = (KaiTool_VR_BasicHighlighter)target;
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            var title_Style = new GUIStyle();
            title_Style.fontStyle = FontStyle.Bold;
            //EditorGUILayout.PropertyField(m_isHighlighted);
            if (m_VRHighlighter.IsHighlighted)
            {
                EditorGUILayout.LabelField("IsHighlighted : TRUE", title_Style);
            }
            else
            {
                EditorGUILayout.LabelField("IsHighlighted : FALSE", title_Style);
            }

            //using (new HorizontalScope())
            //{
            //    EditorGUILayout.LabelField("Events", title_Style);
            //}
            using (var toggleGroup = new ToggleGroupScope("Events", s_isShowEvents))
            {
                s_isShowEvents = toggleGroup.enabled;
                if (s_isShowEvents)
                {
                    EditorGUILayout.PropertyField(m_onHighlighted);
                    EditorGUILayout.PropertyField(m_onUnhighlighted);
                }
            }
            if (Application.IsPlaying(this))
            {
                EditorGUILayout.LabelField("Buttons", title_Style);
                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Highlight"))
                    {
                        m_VRHighlighter.Highlight();
                    }
                    if (GUILayout.Button("Unhighlight"))
                    {
                        m_VRHighlighter.Unhighlight();
                    }
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
