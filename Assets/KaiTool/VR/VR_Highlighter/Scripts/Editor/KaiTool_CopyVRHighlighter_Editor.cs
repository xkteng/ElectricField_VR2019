using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KaiTool.VR.Highlighter
{
    [CustomEditor(typeof(KaiTool_VR_CopyHighlighter))]
    [CanEditMultipleObjects]
    public class KaiTool_CopyVRHighlighter_Editor : KaiTool_BasicVRHighlighter_Editor
    {
        private SerializedProperty m_highlight_Mat;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_highlight_Mat = serializedObject.FindProperty("m_highlight_Mat");
        }
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(m_highlight_Mat);
            base.OnInspectorGUI();
        }
    }
}