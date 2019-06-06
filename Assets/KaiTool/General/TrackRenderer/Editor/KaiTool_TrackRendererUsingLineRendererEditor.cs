using UnityEditor;
using UnityEngine;
namespace KaiTool.TrackRenderer
{
    [CustomEditor(typeof(KaiTool_TrackRendererUsingLineRenderer))]
    [CanEditMultipleObjects]
    public class KaiTool_TrackRendererUsingLineRendererEditor : Editor
    {

        private KaiTool_TrackRendererUsingLineRenderer m_trackRenderer;
        private SerializedProperty m_duration;
        private SerializedProperty m_delay;

        private SerializedProperty m_isStartedPlaying;
        private SerializedProperty m_isPlaying;
        private SerializedProperty m_isEnded;

        private SerializedProperty m_play;
        private SerializedProperty m_pause;
        private SerializedProperty m_resume;
        private SerializedProperty m_stop;
        private SerializedProperty m_restart;

        private SerializedProperty m_intervalTime;
        private SerializedProperty m_trackLength;
        private SerializedProperty m_trackPoints;

        private SerializedProperty m_startWidth;
        private SerializedProperty m_endWidth;
        private SerializedProperty m_startColor;
        private SerializedProperty m_endColor;

        private SerializedProperty m_isDrawGizmos;
        private SerializedProperty m_gizmosColor;
        private SerializedProperty m_gizmosSize;


        //Toggles
        private SerializedProperty m_isShowStatusProperties;
        private SerializedProperty m_isShowEventsProperties;
        private SerializedProperty m_isShowGizmosProperties;
        private SerializedProperty m_isShowLineRendererProperties;

        public bool m_isShow = false;

        private void OnEnable()
        {
            m_trackRenderer = (KaiTool_TrackRendererUsingLineRenderer)target;
            m_duration = serializedObject.FindProperty("m_duration");
            m_delay = serializedObject.FindProperty("m_delay");

            m_isStartedPlaying = serializedObject.FindProperty("m_isStartedPlaying");
            m_isPlaying = serializedObject.FindProperty("m_isPlaying");
            m_isEnded = serializedObject.FindProperty("m_isEnded");

            m_play = serializedObject.FindProperty("m_play");
            m_pause = serializedObject.FindProperty("m_pause");
            m_resume = serializedObject.FindProperty("m_resume");
            m_stop = serializedObject.FindProperty("m_stop");
            m_restart = serializedObject.FindProperty("m_restart");

            m_intervalTime = serializedObject.FindProperty("m_intervalTime");
            m_trackLength = serializedObject.FindProperty("m_trackLength");
            m_trackPoints = serializedObject.FindProperty("m_trackPoints");

            m_startColor = serializedObject.FindProperty("m_startColor");
            m_endColor = serializedObject.FindProperty("m_endColor");
            m_startWidth = serializedObject.FindProperty("m_startWidth");
            m_endWidth = serializedObject.FindProperty("m_endWidth");

            m_isDrawGizmos = serializedObject.FindProperty("m_isDrawGizmos");
            m_gizmosColor = serializedObject.FindProperty("m_gizmosColor");
            m_gizmosSize = serializedObject.FindProperty("m_gizmosSize");

            m_isShowStatusProperties = serializedObject.FindProperty("m_isShowStatusProperties");
            m_isShowEventsProperties = serializedObject.FindProperty("m_isShowEventsProperties");
            m_isShowGizmosProperties = serializedObject.FindProperty("m_isShowGizmosProperties");
            m_isShowLineRendererProperties = serializedObject.FindProperty("m_isShowLineRendererProperties");

        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space();
            GUI.skin.label.fontSize = 24;
            GUI.skin.label.fontStyle = FontStyle.Bold;
            GUILayout.Label("TrackRenderer");

            EditorGUILayout.Space();
            GUI.skin.label.fontSize = 12;
            GUI.skin.label.fontStyle = FontStyle.Normal;
            EditorGUILayout.PropertyField(m_duration);
            EditorGUILayout.PropertyField(m_delay);

            EditorGUILayout.Space();
            if (m_trackRenderer.IsShowStatusProperties = EditorGUILayout.Toggle("Status", m_trackRenderer.IsShowStatusProperties))
            {
                EditorGUILayout.PropertyField(m_isStartedPlaying);
                EditorGUILayout.PropertyField(m_isPlaying);
                EditorGUILayout.PropertyField(m_isEnded);
            }

            EditorGUILayout.Space();
            if (m_trackRenderer.IsShowEventsProperties = EditorGUILayout.Toggle("Events", m_trackRenderer.IsShowEventsProperties))
            {
                EditorGUILayout.PropertyField(m_play);
                EditorGUILayout.PropertyField(m_stop);
                EditorGUILayout.PropertyField(m_pause);
                EditorGUILayout.PropertyField(m_resume);
                EditorGUILayout.PropertyField(m_restart);
            }

            EditorGUILayout.Space();

            if (m_trackRenderer.IsShowLineRendererProperties = EditorGUILayout.Toggle("LineRenderer", m_trackRenderer.IsShowLineRendererProperties))
            {
                EditorGUILayout.PropertyField(m_startWidth);
                EditorGUILayout.PropertyField(m_endWidth);
                EditorGUILayout.PropertyField(m_startColor);
                EditorGUILayout.PropertyField(m_endColor);
            }
            EditorGUILayout.Space();

            if (m_trackRenderer.IsShowGizmosProperties = EditorGUILayout.Toggle("Gizmos", m_trackRenderer.IsShowGizmosProperties))
            {
                EditorGUILayout.PropertyField(m_isDrawGizmos);
                EditorGUILayout.PropertyField(m_gizmosColor);
                EditorGUILayout.PropertyField(m_gizmosSize);

            }

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(m_trackLength);
            EditorGUILayout.PropertyField(m_trackPoints, true);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Play"))
            {
                var args = new TrackRendererEventArgs();
                m_trackRenderer.OnPlay(args);
            }
            if (GUILayout.Button("Restart"))
            {
                var args = new TrackRendererEventArgs();
                m_trackRenderer.OnRestart(args);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Pause"))
            {
                var args = new TrackRendererEventArgs();
                m_trackRenderer.OnPause(args);
            }
            if (GUILayout.Button("Resume"))
            {
                var args = new TrackRendererEventArgs();
                m_trackRenderer.OnResume(args);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("TogglePause"))
            {
                var args = new TrackRendererEventArgs();
                m_trackRenderer.TogglePause(args);
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.EndVertical();
            m_trackRenderer.SetTrackLength();
            serializedObject.ApplyModifiedProperties();
        }
    }
}