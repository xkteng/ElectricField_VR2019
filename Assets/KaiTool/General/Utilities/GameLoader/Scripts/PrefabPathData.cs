using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.Utilities
{
    [CreateAssetMenu(menuName = "KaiTool/GameObjectLoader/PrefabPathData", fileName = "PrefabPathData_Default")]

    public class PrefabPathData : SerializedScriptableObject
    {
        public Dictionary<string, GameObject> m_prefabPathDic = new Dictionary<string, GameObject>();
    }
}