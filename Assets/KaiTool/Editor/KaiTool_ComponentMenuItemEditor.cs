using System.Collections;
using UnityEditor;
using UnityEngine;
namespace KaiTool.KaiTool_Editor
{
    public class KaiTool_ComponentMenuItemEditor
    {
        [MenuItem("Tools/KaiTool/Component/DestroyAllMonobehaviours")]
        private static void DestroyAllMonoBehaviours()
        {
            var selectedObjects = Selection.gameObjects;
            foreach (var obj in selectedObjects)
            {
                foreach (var behaviour in obj.GetComponentsInChildren<MonoBehaviour>())
                {
                    UnityEngine.Object.DestroyImmediate(behaviour);
                }
            }
        }
        [MenuItem("Tools/KaiTool/Collider/DestroyAllColliders")]
        private static void DestroyAllColliders()
        {
            var selectedObjects = Selection.gameObjects;
            foreach (var obj in selectedObjects)
            {
                foreach (var collider in obj.GetComponentsInChildren<Collider>())
                {
                    UnityEngine.Object.DestroyImmediate(collider);
                }
            }
        }
        [MenuItem("Tools/KaiTool/Joint/DestroyAllJoints")]
        private static void DestroyAllJoints()
        {
            var selectedObjects = Selection.gameObjects;
            foreach (var obj in selectedObjects)
            {
                foreach (var joint in obj.GetComponentsInChildren<Joint>())
                {
                    UnityEngine.Object.DestroyImmediate(joint);
                }
            }
        }
        [MenuItem("Tools/KaiTool/Rigidbody/DestroyAllRigidbodies")]
        private static void DestroyAllRigidbodies()
        {
            var selectedObjects = Selection.gameObjects;
            foreach (var obj in selectedObjects)
            {
                foreach (var rigidbody in obj.GetComponentsInChildren<Rigidbody>())
                {
                    UnityEngine.Object.DestroyImmediate(rigidbody);
                }
            }
        }

        [MenuItem("Tools/KaiTool/Component/DestroyAllMonobehaviours", true)]
        [MenuItem("Tools/KaiTool/Collider/DestroyAllColliders", true)]
        [MenuItem("Tools/KaiTool/Joint/DestroyAllJoints", true)]
        [MenuItem("Tools/KaiTool/Rigidbody/DestroyAllRigidbodies", true)]
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