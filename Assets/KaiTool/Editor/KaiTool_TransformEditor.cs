using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace KaiTool.KaiTool_Editor
{
    [CustomEditor(typeof(Transform))]
    [CanEditMultipleObjects]
    public class KaiTool_TransformEditor : Editor
    {
        private Editor m_editor;
        private Transform m_transform;

        private void OnEnable()
        {
            m_transform = (Transform)target;
            m_editor = CreateEditor(target, Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.TransformInspector", true));
        }
        public override void OnInspectorGUI()
        {
#if KAITOOL_EDITOR
            serializedObject.Update();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            var guiStyle0 = new GUIStyle();
            guiStyle0.fixedWidth = 20f;
            using (var verticalScope = new GUILayout.HorizontalScope(guiStyle0))
            {
                EditorGUILayout.BeginVertical();
                if (GUILayout.Button("P"))
                {
                    m_transform.localPosition = Vector3.zero;
                }
                if (GUILayout.Button("R"))
                {
                    m_transform.localRotation = Quaternion.identity;
                }
                if (GUILayout.Button("S"))
                {
                    m_transform.localScale = Vector3.one;
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.BeginVertical();
            m_editor.OnInspectorGUI();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
#else
            m_editor.OnInspectorGUI();
#endif
            serializedObject.ApplyModifiedProperties();

        }
        private void OnDisable()
        {
            DestroyImmediate(m_editor);
        }
        [MenuItem("Tools/KaiTool/GameObject/ToggleActive %&a")]
        private static void ToggleActive()
        {
            var selectedObjects = Selection.gameObjects;
            if (selectedObjects != null)
            {
                foreach (var item in selectedObjects)
                {
                    var gameObject = item;
                    gameObject.SetActive(!gameObject.activeSelf);
                }

            }
        }
        [MenuItem("Tools/KaiTool/Transform/ResetLocalPosition %#w")]
        private static void ResetLocalPosition()
        {
            var selectedObjects = Selection.gameObjects;
            if (selectedObjects != null)
            {
                foreach (var item in selectedObjects)
                {
                    var gameObject = item;
                    gameObject.transform.localPosition = Vector3.zero;
                }

            }
        }
        [MenuItem("Tools/KaiTool/Transform/ResetLocalRotation %#e")]
        private static void ResetLocalRotation()
        {
            var selectedObjects = Selection.gameObjects;
            if (selectedObjects != null)
            {
                foreach (var item in selectedObjects)
                {
                    var gameObject = item;
                    gameObject.transform.localRotation = Quaternion.identity;
                }
            }
        }
        [MenuItem("Tools/KaiTool/Transform/ResetLocalScale %#r")]
        private static void ResetLocalScale()
        {
            var selectedObjects = Selection.gameObjects;
            if (selectedObjects != null)
            {
                foreach (var item in selectedObjects)
                {
                    var gameObject = item;
                    gameObject.transform.localScale = Vector3.one;
                }
            }
        }
        [MenuItem("Tools/KaiTool/GameObject/ToggleActive %&a", true)]
        [MenuItem("Tools/KaiTool/Transform/ResetLocalPosition %#w", true)]
        [MenuItem("Tools/KaiTool/Transform/ResetLocalRotation %#e", true)]
        [MenuItem("Tools/KaiTool/Transform/ResetLocalScale %#r", true)]

        private static bool CheckSelection()
        {
            var selectedObject = Selection.activeObject;
            if (selectedObject != null && selectedObject.GetType() == typeof(GameObject))
            {
                return true;
            }
            return false;
        }


    }
}