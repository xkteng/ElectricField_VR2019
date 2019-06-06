//***********************************************
//By Kai
//Description:LevelLoader 接口
//
//
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KaiTool.Utilities
{
    public class LevelLoadEvent : UnityEvent<LevelName> { }

    public interface ILevelLoader
    {
        void LoadLevel(LevelName levelName, bool useLoadingScene);
        void LoadLevel(LevelName levelName);
        LevelLoadEvent LevelLoaded { get; }
        FloatValueEvent UpdateLoadingProgress { get; }
    }
}