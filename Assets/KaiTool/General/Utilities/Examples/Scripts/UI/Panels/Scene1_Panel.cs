//***********************************************
//By Kai
//Description:Scene1 Panel
//
//
//***********************************************
using SeperateUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.Utilities
{
    public class Scene1_Panel : PanelBase
    {
        public override void Init(UIManager uiManager, params object[] args)
        {
            base.Init(uiManager, args);
            m_skinPath = "UI/Panels/Scene1_Panel";
            m_layer = 1;
        }
        protected override void RegisterButtonEvent(string name)
        {
            switch (name)
            {
                case "Back_Btn":
                    Manager.Instance.PlayerLoadGameLevel_Start.Invoke(LevelName.Entrance, true);
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