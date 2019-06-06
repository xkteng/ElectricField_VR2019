using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KaiTool.Utilities
{
    [CreateAssetMenu(menuName = "KaiTool/LevelLoader/LevelAssembly", fileName = "LevelAssembly_Default")]
    public class KT_LevelAssembly : SerializedScriptableObject
    {
        public Dictionary<LevelName, string[]> m_levelAssemblyDic = new Dictionary<LevelName, string[]>();
    }
    public enum LevelName
    {
        Entrance,
        Scene0,
        Scene1,
        None,
    }
}