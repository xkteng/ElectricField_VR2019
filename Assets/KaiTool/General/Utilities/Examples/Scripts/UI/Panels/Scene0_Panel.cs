//***********************************************
//By Kai
//Description: Scene0 Panel
//
//
//***********************************************
using SeperateUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.Utilities
{
    public class Scene0_Panel : PanelBase
    {
        public override void Init(UIManager uiManager, params object[] args)
        {
            base.Init(uiManager, args);
            m_skinPath = "UI/Panels/Scene0_Panel";
            m_layer = 1;
        }
        protected override void RegisterButtonEvent(string name)
        {
            switch (name)
            {
                case "Cube_Btn":
                    Scene0_Event.LoadShape.Invoke("cube");
                    break;
                case "Sphere_Btn":
                    Scene0_Event.LoadShape.Invoke("sphere");
                    break;
                case "Clear_Btn":
                    Scene0_Event.ClearGameObject.Invoke();
                    break;
                case "Back_Btn":
                    //GameEvent.PlayerLoadGameLevel_Start.Invoke(LevelName.Entrance, true);
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