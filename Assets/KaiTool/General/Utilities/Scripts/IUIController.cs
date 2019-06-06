//***********************************************
//By Kai
//Description：UI Controller 接口
//
//
//***********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.Utilities
{
    public interface IUIController
    {
        void OnPlayerLoadGameLevel_Start(LevelName levelName, bool useLoadingPanel);
        void OnPlayerLoadGameLevel_End(LevelName levelName);
    }
}