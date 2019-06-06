using UnityEngine;
using UnityEditor;
namespace KaiTool.TrackRenderer
{
    [CustomEditor(typeof(KaiTool_TrackRendererController))]
    [CanEditMultipleObjects]
    public class KaiTool_TrackRendererControllerEditor : Editor
    {
        private SerializedProperty m_orginalTrackes;
        private KaiTool_TrackRendererController m_trackRendererController;
        private void OnEnable()
        {
            m_orginalTrackes = serializedObject.FindProperty("m_orginalTrackes");
            m_trackRendererController = (KaiTool_TrackRendererController)target;
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space();
            GUI.skin.label.fontSize = 24;
            GUI.skin.label.fontStyle = FontStyle.Bold;
            GUILayout.Label("TrackRendererCtrl");
            EditorGUILayout.Space();
            GUI.skin.label.fontSize = 12;
            GUI.skin.label.fontStyle = FontStyle.Normal;
            EditorGUILayout.PropertyField(m_orginalTrackes, true);
            if (GUILayout.Button("Play"))
            {
                m_trackRendererController.Play();
            }
            if (GUILayout.Button("Restart"))
            {
                m_trackRendererController.Restart();
            }
            if (GUILayout.Button("UpdateFigure"))
            {
                m_trackRendererController.UpdateAllTrackFigure();
            }
            EditorGUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }

    }
}