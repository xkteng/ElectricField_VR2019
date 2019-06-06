using System;
using UnityEngine;

namespace KaiTool.Utilities
{
    [CreateAssetMenu(menuName = "KaiTool/Spawn/SpawnPointData", fileName = "SpawnPointData_Default")]
    public class SpawnPointData : ScriptableObject
    {
        public SpawnInfo[] m_infos;
    }

    [Serializable]
    public class SpawnInfo
    {
        public string m_key;
        public Vector3 m_position;
        public Vector3 m_rotation;
    }
}