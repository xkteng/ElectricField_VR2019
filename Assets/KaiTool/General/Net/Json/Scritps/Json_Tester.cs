using UnityEngine;

namespace KaiTool.Json
{
    public class Json_Tester : MonoBehaviour
    {
        private void Start()
        {
            var path = Application.streamingAssetsPath + "/json/SceneDataAssemble.json";
            var dic = KaiTool_JsonReader.GetDictionary(path);
        }

        [ContextMenu("Vector2")]
        private void PrintVector2()
        {
            var v2 = new KT_JsonVector2(0f, 0f);
            print(v2.ToJson());
        }
        [ContextMenu("Vector3")]
        private void PrintVector3()
        {
            var v3 = new KT_JsonVector3(0f, 0f, 0f);
            print(v3.ToJson());
        }
        [ContextMenu("Vector4")]
        private void PrintVector4()
        {
            var v4 = new KT_JsonVector4(0f, 0f, 0f, 0f);
            print(v4.ToJson());
        }

        [ContextMenu("Quaternion")]
        private void PrintQuaterion()
        {
            var q = new KT_JsonQuaterion(0f,0f,0f,0f);
            print(q.ToJson());
        }
    }
}