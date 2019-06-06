//***********************************************
//By Kai
//Description:开始界面
//
//
//***********************************************
using SeperateUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.Utilities
{
    public class Entrance_Panel : PanelBase
    {
        public override void Init(UIManager uiManager, params object[] args)
        {
            base.Init(uiManager, args);
            m_skinPath = "UI/Panels/Entrance_Panel";
            m_layer = 1;
        }
        protected override void RegisterButtonEvent(string name)
        {
            switch (name)
            {
                case "Scene0_Btn":
                    //GameEvent.PlayerLoadGameLevel_Start.Invoke(LevelName.Scene0, true);
                    Manager.Instance.PlayerLoadGameLevel_Start.Invoke(LevelName.Scene0, true);
                    break;
                case "Scene1_Btn":
                    //GameEvent.PlayerLoadGameLevel_Start.Invoke(LevelName.Scene1, true);
                    Manager.Instance.PlayerLoadGameLevel_Start.Invoke(LevelName.Scene1, true);
                    break;
            }
        }

        protected override void RegisterSliderEvent(string name, float value)
        {
        }

        protected override void RegisterToggleEvent(string name, bool value)
        {
        }
    }
}