using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using KaiTool.UI;

namespace KaiTool.KaiTool_Editor
{
    [CustomEditor(typeof(KaiTool_BasicUIObject))]
    public class KaiTool_BasicUIObject_Editor : KaiTool_BasicEditor
    {

        private KaiTool_BasicUIObject UIObject { get { return target as KaiTool_BasicUIObject; } }

        private TabsBlock m_tabs;

        private SerializedProperty m_subUIObjectsToShow;
        private SerializedProperty m_subUIObjectToHide;
        private SerializedProperty m_show;
        private SerializedProperty m_hide;
        private SerializedProperty m_gizmosColor;


        private void OnEnable()
        {
            m_tabs = new TabsBlock(new Dictionary<string, System.Action>()
            {
                {"Variables", VariablesTab},
                {"Events", EventsTab},
                {"Gizmos",GizmosTab}
            });
            m_tabs.SetCurrentMethod(UIObject.lastTab);
            m_subUIObjectsToShow = serializedObject.FindProperty("m_subUIObjectsToShow");
            m_subUIObjectToHide = serializedObject.FindProperty("m_subUIObjectsToHide");
            m_show = serializedObject.FindProperty("m_show");
            m_hide = serializedObject.FindProperty("m_hide");
            m_gizmosColor = serializedObject.FindProperty("m_gizmosColor");
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            serializedObject.Update();
            Undo.RecordObject(UIObject, "KaiTool_UIBasicObject");
            m_tabs.Draw();
            if (GUI.changed)
                UIObject.lastTab = m_tabs.curMethodIndex;
            EditorUtility.SetDirty(UIObject);
            ButtonBlock();
            serializedObject.ApplyModifiedProperties();

        }

        private void VariablesTab()
        {
            /*
            using (new HorizontalBlock())
            {
                GUILayout.Label("UseSubUIObjects ", EditorStyles.boldLabel, GUILayout.Width(170f));
                UIObject.useSubUIObjects = EditorGUILayout.Toggle(UIObject.useSubUIObjects);
            }
            */
            using (new HorizontalBlock())
            {
                GUILayout.Label("IsShownOnEnable", EditorStyles.boldLabel, GUILayout.Width(130f));
                UIObject.IsShownOnEnable = EditorGUILayout.Toggle(UIObject.IsShownOnEnable);
            }
#if KAITOOL_EDITOR
            using (new HorizontalBlock())
            {
                GUILayout.Label("IsShown", EditorStyles.boldLabel, GUILayout.Width(130f));
                EditorGUILayout.Toggle(UIObject.IsShown);
            }
#endif
            EditorGUILayout.PropertyField(m_subUIObjectsToShow, true);
            EditorGUILayout.PropertyField(m_subUIObjectToHide, true);
        }
        private void EventsTab()
        {
            EditorGUILayout.PropertyField(m_show);
            EditorGUILayout.PropertyField(m_hide);
        }
        private void GizmosTab()
        {
            using (new HorizontalBlock())
            {
                GUILayout.Label("IsDrawGizmos", EditorStyles.boldLabel, GUILayout.Width(130f));
                UIObject.IsDrawGizmos = EditorGUILayout.Toggle(UIObject.IsDrawGizmos);
            }
            if (UIObject.IsDrawGizmos)
            {
                using (new HorizontalBlock())
                {
                    GUILayout.Label("GizmosSize", EditorStyles.label, GUILayout.Width(130f));
                    UIObject.GizmosSize = EditorGUILayout.FloatField(UIObject.GizmosSize);
                }
                EditorGUILayout.PropertyField(m_gizmosColor);
            }
        }
        private void ButtonBlock()
        {
            GUILayout.Space(20f);
            using (new ColoredBlock(Color.green))
            {
                using (new VerticalBlock())
                {
                    using (new HorizontalBlock())
                    {
                        if (GUILayout.Button("Show"))
                        {
                            UIObject.Show();
                        }
                        GUILayout.Space(1);
                        if (GUILayout.Button("Hide"))
                        {
                            UIObject.Hide();
                        }
                        GUILayout.Space(1);
                        if (GUILayout.Button("Reset"))
                        {
                            UIObject.Reset();
                        }
                    }
                }
            }
        }
    }
}